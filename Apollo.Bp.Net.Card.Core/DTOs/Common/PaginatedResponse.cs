using System.Collections.Generic;
using Apollo.Bp.Net.Api.Common.Model.Response;
using Apollo.Bp.Net.Types.Common.Enums;
using Newtonsoft.Json;

namespace Apollo.Bp.Net.Card.Core.DTOs.Common
{
	public class PaginatedResponse<TObject> : StatusResponse
	{
		public PaginatedResponse()
			: base(HttpStatusCode.Ok)
		{
		}

		public PaginatedResponse(IReadOnlyCollection<TObject> result, Pagination pagination)
			: this()
		{
			Result = result;
			Pagination = pagination;
		}

		[JsonProperty("data")]
		public IReadOnlyCollection<TObject> Result { get; set; }

		[JsonProperty("paging")]
		public Pagination Pagination { get; set; }
	}
}