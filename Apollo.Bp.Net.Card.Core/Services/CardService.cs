using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.Constants;
using Apollo.Bp.Net.Card.Core.DTOs.Cards;
using Apollo.Bp.Net.Card.Core.DTOs.Common;
using Apollo.Bp.Net.Card.Core.DTOs.Notifications;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.DTOs.Responses;
using Apollo.Bp.Net.Card.Core.Exceptions;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Types.Common.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Bp.Net.Card.Core.Services
{
	public class CardService : ICardService
	{
		private readonly IMarqetaIntegration _marqetaIntegration;
		private readonly IMambuIntegration _mambuIntegration;
		private readonly ICardDbContext _cardDbContext;
		private readonly IMapper _mapper;

		public CardService(IMarqetaIntegration marqetaIntegration, IMambuIntegration mambuIntegration, IMapper mapper, ICardDbContext cardDbContext)
		{
			_marqetaIntegration = marqetaIntegration;
			_mambuIntegration = mambuIntegration;
			_mapper = mapper;
			_cardDbContext = cardDbContext;
		}

		public async Task<PaginatedResponse<CardDto>> GetCards(GetCardsFilters filters, PaginationParams paginationParams, CancellationToken token)
		{
			var afterDate = new DateTime(paginationParams.AfterTicks ?? 0);
			var emptyParams = !paginationParams.AfterTicks.HasValue || !paginationParams.AfterId.HasValue;

			var cardsQuery = _cardDbContext.Cards.AsQueryable();

			if (filters.CardId.HasValue)
			{
				cardsQuery = cardsQuery.Where(c => c.Token == filters.CardId.Value);
			}

			if (filters.ClientId.HasValue)
			{
				cardsQuery = cardsQuery.Where(c => c.PartyId == filters.ClientId.Value);
			}

			if (!string.IsNullOrEmpty(filters.AccountNumber))
			{
				cardsQuery = cardsQuery.Where(c => c.AccountId == filters.AccountNumber);
			}

			var cards = await cardsQuery
							.OrderByDescending(x => x.CreateAt)
							.Where(p => emptyParams || (p.CreateAt < afterDate && p.Id != paginationParams.AfterId.Value))
							.Take(paginationParams.Limit + 1)
							.AsNoTracking()
							.ToListAsync(token);

			if (!cards.Any())
			{
				throw new CardNotFoundException(ErrorConstants.InvalidInputParametersError, ErrorCodeConstants.InvalidInputParametersError);
			}

			var pagination = new Pagination(cards.Count == paginationParams.Limit + 1);

			if (pagination.HasMore)
			{
				cards = cards.Take(paginationParams.Limit).ToList();
			}

			var cardsResponse = _mapper.Map<List<CardDto>>(cards);

			return new PaginatedResponse<CardDto>(cardsResponse, pagination);
		}

		public async Task SaveCardData(CardDataInputModel cardDataInputModel, CancellationToken token)
		{
			var cardEntity = _mapper.Map<Entities.Card>(cardDataInputModel);

			cardEntity.Id = Guid.NewGuid();
			cardEntity.CreateAt = DateTime.UtcNow;

			try
			{
				await _cardDbContext.Cards.AddAsync(cardEntity, token);
				await _cardDbContext.SaveChangesAsync(token);
			}
			catch (Exception ex)
			{
				throw new CardInternalErrorException(ErrorConstants.CardDataSavingError, ErrorCodeConstants.CardDataSavingError, ex);
			}
		}

		public async Task<VirtualCardResponse> CreateVirtualCard(VirtualCardInputModel virtualCardInputModel, CancellationToken token)
		{
			var userInputModel = _mapper.Map<UserInputModel>(virtualCardInputModel);
			var cardInputModel = _mapper.Map<CardInputModel>(virtualCardInputModel);

			await _marqetaIntegration.CreateUser(userInputModel, token);

			var card = await _marqetaIntegration.CreateCard(cardInputModel, token);

			var cardEntity = _mapper.Map<Entities.Card>(card);

			cardEntity.Id = Guid.NewGuid();
			cardEntity.AccountId = virtualCardInputModel.AccountNumber;
			cardEntity.CreateAt = DateTime.UtcNow;

			try
			{
				await _cardDbContext.Cards.AddAsync(cardEntity, token);
				await _cardDbContext.SaveChangesAsync(token);
			}
			catch (Exception ex)
			{
				throw new CardInternalErrorException(ErrorConstants.CardDataSavingError, ErrorCodeConstants.CardDataSavingError, ex);
			}

			var mambuInputModel = new MambuInputModel { ReferenceToken = cardEntity.Id.ToString() };

			await _mambuIntegration.CreateCardAsync(virtualCardInputModel.AccountNumber, mambuInputModel, token);

			return _mapper.Map<VirtualCardResponse>(cardEntity);
		}

		public async Task<ChangeCardStatusResponse> BlockCardAsync(Guid cardId, CancellationToken token)
		{
			var card = await _cardDbContext.Cards.FirstOrDefaultAsync(x => x.Token == cardId, token);

			if (card == null)
			{
				throw new CardNotFoundException(ErrorConstants.CardNotFoundError, ErrorCodeConstants.CardNotFoundError);
			}

			if (card.State != CardStatusConstants.Active)
			{
				throw new CardInternalErrorException(
					ErrorConstants.InvalidCardBlockingStatusError,
					ErrorCodeConstants.InvalidCardBlockingStatusError);
			}

			return await ChangeCardStatusAsync(card, CardStatusConstants.Suspended, token);
		}

		public async Task<ChangeCardStatusResponse> UnblockCardAsync(Guid cardId, CancellationToken token)
		{
			var card = await _cardDbContext.Cards.FirstOrDefaultAsync(x => x.Token == cardId, token);

			if (card == null)
			{
				throw new CardNotFoundException(ErrorConstants.CardNotFoundError, ErrorCodeConstants.CardNotFoundError);
			}

			if (card.State != CardStatusConstants.Suspended)
			{
				throw new CardInternalErrorException(
					ErrorConstants.InvalidCardUnblockingStatusError,
					ErrorCodeConstants.InvalidCardUnblockingStatusError);
			}

			return await ChangeCardStatusAsync(card, CardStatusConstants.Active, token);
		}

		public async Task UpdateCardState(CardTransitionNotification message, CancellationToken token)
		{
			var card = await _cardDbContext.Cards.FirstOrDefaultAsync(card => card.Token == message.CardToken, token);

			if (card == null)
			{
				throw new CardNotFoundException(ErrorConstants.CardNotFoundError, ErrorCodeConstants.CardNotFoundError);
			}

			if (card.StateUpdateAt.HasValue && message.CreatedTime < card.StateUpdateAt)
			{
				return;
			}

			card.State = message.State;
			card.StateReason = message.Reason;
			card.ReasonCode = message.ReasonCode;
			card.StateUpdateAt = message.CreatedTime;

			try
			{
				await _cardDbContext.SaveChangesAsync(token);
			}
			catch
			{
				throw new CardInternalErrorException(ErrorConstants.CardDataSavingError, ErrorCodeConstants.CardDataSavingError);
			}
		}

		private async Task<ChangeCardStatusResponse> ChangeCardStatusAsync(Entities.Card card, string cardStatus, CancellationToken token)
		{
			await using var transaction = await _cardDbContext.Database.BeginTransactionAsync(token);

			ChangeCardStatusMarqetaResponse marqetaResponse;

			try
			{
				card.ReasonCode = CardStatusReasonCodeConstants.RequestedByYou;
				card.State = cardStatus;
				card.StateUpdateAt = DateTime.UtcNow;

				await _cardDbContext.SaveChangesAsync(token);

				var marqetaInput = new ChangeCardStatusInputModel
				{
					CardToken = card.Token,
					Channel = CardDomainConstants.ChangeChannel,
					ReasonCode = CardStatusReasonCodeConstants.RequestedByYou,
					State = cardStatus
				};

				marqetaResponse = await _marqetaIntegration.BlockCard(marqetaInput, token);

				await transaction.CommitAsync(token);
			}
			catch (ApolloException)
			{
				await transaction.RollbackAsync(token);

				throw;
			}
			catch (Exception)
			{
				await transaction.RollbackAsync(token);

				throw new CardInternalErrorException(ErrorConstants.CardDataSavingError, ErrorCodeConstants.CardDataSavingError);
			}

			return _mapper.Map<ChangeCardStatusResponse>(marqetaResponse);
		}
	}
}