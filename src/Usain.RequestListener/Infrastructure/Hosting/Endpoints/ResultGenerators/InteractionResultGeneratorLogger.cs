namespace Usain.RequestListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System;
    using Microsoft.Extensions.Logging;

    internal static class InteractionResultGeneratorLogger
    {
        private static readonly Action<ILogger, Exception?>
            UrlVerificationInteractionMissingChallenge =
                LoggerMessage.Define(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(UrlVerificationInteractionMissingChallenge)),
                    "UrlVerification event has no challenge. Generating 400 Bad request.");

        public static void LogUrlVerificationInteractionMissingChallenge(
            this ILogger logger)
        {
            UrlVerificationInteractionMissingChallenge(
                logger,
                null);
        }
    }
}
