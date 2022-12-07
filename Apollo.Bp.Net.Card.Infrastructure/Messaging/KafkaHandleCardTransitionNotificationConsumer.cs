using System;
using System.Threading;
using System.Threading.Tasks;
using Apollo.Bp.Net.Card.Core.Constants;
using Apollo.Bp.Net.Card.Core.DTOs.Notifications;
using Apollo.Bp.Net.Card.Core.Exceptions;
using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Card.Core.KafkaMessages;
using Apollo.Bp.Net.Kafka.Base;
using Microsoft.Extensions.Logging;

namespace Apollo.Bp.Net.Card.Infrastructure.Messaging
{
	public class KafkaHandleCardTransitionNotificationConsumer : IKafkaHandler<CardTransitionsMessage>
	{
		private readonly ICardService _cardService;
		private readonly ILogger<KafkaHandleCardTransitionNotificationConsumer> _logger;

		public KafkaHandleCardTransitionNotificationConsumer(ICardService cardService, ILogger<KafkaHandleCardTransitionNotificationConsumer> logger)
		{
			_cardService = cardService;
			_logger = logger;
		}

		public void Handle(CardTransitionsMessage message, CancellationToken token)
		{
			foreach (var cardTransitionMessage in message)
			{
				HandleCardTransitionMessage(cardTransitionMessage, token).Wait(token);
			}
		}

		private static bool ShouldMessageBeHandled(CardTransitionNotification message)
		{
			return message.Type is MarqetaCardTransitionTypeConstants.StateActivated
				or MarqetaCardTransitionTypeConstants.StateReinstated
				or MarqetaCardTransitionTypeConstants.StateSuspended
				or MarqetaCardTransitionTypeConstants.StateTerminated;
		}

		private async Task HandleCardTransitionMessage(CardTransitionNotification message, CancellationToken token)
		{
			if (!ShouldMessageBeHandled(message))
			{
				return;
			}

			try
			{
				await _cardService.UpdateCardState(message, token);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Card transition event was not handled");

				if (ex is not CardNotFoundException)
				{
					throw;
				}
			}
		}
	}
}