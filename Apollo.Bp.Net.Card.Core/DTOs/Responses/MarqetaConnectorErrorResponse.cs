using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class MarqetaConnectorErrorResponse
	{
		[JsonProperty("error")]
		public Error Error { get; set; }
	}
}