using System;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class ChangeCardStatusMarqetaResponse
	{
		[JsonProperty("card_token")]
		public Guid CardToken { get; set; }

		[JsonProperty("reason_code")]
		public string ReasonCode { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }
	}
}