using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Requests
{
	public class MambuInputModel
	{
		[JsonProperty("referenceToken")]
		public string ReferenceToken { get; set; }
	}
}