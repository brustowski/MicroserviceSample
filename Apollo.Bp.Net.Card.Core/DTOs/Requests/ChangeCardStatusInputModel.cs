using System;
using Apollo.Bp.Net.Card.Core.DTOs.Validation;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Requests
{
	public class ChangeCardStatusInputModel
	{
		[NotEmpty]
		[JsonProperty("card_token")]
		public Guid CardToken { get; set; }

		[NotEmpty]
		[JsonProperty("channel")]
		public string Channel { get; set; }

		[NotEmpty]
		[JsonProperty("reason_code")]
		public string ReasonCode { get; set; }

		[NotEmpty]
		[JsonProperty("state")]
		public string State { get; set; }
	}
}