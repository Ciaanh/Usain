namespace Usain.CommandListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class EndpointHandlerLogger
    {
        private static readonly Action<ILogger, string, Exception?>
            MethodNotAllowed =
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(MethodNotAllowed)),
                    "Commands endpoint only support POST request. Was `{HttpMethod}`");

        private static readonly Action<ILogger, Exception?>
            JsonDeserializationReturnNull =
                LoggerMessage.Define(
                    LogLevel.Warning,
                    new EventId(
                        0,
                        nameof(JsonDeserializationReturnNull)),
                    "Json deserialization returned null. Unprocessable entity is returned.");

        public static void LogMethodNotAllowed(
            this ILogger logger,
            string httpMethod)
        {
            MethodNotAllowed(
                logger,
                httpMethod,
                null);
        }

        public static void LogJsonDeserializationReturnNull(
            this ILogger logger)
        {
            JsonDeserializationReturnNull(
                logger,
                null);
        }
    }
}
