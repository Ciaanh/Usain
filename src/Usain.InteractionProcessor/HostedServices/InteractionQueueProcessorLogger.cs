namespace Usain.InteractionProcessor.HostedServices
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class InteractionQueueProcessorLogger
    {
        private static readonly Action<ILogger, Exception?> ProcessingQueue
            = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(
                    0,
                    nameof(ProcessingQueue)),
                "Queue processor start processing queue");

        private static readonly Action<ILogger, Exception?> ProcessedQueue
            = LoggerMessage.Define(
                LogLevel.Debug,
                new EventId(
                    0,
                    nameof(ProcessedQueue)),
                "Queue processor processed queue");

        private static readonly Action<ILogger, string, Exception?> InteractionHasBeenDequeued
            = LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(
                    0,
                    nameof(InteractionHasBeenDequeued)),
                "Queue processor has dequeued an interaction of type `{InteractionTypeName}`.");

        public static void LogProcessingQueue(
            this ILogger logger)
            => ProcessingQueue(
                logger,
                null);

        public static void LogProcessedQueue(
            this ILogger logger)
            => ProcessedQueue(
                logger,
                null);

        public static void LogInteractionHasBeenDequeued(
            this ILogger logger,
            string interactionTypeName)
            => InteractionHasBeenDequeued(
                logger,
                interactionTypeName,
                null);
    }
}
