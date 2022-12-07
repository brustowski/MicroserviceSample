using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class MarqetaConnectorCardResponse
	{
		[JsonProperty("data")]
		public CardResponse Data { get; set; }
	}
}