using System;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Responses
{
	public class UserResponse
	{
		[JsonProperty("token")]
		public Guid Token { get; set; }

		[JsonProperty("active")]
		public bool Active { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("corporate_card_holder")]
		public bool CorporateCardHolder { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }
	}
}