namespace Usain.RequestListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class EventsEndpointHandlerLogger
    {
        private static readonly Action<ILogger, Exception?> ProcessingEvent =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(ProcessingEvent)),
                "Processing event request");

        private static readonly Action<ILogger, string?, Exception?>
            ProcessingEventOfType = LoggerMessage.Define<string?>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(ProcessingEventOfType)),
                "Processing event of type `{EventType}`");

        public static void LogProcessingEvent(
            this ILogger logger)
        {
            ProcessingEvent(
                logger,
                null);
        }

        public static void LogProcessingEventOfType(
            this ILogger logger,
            string? type)
        {
            ProcessingEventOfType(
                logger,
                type,
                null);
        }
    }
}
