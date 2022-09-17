using System;
using Azure.Messaging.ServiceBus;
using Telegram.Bot.Types;

namespace BerkutTextAdapter.Infrastructure
{
    public interface IServiceBusMessageFactory
    {
        public ServiceBusMessage GetMessage(Update update);
    }
}

