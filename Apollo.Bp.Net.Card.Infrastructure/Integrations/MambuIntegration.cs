using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.Constants;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.Exceptions;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Types.Common.Exceptions;
using Newtonsoft.Json;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace Apollo.Bp.Net.Card.Infrastructure.Integrations
{
	public class MambuIntegration : IMambuIntegration
	{
		private readonly HttpClient _httpClient;

		internal static string HttpClientKey => "mambu_integration";

		public MambuIntegration(IHttpClientFactory factory)
		{
			_httpClient = factory.CreateClient(HttpClientKey);
		}

		public async Task CreateCardAsync(string accountNumber, MambuInputModel mambuInputModel, CancellationToken token)
		{
			var requestUri = $"/api/deposits/{accountNumber}/cards";

			var requestBody = JsonConvert.SerializeObject(mambuInputModel);

			try
			{
				var response = await _httpClient.PostAsync(requestUri, new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json), token);

				if (response.StatusCode != HttpStatusCode.Created)
				{
					throw new CardBadRequestException(ErrorConstants.CardReferenceCreatingError, ErrorCodeConstants.CardReferenceCreatingError);
				}
			}
			catch (TaskCanceledException taskCanceledException)
			{
				throw new CardInternalErrorException(
					ErrorConstants.MambuIntegrationError,
					ErrorCodeConstants.MambuIntegrationError,
					taskCanceledException);
			}
			catch (Exception ex) when (ex is not ApolloException)
			{
				throw new CardInternalErrorException(
					ErrorConstants.InternalError,
					ErrorCodeConstants.MambuIntegrationError,
					ex);
			}
		}
	}
}