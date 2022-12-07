using Apollo.Bp.Net.Types.Common.Enums;
using Apollo.Bp.Net.Types.Common.Exceptions;

namespace Apollo.Bp.Net.Card.Core.Exceptions
{
	public class CardBadRequestException : ApolloException
	{
		public CardBadRequestException(string msg, string code)
			: base(ErrorType.BadRequest, msg, code)
		{
		}
	}
}