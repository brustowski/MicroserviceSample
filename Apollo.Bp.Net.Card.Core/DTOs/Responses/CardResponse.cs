using System;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class CardResponse
	{
		[JsonProperty("token")]
		public Guid Token { get; set; }

		[JsonProperty("user_token")]
		public Guid UserToken { get; set; }

		[JsonProperty("card_product_token")]
		public Guid CardProductToken { get; set; }

		[JsonProperty("last_four")]
		public string LastFour { get; set; }

		[JsonProperty("pan")]
		public string Pan { get; set; }

		[JsonProperty("expiration")]
		public string Expiration { get; set; }

		[JsonProperty("expiration_time")]
		public DateTime? ExpirationTime { get; set; }

		[JsonProperty("barcode")]
		public string BarCode { get; set; }

		[JsonProperty("pin_is_set")]
		public bool PinIsSet { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("state_reason")]
		public string StateReason { get; set; }

		[JsonProperty("fulfillment_status")]
		public string FulfillmentStatus { get; set; }

		[JsonProperty("instrument_type")]
		public string InstrumentType { get; set; }
	}
}