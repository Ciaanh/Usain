namespace Usain.RequestListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System;
    using Commands;
    using Microsoft.Extensions.Logging;

    internal static class ResultGeneratorLogger
    {
        private static readonly Action<ILogger, string, Exception?>
            UnsuccessfulCommandResult =
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(UnsuccessfulCommandResult)),
                    "Unsuccessful command result `{CommandResult}` - Generating 422 Unprocessable Entity.");

        public static void LogUnsuccessfulCommandResult(
            this ILogger logger,
            CommandResult commandResult)
        {
            UnsuccessfulCommandResult(
                logger,
                commandResult.ToString(),
                null);
        }
    }
}
