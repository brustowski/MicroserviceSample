using System;
using Apollo.Bp.Net.Card.Core.DTOs.Validation;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Requests
{
	public class CardInputModel
	{
		[NotEmpty]
		[JsonProperty("user_token")]
		public Guid UserToken { get; set; }

		[NotEmpty]
		[JsonProperty("card_product_token")]
		public Guid CardProductToken { get; set; }
	}
}