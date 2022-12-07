using System;
using System.Collections.Generic;
using Apollo.Bp.Net.Card.Core.DTOs.Notifications;
using Apollo.Bp.Net.Kafka.Base;

namespace Apollo.Bp.Net.Card.Core.KafkaMessages
{
	public class CardActionsMessage : List<CardActionNotification>, IKafkaMessage
	{
		public Exception Exception { get; set; }
	}
}