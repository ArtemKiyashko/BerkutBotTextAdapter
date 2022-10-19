using System;
using Azure.Messaging.ServiceBus;
using BerkutTextAdapter.Extensions;
using Telegram.Bot.Types;

namespace BerkutTextAdapter.Infrastructure
{
    public class ServiceBusMessageFactory : IServiceBusMessageFactory
    {
        private readonly ITgMessageFactory _tgMessageFactory;

        public ServiceBusMessageFactory(ITgMessageFactory tgMessageFactory)
        {
            _tgMessageFactory = tgMessageFactory;
        }

        public ServiceBusMessage GetMessage(Update update)
        {
            var tgMessage = _tgMessageFactory.GetMessage(update);

            if (!tgMessage.IsForTextProcessing()) return default;

            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(tgMessage.ToJson());
            serviceBusMessage.SessionId = tgMessage.Chat.Id.ToString();

            return serviceBusMessage;
        }
    }
}

