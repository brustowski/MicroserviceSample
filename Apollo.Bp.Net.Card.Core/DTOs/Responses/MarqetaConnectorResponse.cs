using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class MarqetaConnectorResponse<T>
	{
		[JsonProperty("data")]
		public T Data { get; set; }
	}
}