using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.DTOs.Notifications;
using Apollo.Bp.Net.Card.Core.DTOs.Requests;

namespace Apollo.Bp.Net.Card.Core.Interfaces
{
	public interface IPinService
	{
		Task SetPin(SetCardPinInputModel setCardPinInputModel, CancellationToken cancellationToken);

		Task UpdatePinState(CardActionNotification message, CancellationToken cancellationToken);
	}
}