using System;
using Apollo.Bp.Net.Card.Core.DTOs.Validation;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Requests
{
	public class UserInputModel
	{
		[NotEmpty]
		[JsonProperty("token")]
		public Guid Token { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[NotEmpty]
		[JsonProperty("phone")]
		public string Phone { get; set; }
	}
}