using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class Error
	{
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }
	}
}