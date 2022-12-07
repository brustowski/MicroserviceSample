using System;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Notifications
{
	public class CardTransitionNotification
	{
		[JsonProperty("card_token")]
		public Guid CardToken { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("reason")]
		public string Reason { get; set; }

		[JsonProperty("reason_code")]
		public string ReasonCode { get; set; }

		[JsonProperty("created_time")]
		public DateTime CreatedTime { get; set; }
	}
}