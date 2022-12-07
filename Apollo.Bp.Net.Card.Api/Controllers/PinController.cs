using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Api.Common;
using Apollo.Bp.Net.Api.Common.Controller;
using Apollo.Bp.Net.Api.Common.Model.Response;
using Apollo.Bp.Net.Card.Core.Constants;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Apollo.Bp.Net.Card.Api.Controllers
{
	/// <summary>
	/// API for PIN setup process.
	/// </summary>
	[OkResponseStatus(typeof(ResultResponse<>))]
	[Route("pins")]
	public class PinController : ApiController
	{
		private readonly IPinService _pinService;

		public PinController(IPinService pinService)
		{
			_pinService = pinService;
		}

		/// <summary>
		/// Pin setup on Marqeta side.
		/// </summary>
		/// <param name="requestId">Request ID.</param>
		/// <param name="referenceId">Reference ID.</param>
		/// <param name="setCardPinInputModel">Set card pin input model.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>OK result.</returns>
		[HttpPut]
		public async Task<IActionResult> SetPin(
			[Required(ErrorMessage = ErrorConstants.EmptyRequestIdError)]
			[FromHeader(Name = "x-request-id")]
			Guid requestId,
			[Required(ErrorMessage = ErrorConstants.EmptyReferenceIdError)]
			[FromHeader(Name = "x-reference-id")]
			Guid referenceId,
			[FromBody] SetCardPinInputModel setCardPinInputModel,
			CancellationToken cancellationToken)
		{
			await _pinService.SetPin(setCardPinInputModel, cancellationToken);

			return Ok();
		}
	}
}