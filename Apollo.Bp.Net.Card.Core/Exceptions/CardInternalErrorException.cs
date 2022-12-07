using System;
using Apollo.Bp.Net.Types.Common.Enums;
using Apollo.Bp.Net.Types.Common.Exceptions;

namespace Apollo.Bp.Net.Card.Core.Exceptions
{
	public class CardInternalErrorException : ApolloException
	{
		public CardInternalErrorException(string msg, string code)
			: base(ErrorType.InternalServerError, msg, code)
		{
		}

		public CardInternalErrorException(string msg, string code, Exception inner)
			: base(ErrorType.InternalServerError, msg, code, inner)
		{
		}
	}
}