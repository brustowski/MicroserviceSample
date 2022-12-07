using System;

namespace Apollo.Bp.Net.Card.Core.DTOs.Common
{
	public class PaginationParams
	{
		public PaginationParams(int limit, long? afterTicks, Guid? afterId)
		{
			Limit = limit;
			AfterTicks = afterTicks;
			AfterId = afterId;
		}

		public long? AfterTicks { get; set; }

		public Guid? AfterId { get; set; }

		public int Limit { get; set; }
	}
}