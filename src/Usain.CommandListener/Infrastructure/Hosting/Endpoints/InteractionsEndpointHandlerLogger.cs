namespace Usain.CommandListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class InteractionsEndpointHandlerLogger
    {
        private static readonly Action<ILogger, Exception?> ProcessingInteraction =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(ProcessingInteraction)),
                "Processing interaction request");

        private static readonly Action<ILogger, string?, Exception?>
            ProcessingInteractionOfType = LoggerMessage.Define<string?>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(ProcessingInteractionOfType)),
                "Processing Interaction of type `{InteractionType}`");

        public static void LogProcessingInteraction(
            this ILogger logger)
        {
            ProcessingInteraction(
                logger,
                null);
        }

        public static void LogProcessingInteractionOfType(
            this ILogger logger,
            string? type)
        {
            ProcessingInteractionOfType(
                logger,
                type,
                null);
        }
    }
}
