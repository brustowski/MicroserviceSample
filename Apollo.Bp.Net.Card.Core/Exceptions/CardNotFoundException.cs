using Apollo.Bp.Net.Types.Common.Enums;
using Apollo.Bp.Net.Types.Common.Exceptions;

namespace Apollo.Bp.Net.Card.Core.Exceptions
{
	public class CardNotFoundException : ApolloException
	{
		public CardNotFoundException(string msg, string code)
			: base(ErrorType.NotFound, msg, code)
		{
		}
	}
}