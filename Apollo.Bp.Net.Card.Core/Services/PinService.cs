using System;
using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.Configuration;
using Apollo.Bp.Net.Card.Core.Constants;
using Apollo.Bp.Net.Card.Core.DTOs.Notifications;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.Exceptions;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Types.Common.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Apollo.Bp.Net.Card.Core.Services
{
	public class PinService : IPinService
	{
		private readonly IMarqetaIntegration _marqetaIntegration;
		private readonly MarqetaOptions _marqetaOptions;
		private readonly ICardDbContext _cardDbContext;
		private readonly IMapper _mapper;

		public PinService(IMarqetaIntegration marqetaIntegration, IOptions<MarqetaOptions> marqetaOptions, ICardDbContext cardDbContext,
			IMapper mapper)
		{
			_marqetaIntegration = marqetaIntegration;
			_marqetaOptions = marqetaOptions.Value;
			_cardDbContext = cardDbContext;
			_mapper = mapper;
		}

		public async Task SetPin(SetCardPinInputModel setCardPinInputModel, CancellationToken cancellationToken)
		{
			var card = await _cardDbContext.Cards.FirstOrDefaultAsync(c => c.Id == setCardPinInputModel.CardId, cancellationToken);

			if (card == null)
			{
				throw new CardNotFoundException(ErrorConstants.CardNotFoundError, ErrorCodeConstants.CardNotFoundError);
			}

			if (IsPinNotAllowed(setCardPinInputModel.Pin))
			{
				throw new CardBadRequestException(ErrorConstants.NotAllowedPinError, ErrorCodeConstants.NotAllowedPinError);
			}

			await using var transaction = await _cardDbContext.Database.BeginTransactionAsync(cancellationToken);

			try
			{
				card.PinIsSet = true;
				card.PinUpdateAt = DateTime.UtcNow;

				await _cardDbContext.SaveChangesAsync(cancellationToken);

				var setPinInputModel = _mapper.Map<SetPinInputModel>(setCardPinInputModel);
				await _marqetaIntegration.SetPin(setPinInputModel, cancellationToken);

				await transaction.CommitAsync(cancellationToken);
			}
			catch (ApolloException)
			{
				await transaction.RollbackAsync(cancellationToken);
				throw;
			}
			catch (Exception)
			{
				await transaction.RollbackAsync(cancellationToken);
				throw new CardInternalErrorException(ErrorConstants.CardDataSavingError, ErrorCodeConstants.CardDataSavingError);
			}
		}

		public async Task UpdatePinState(CardActionNotification message, CancellationToken cancellationToken)
		{
			if (message.State != MarqetaCardActionStateConstants.Success)
			{
				return;
			}

			var card = await _cardDbContext.Cards.FirstOrDefaultAsync(c => c.Token == message.CardToken, cancellationToken);

			if (card == null)
			{
				throw new CardNotFoundException(ErrorConstants.CardNotFoundError, ErrorCodeConstants.CardNotFoundError);
			}

			if (card.PinUpdateAt.HasValue && message.CreatedTime < card.PinUpdateAt)
			{
				return;
			}

			card.PinIsSet = true;
			card.PinUpdateAt = message.CreatedTime;

			try
			{
				await _cardDbContext.SaveChangesAsync(cancellationToken);
			}
			catch
			{
				throw new CardInternalErrorException(ErrorConstants.CardDataSavingError, ErrorCodeConstants.CardDataSavingError);
			}
		}

		private bool IsPinNotAllowed(string pin)
		{
			return _marqetaOptions.ProhibitedPins != null && _marqetaOptions.ProhibitedPins.Contains(pin);
		}
	}
}