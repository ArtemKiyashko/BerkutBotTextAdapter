using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace BerkutTextAdapter.Extensions
{
    public static class TelegramMessageExtensions
    {
        public static bool IsForTextProcessing(this Message tgMessage)
            => tgMessage is not null && tgMessage.Type == Telegram.Bot.Types.Enums.MessageType.Text;
    }
}

