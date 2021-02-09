namespace Usain.CommandListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System;
    using Microsoft.Extensions.Logging;

    internal static class EventResultGeneratorLogger
    {
        private static readonly Action<ILogger, Exception?>
            UrlVerificationEventMissingChallenge =
                LoggerMessage.Define(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(UrlVerificationEventMissingChallenge)),
                    "UrlVerification event has no challenge. Generating 400 Bad request.");

        public static void LogUrlVerificationEventMissingChallenge(
            this ILogger logger)
        {
            UrlVerificationEventMissingChallenge(
                logger,
                null);
        }
    }
}
