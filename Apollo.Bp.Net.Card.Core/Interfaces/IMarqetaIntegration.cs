using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;
using Apollo.Bp.Net.Card.Core.DTOs.Responses;

namespace Apollo.Bp.Net.Card.Core.Interfaces
{
	public interface IMarqetaIntegration
	{
		Task<MarqetaConnectorUserResponse> CreateUser(UserInputModel userInputModel, CancellationToken cancellationToken);

		Task<MarqetaConnectorCardResponse> CreateCard(CardInputModel cardInputModel, CancellationToken cancellationToken);

		Task SetPin(SetPinInputModel setPinInputModel, CancellationToken cancellationToken);

		Task<ChangeCardStatusMarqetaResponse> BlockCard(ChangeCardStatusInputModel input, CancellationToken cancellationToken);
	}
}