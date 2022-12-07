using System;
using Apollo.Bp.Net.Card.Core.DTOs.Validation;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Requests
{
	public class VirtualCardInputModel
	{
		[NotEmpty]
		[JsonProperty("client_id")]
		public Guid ClientId { get; set; }

		[NotEmpty]
		[JsonProperty("account_number")]
		public string AccountNumber { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[NotEmpty]
		[JsonProperty("card_product_id")]
		public string CardProductId { get; set; }
	}
}