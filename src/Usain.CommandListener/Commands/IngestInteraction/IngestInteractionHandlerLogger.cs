namespace Usain.CommandListener.Commands.IngestInteraction
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class IngestInteractionCommandHandlerLogger
    {
        private static readonly Action<ILogger, string?, Exception?> IngestingInteractionOfType
            = LoggerMessage.Define<string?>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(IngestingInteractionOfType)),
                "Ingesting interaction of type `{InteractionType}`");

        public static void LogIngestingInteractionOfType(
            this ILogger logger,
            string? interactionType)
        {
            IngestingInteractionOfType(
                logger,
                interactionType,
                null);
        }
    }
}
