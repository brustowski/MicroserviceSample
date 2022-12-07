using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Common
{
	public class Pagination
	{
		public Pagination(bool hasMore)
		{
			HasMore = hasMore;
		}

		[JsonProperty("hasMore")]
		public bool HasMore { get; set; }
	}
}