using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class MarqetaConnectorUserResponse
	{
		[JsonProperty("data")]
		public UserResponse Data { get; set; }
	}
}