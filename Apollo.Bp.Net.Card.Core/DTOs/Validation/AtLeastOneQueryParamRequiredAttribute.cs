using System;
using System.Linq;
using Apollo.Bp.Net.Card.Core.Constants;
using Apollo.Bp.Net.Card.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Apollo.Bp.Net.Card.Core.DTOs.Validation
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AtLeastOneQueryParamRequiredAttribute : ActionFilterAttribute
	{
		private readonly string[] _keys;

		public AtLeastOneQueryParamRequiredAttribute(params string[] queryKeys)
		{
			_keys = queryKeys;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var query = context.HttpContext.Request.Query;

			if (_keys.All(key => !query.TryGetValue(key, out var value) || string.IsNullOrWhiteSpace(value)))
			{
				throw new CardBadRequestException(ErrorConstants.InvalidCardFilterParametersError, ErrorCodeConstants.InvalidFilterParametersError);
			}
		}
	}
}