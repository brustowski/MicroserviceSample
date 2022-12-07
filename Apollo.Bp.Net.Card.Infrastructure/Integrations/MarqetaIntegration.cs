using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.Constants;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.DTOs.Responses;
using Apollo.Bp.Net.Card.Core.Exceptions;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Types.Common.Enums;
using Apollo.Bp.Net.Types.Common.Exceptions;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Infrastructure.Integrations
{
	public class MarqetaIntegration : IMarqetaIntegration
	{
		private readonly HttpClient _httpClient;

		internal static string HttpClientKey => "marqeta_integration";

		public MarqetaIntegration(IHttpClientFactory factory)
		{
			_httpClient = factory.CreateClient(HttpClientKey);
		}

		public async Task<MarqetaConnectorUserResponse> CreateUser(UserInputModel userInputModel, CancellationToken cancellationToken)
		{
			const string RequestUri = "users";

			var requestBody = JsonConvert.SerializeObject(userInputModel);

			try
			{
				var response = await _httpClient.PostAsync(RequestUri, new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json), cancellationToken);

				var contentResponse = await response.Content.ReadAsStringAsync(cancellationToken);

				if (response.IsSuccessStatusCode)
				{
					return JsonConvert.DeserializeObject<MarqetaConnectorUserResponse>(contentResponse);
				}

				var errorCode = JsonConvert.DeserializeObject<MarqetaConnectorErrorResponse>(contentResponse)?.Error?.Code;

				throw DomainException(errorCode, (ErrorConstants.CardClientCreatingError, ErrorCodeConstants.CardClientCreatingError));
			}
			catch (TaskCanceledException taskCanceledException)
			{
				throw new CardInternalErrorException(
					ErrorConstants.MarqetaConnectorError,
					ErrorCodeConstants.MarqetaConnectorError,
					taskCanceledException);
			}
			catch (Exception ex) when (ex is not ApolloException)
			{
				throw new CardDomainException(ErrorType.InternalServerError, ErrorConstants.GeneralError,
					ErrorCodeConstants.MarqetaConnectorError, ex);
			}
		}

		public async Task<MarqetaConnectorCardResponse> CreateCard(CardInputModel cardInputModel, CancellationToken cancellationToken)
		{
			const string RequestUri = "cards";

			var requestBody = JsonConvert.SerializeObject(cardInputModel);

			try
			{
				var response = await _httpClient.PostAsync(RequestUri, new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json), cancellationToken);

				var contentResponse = await response.Content.ReadAsStringAsync(cancellationToken);

				if (response.IsSuccessStatusCode)
				{
					return JsonConvert.DeserializeObject<MarqetaConnectorCardResponse>(contentResponse);
				}

				var errorCode = JsonConvert.DeserializeObject<MarqetaConnectorErrorResponse>(contentResponse)?.Error?.Code;

				throw DomainException(errorCode, (ErrorConstants.CardCreatingError, ErrorCodeConstants.CardCreatingError));
			}
			catch (Exception ex) when (ex is not ApolloException)
			{
				throw new CardInternalErrorException(ErrorConstants.CardCreatingError, ErrorCodeConstants.CardCreatingError, ex);
			}
		}

		public async Task SetPin(SetPinInputModel setPinInputModel, CancellationToken cancellationToken)
		{
			const string RequestUri = "pins";

			var requestBody = JsonConvert.SerializeObject(setPinInputModel);

			try
			{
				var response = await _httpClient.PutAsync(RequestUri, new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json), cancellationToken);

				var contentResponse = await response.Content.ReadAsStringAsync(cancellationToken);

				if (response.IsSuccessStatusCode)
				{
					return;
				}

				var errorCode = JsonConvert.DeserializeObject<MarqetaConnectorErrorResponse>(contentResponse)?.Error?.Code;

				throw DomainException(errorCode);
			}
			catch (Exception ex) when (ex is not ApolloException)
			{
				throw new CardInternalErrorException(ErrorConstants.PinSetError, ErrorCodeConstants.PinSetError, ex);
			}
		}

		public async Task<ChangeCardStatusMarqetaResponse> BlockCard(ChangeCardStatusInputModel input, CancellationToken cancellationToken)
		{
			const string RequestUri = "card-transitions";

			var requestBody = JsonConvert.SerializeObject(input);

			try
			{
				var response = await _httpClient.PostAsync(RequestUri, new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json), cancellationToken);

				var contentResponse = await response.Content.ReadAsStringAsync(cancellationToken);

				if (response.IsSuccessStatusCode)
				{
					return JsonConvert.DeserializeObject<MarqetaConnectorResponse<ChangeCardStatusMarqetaResponse>>(contentResponse).Data;
				}

				var errorCode = JsonConvert.DeserializeObject<MarqetaConnectorErrorResponse>(contentResponse)?.Error?.Code;

				throw CardStatusException(errorCode);
			}
			catch (Exception ex) when (ex is not ApolloException)
			{
				throw new CardDomainException(ErrorType.InternalServerError, ErrorConstants.GeneralError,
					ErrorCodeConstants.MarqetaConnectorError, ex);
			}
		}

		private static CardBadRequestException DomainException(string errorCode, (string Code, string Message) defaultError = default)
		{
			var (message, code) = errorCode switch
			{
				MarqetaConnectorErrorCodeConstants.ClientExist =>
					(ErrorConstants.ClientAlreadyExistError, ErrorCodeConstants.ClientAlreadyExistError),
				MarqetaConnectorErrorCodeConstants.CardHolderExist =>
					(ErrorConstants.CardHolderAlreadyExistError, ErrorCodeConstants.CardHolderAlreadyExistError),
				MarqetaConnectorErrorCodeConstants.CardNotFound =>
					(ErrorConstants.CardNotFoundError, ErrorCodeConstants.CardNotFoundError),
				MarqetaConnectorErrorCodeConstants.PinIsInvalid =>
					(ErrorConstants.InvalidPinError, ErrorCodeConstants.InvalidPinError),
				_ => (defaultError.Message ?? ErrorConstants.GeneralError, defaultError.Code ?? ErrorCodeConstants.MarqetaConnectorError)
			};

			return new CardBadRequestException(message, code);
		}

		private static CardBadRequestException CardStatusException(string errorCode, (string Code, string Message) defaultError = default)
		{
			var (message, code) = errorCode switch
			{
				MarqetaConnectorErrorCodeConstants.CardNotFound =>
					(ErrorConstants.CardNotFoundError, ErrorCodeConstants.CardNotFoundError),
				MarqetaConnectorErrorCodeConstants.GeneralError =>
					(ErrorConstants.ReasonCodeIsInvalidError, ErrorCodeConstants.ReasonCodeIsInvalidError),
				MarqetaConnectorErrorCodeConstants.MalformedJsonRequest =>
					(ErrorConstants.StateOrChannelIsInvalidError, ErrorCodeConstants.StateOrChannelIsInvalidError),
				_ => (defaultError.Message ?? ErrorConstants.GeneralError, defaultError.Code ?? ErrorCodeConstants.MarqetaConnectorError)
			};

			return new CardBadRequestException(message, code);
		}
	}
}