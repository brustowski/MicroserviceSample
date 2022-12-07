using System;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Notifications
{
	public class CardActionNotification
	{
		[JsonProperty("card_token")]
		public Guid CardToken { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("created_time")]
		public DateTime CreatedTime { get; set; }
	}
}