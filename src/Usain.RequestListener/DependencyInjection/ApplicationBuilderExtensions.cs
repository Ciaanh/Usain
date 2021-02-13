// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Diagnostics;
    using AspNetCore.Builder;
    using Logging;
    using Options;
    using Usain.RequestListener.Configuration;
    using Usain.RequestListener.Infrastructure.Hosting.Middlewares;

    public static class ApplicationBuilderExtensions
    {
        private static string ILoggerFactoryName = nameof(ILoggerFactory);
        private static string IServiceScopeFactoryName = nameof(IServiceScopeFactory);

        public static IApplicationBuilder UseUsainRequestListener(
            this IApplicationBuilder app)
        {
            app.Validate();
            app.UseMiddleware<RequestAuthenticationMiddleware>();
            app.UseMiddleware<RequestListenerMiddleware>();
            return app;
        }

        internal static void Validate(
            this IApplicationBuilder app)
        {
            var loggerFactory =
                app.ApplicationServices.GetService(typeof(ILoggerFactory)) as
                    ILoggerFactory
                ?? throw new ArgumentNullException(ILoggerFactoryName);

            var logger = loggerFactory.CreateLogger("UsainServer.Startup");
            logger.LogInformation(
                "Starting UsainServer version {version}",
                FileVersionInfo
                    .GetVersionInfo(
                        typeof(RequestListenerMiddleware)
                            .Assembly.Location)
                    .ProductVersion);

            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>()
                ?? throw new ArgumentNullException(IServiceScopeFactoryName);

            using var scope = scopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var optionsMonitor = serviceProvider
                .GetRequiredService<IOptions<RequestListenerOptions>>();
            ValidateOptions(optionsMonitor);
        }

        internal static void ValidateOptions(
            IOptions<RequestListenerOptions> serverOptions)
        {
            var options = serverOptions.Value;
            if (options == null)
            {
                throw new InvalidOperationException(
                    "Unable to read options");
            }

            if (options.IsRequestAuthenticationEnabled
                && string.IsNullOrEmpty(options.SigningKey))
            {
                throw new InvalidOperationException(
                    $"Configure {nameof(options.SigningKey)} in UsainServer options or deactivate Request Authentication");
            }
        }
    }
}
