using System;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class VirtualCardResponse
	{
		[JsonProperty("card_id")]
		public Guid CardId { get; set; }

		[JsonProperty("last_four")]
		public string LastFour { get; set; }

		[JsonProperty("pan")]
		public string Pan { get; set; }

		[JsonProperty("expiration")]
		public string Expiration { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }
	}
}