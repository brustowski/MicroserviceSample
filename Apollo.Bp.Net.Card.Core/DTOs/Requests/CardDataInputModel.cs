using System;
using Apollo.Bp.Net.Card.Core.DTOs.Validation;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Requests
{
	public class CardDataInputModel
	{
		[NotEmpty]
		[JsonProperty("card_id")]
		public Guid CardId { get; set; }

		[NotEmpty]
		[JsonProperty("party_id")]
		public Guid PartyId { get; set; }

		[NotEmpty]
		[JsonProperty("card_product_id")]
		public Guid CardProductId { get; set; }

		[NotEmpty]
		[JsonProperty("account_id")]
		public string AccountId { get; set; }

		[NotEmpty]
		[JsonProperty("last_four")]
		public string LastFour { get; set; }

		[JsonProperty("pan")]
		public string Pan { get; set; }

		[NotEmpty]
		[JsonProperty("expiration")]
		public string Expiration { get; set; }

		[JsonProperty("expiration_time")]
		public DateTime? ExpirationTime { get; set; }

		[JsonProperty("barcode")]
		public string Barcode { get; set; }

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