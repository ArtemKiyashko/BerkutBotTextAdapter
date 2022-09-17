using System;
using Telegram.Bot.Types;

namespace BerkutTextAdapter.Infrastructure
{
    public interface ITgMessageFactory
    {
        public Message GetMessage(Update incomingUpdate);
    }
}

