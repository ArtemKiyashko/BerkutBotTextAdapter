using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using BerkutTextAdapter.Extensions;
using BerkutTextAdapter.Infrastructure;
using BerkutTextAdapter.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BerkutTextAdapter
{
    public class BerkutTextAdapter
    {
        private readonly ILogger<BerkutTextAdapter> _logger;
        private readonly IServiceBusMessageFactory _serviceBusMessageFactory;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusOptions _options;

        public BerkutTextAdapter(
            ILogger<BerkutTextAdapter> log,
            IServiceBusMessageFactory serviceBusMessageFactory,
            ServiceBusClient serviceBusClient,
            IOptions<ServiceBusOptions> options)
        {
            _logger = log;
            _serviceBusMessageFactory = serviceBusMessageFactory;
            _serviceBusClient = serviceBusClient;
            _options = options.Value;
        }

        [FunctionName("BerkutTextAdapter")]
        public async Task Run([ServiceBusTrigger("alltgmessages", "textadapter", Connection = "ServiceBusOptions", IsSessionsEnabled = true)] Telegram.Bot.Types.Update tgUpdate, ILogger log)
        {
            var sbMessage = _serviceBusMessageFactory.GetMessage(tgUpdate);
            if (sbMessage is null) return;

            await using ServiceBusSender sender = _serviceBusClient.CreateSender(_options.TextMessageTopicName);
            await sender.SendMessageAsync(sbMessage);
        }
    }
}

