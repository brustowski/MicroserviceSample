using System;

namespace Apollo.Bp.Net.Card.Core.DTOs.Cards
{
	public class GetCardsFilters
	{
		public GetCardsFilters(Guid? cardId, Guid? clientId, string accountNumber)
		{
			CardId = cardId;
			ClientId = clientId;
			AccountNumber = accountNumber;
		}

		public Guid? CardId { get; set; }

		public Guid? ClientId { get; set; }

		public string AccountNumber { get; set; }
	}
}