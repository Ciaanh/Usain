namespace Usain.InteractionProcessor.InteractionReactions
{
    using System;
    using Microsoft.Extensions.Logging;

    internal static class LoggerMessageExtensions
    {
        private static readonly Action<ILogger, string, Exception?> NoReaction =
            LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(NoReaction)),
                "Noop interaction reaction for interaction of type `{InteractionType}`");

        public static void LogNoReaction(
            this ILogger logger,
            string? eventType = null)
        {
            NoReaction(
                logger,
                eventType ?? "unknown type",
                null);
        }
    }
}
