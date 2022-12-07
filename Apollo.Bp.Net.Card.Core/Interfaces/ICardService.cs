using System;
using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.DTOs.Cards;
using Apollo.Bp.Net.Card.Core.DTOs.Common;
using Apollo.Bp.Net.Card.Core.DTOs.Notifications;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.DTOs.Responses;

namespace Apollo.Bp.Net.Card.Core.Interfaces
{
	public interface ICardService
	{
		Task<PaginatedResponse<CardDto>> GetCards(GetCardsFilters filters, PaginationParams paginationParams, CancellationToken token);

		Task SaveCardData(CardDataInputModel cardDataInputModel, CancellationToken token);

		Task<VirtualCardResponse> CreateVirtualCard(VirtualCardInputModel virtualCardInputModel, CancellationToken token);

		Task<ChangeCardStatusResponse> BlockCardAsync(Guid cardId, CancellationToken token);

		Task<ChangeCardStatusResponse> UnblockCardAsync(Guid cardId, CancellationToken token);

		Task UpdateCardState(CardTransitionNotification message, CancellationToken token);
	}
}