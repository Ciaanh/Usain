// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using Usain.RequestListener.Configuration;

    /// <summary>
    /// DI extension methods for adding Usain
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IRequestListenerBuilder AddUsainRequestListener(
            this IServiceCollection services)
        {
            var builder = services.AddUsainRequestListenerBuilder();

            builder
                .AddPlatformServices()
                .AddCoreServices()
                .AddDefaultEndpoints();

            return builder;
        }

        public static IRequestListenerBuilder AddUsainRequestListener(
            this IServiceCollection services,
            Action<RequestListenerOptions> configureOptions)
        {
            services.Configure(configureOptions);
            return services.AddUsainRequestListener();
        }

        public static IRequestListenerBuilder AddUsainRequestListenerBuilder(
            this IServiceCollection services)
            => new RequestListenerBuilder(services);
    }
}
