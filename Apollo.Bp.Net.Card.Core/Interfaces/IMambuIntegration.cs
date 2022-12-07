using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;

namespace Apollo.Bp.Net.Card.Core.Interfaces
{
	public interface IMambuIntegration
	{
		Task CreateCardAsync(string accountNumber, MambuInputModel mambuInputModel, CancellationToken token);
	}
}