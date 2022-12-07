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
	public class KafkaHandleCardActionNotificationConsumer : IKafkaHandler<CardActionsMessage>
	{
		private readonly IPinService _pinService;
		private readonly ILogger<KafkaHandleCardActionNotificationConsumer> _logger;

		public KafkaHandleCardActionNotificationConsumer(IPinService pinService, ILogger<KafkaHandleCardActionNotificationConsumer> logger)
		{
			_pinService = pinService;
			_logger = logger;
		}

		public void Handle(CardActionsMessage message, CancellationToken token)
		{
			foreach (var cardActionMessage in message)
			{
				HandleCardActionMessage(cardActionMessage, token).Wait(token);
			}
		}

		private static bool ShouldMessageBeHandled(CardActionNotification message)
		{
			return message.Type is MarqetaCardActionTypeConstants.PinSet or MarqetaCardActionTypeConstants.PinChanged;
		}

		private async Task HandleCardActionMessage(CardActionNotification message, CancellationToken token)
		{
			if (!ShouldMessageBeHandled(message))
			{
				return;
			}

			try
			{
				await _pinService.UpdatePinState(message, token);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Card action event was not handled");

				if (ex is not CardNotFoundException)
				{
					throw;
				}
			}
		}
	}
}