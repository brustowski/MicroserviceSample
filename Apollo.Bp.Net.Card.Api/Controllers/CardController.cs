using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Api.Common;
using Apollo.Bp.Net.Api.Common.Controller;
using Apollo.Bp.Net.Api.Common.Model.Response;
using Apollo.Bp.Net.Card.Core.Constants;
using Apollo.Bp.Net.Card.Core.DTOs.Cards;
using Apollo.Bp.Net.Card.Core.DTOs.Common;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.DTOs.Validation;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Apollo.Bp.Net.Card.Api.Controllers
{
	/// <summary>
	/// API for card creation process.
	/// </summary>
	[OkResponseStatus(typeof(ResultResponse<>))]
	[Route("cards")]
	public class CardController : ApiController
	{
		private readonly ICardService _cardService;

		public CardController(ICardService cardService)
		{
			_cardService = cardService;
		}

		/// <summary>
		/// Cards data retrieving.
		/// </summary>
		/// <param name="requestId">Request ID.</param>
		/// <param name="referenceId">Reference ID.</param>
		/// <param name="cardId">Card identifier.</param>
		/// <param name="clientId">Client identifier.</param>
		/// <param name="accountNumber">>Number of payment account.</param>
		/// <param name="afterTicks">Paging after DateTime ticks.</param>
		/// <param name="afterId">Paging after identifier.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <param name="limit">Paging page size.</param>
		/// <returns>Cards details.</returns>
		[HttpGet]
		[AtLeastOneQueryParamRequired("card_id", "client_id", "account_number")]
		public async Task<IActionResult> GetCards(
			[Required(ErrorMessage = ErrorConstants.EmptyRequestIdError)]
			[FromHeader(Name = "x-request-id")]
			Guid requestId,
			[Required(ErrorMessage = ErrorConstants.EmptyReferenceIdError)]
			[FromHeader(Name = "x-reference-id")]
			Guid referenceId,
			[FromQuery(Name = "card_id")] Guid? cardId,
			[FromQuery(Name = "client_id")] Guid? clientId,
			[FromQuery(Name = "account_number")] string accountNumber,
			[FromQuery(Name = "after_ticks")] long? afterTicks,
			[FromQuery(Name = "after_Id")] Guid? afterId,
			CancellationToken cancellationToken,
			[FromQuery(Name = "limit")] int limit = CardDomainConstants.DefaultPageSize)
		{
			var filters = new GetCardsFilters(cardId, clientId, accountNumber);
			var paginationParameters = new PaginationParams(limit, afterTicks, afterId);

			var result = await _cardService.GetCards(filters, paginationParameters, cancellationToken);

			return ReturnResponse(result);
		}

		/// <summary>
		/// Card creation on Marqeta side.
		/// </summary>
		/// <param name="requestId">Request ID.</param>
		/// <param name="referenceId">Reference ID.</param>
		/// <param name="virtualCardInputModel">Virtual card input model.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Card details.</returns>
		[HttpPost]
		public async Task<IActionResult> CreateVirtualCard(
			[Required(ErrorMessage = ErrorConstants.EmptyRequestIdError)]
			[FromHeader(Name = "x-request-id")]
			Guid requestId,
			[Required(ErrorMessage = ErrorConstants.EmptyReferenceIdError)]
			[FromHeader(Name = "x-reference-id")]
			Guid referenceId,
			[FromBody] VirtualCardInputModel virtualCardInputModel,
			CancellationToken cancellationToken)
		{
			var result = await _cardService.CreateVirtualCard(virtualCardInputModel, cancellationToken);

			return ReturnResponseResult(result);
		}

		/// <summary>
		/// Card data saving.
		/// </summary>
		/// <param name="requestId">Request ID.</param>
		/// <param name="referenceId">Reference ID.</param>
		/// <param name="cardDataInputModel">Card data input model.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>OK response.</returns>
		[HttpPost("data")]
		public async Task<IActionResult> SaveCardData(
			[Required(ErrorMessage = ErrorConstants.EmptyRequestIdError)]
			[FromHeader(Name = "x-request-id")]
			Guid requestId,
			[Required(ErrorMessage = ErrorConstants.EmptyReferenceIdError)]
			[FromHeader(Name = "x-reference-id")]
			Guid referenceId,
			[FromBody] CardDataInputModel cardDataInputModel,
			CancellationToken cancellationToken)
		{
			await _cardService.SaveCardData(cardDataInputModel, cancellationToken);

			return Ok();
		}

		/// <summary>
		/// Blocks card.
		/// </summary>
		/// <param name="requestId">Request ID.</param>
		/// <param name="referenceId">Reference ID.</param>
		/// <param name="cardId">Card ID.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Card details.</returns>
		[HttpPost("block")]
		public async Task<IActionResult> BlockCard(
			[Required(ErrorMessage = ErrorConstants.EmptyRequestIdError)]
			[FromHeader(Name = "x-request-id")]
			Guid requestId,
			[Required(ErrorMessage = ErrorConstants.EmptyReferenceIdError)]
			[FromHeader(Name = "x-reference-id")]
			Guid referenceId,
			[Required(ErrorMessage = ErrorConstants.EmptyCardIdError)]
			[FromQuery] Guid cardId,
			CancellationToken cancellationToken)
		{
			var result = await _cardService.BlockCardAsync(cardId, cancellationToken);

			return ReturnResponseResult(result);
		}

		/// <summary>
		/// Unblocks card.
		/// </summary>
		/// <param name="requestId">Request ID.</param>
		/// <param name="referenceId">Reference ID.</param>
		/// <param name="cardId">Card ID.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Card details.</returns>
		[HttpPost("unblock")]
		public async Task<IActionResult> UnblockCard(
			[Required(ErrorMessage = ErrorConstants.EmptyRequestIdError)]
			[FromHeader(Name = "x-request-id")]
			Guid requestId,
			[Required(ErrorMessage = ErrorConstants.EmptyReferenceIdError)]
			[FromHeader(Name = "x-reference-id")]
			Guid referenceId,
			[Required(ErrorMessage = ErrorConstants.EmptyCardIdError)]
			[FromQuery] Guid cardId,
			CancellationToken cancellationToken)
		{
			var result = await _cardService.UnblockCardAsync(cardId, cancellationToken);

			return ReturnResponseResult(result);
		}
	}
}