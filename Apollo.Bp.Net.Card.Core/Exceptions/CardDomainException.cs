using System;
using Apollo.Bp.Net.Types.Common.Enums;
using Apollo.Bp.Net.Types.Common.Exceptions;

namespace Apollo.Bp.Net.Card.Core.Exceptions
{
	public class CardDomainException : ApolloException
	{
		public CardDomainException(ErrorType errorType, string msg, string code)
			: base(errorType, msg, code)
		{
		}

		public CardDomainException(ErrorType errorType, string msg, string code, Exception inner)
			: base(errorType, msg, code, inner)
		{
		}
	}
}