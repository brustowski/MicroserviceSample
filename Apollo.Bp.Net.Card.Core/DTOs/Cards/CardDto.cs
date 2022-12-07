using System;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Cards
{
	public class CardDto
	{
		[JsonProperty("card_id")]
		public Guid CardId { get; set; }

		[JsonProperty("client_id")]
		public Guid ClientId { get; set; }

		[JsonProperty("account_number")]
		public string AccountNumber { get; set; }

		[JsonProperty("card_product_id")]
		public Guid? CardProductId { get; set; }

		[JsonProperty("last_four")]
		public string LastFour { get; set; }

		[JsonProperty("pan")]
		public string Pan { get; set; }

		[JsonProperty("expiration")]
		public string Expiration { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("reason_code")]
		public string ReasonCode { get; set; }
	}
}