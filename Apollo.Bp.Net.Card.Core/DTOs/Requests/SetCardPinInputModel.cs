using System;
using Apollo.Bp.Net.Card.Core.DTOs.Validation;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Requests
{
	public class SetCardPinInputModel
	{
		[NotEmpty]
		[JsonProperty("card_id")]
		public Guid CardId { get; set; }

		[NotEmpty]
		[JsonProperty("control_token")]
		public Guid CardProductToken { get; set; }

		[NotEmpty]
		[JsonProperty("pin")]
		public string Pin { get; set; }
	}
}