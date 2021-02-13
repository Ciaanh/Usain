// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using AspNetCore.Http;
    using Extensions;
    using MediatR;
    using Options;
    using Usain.Core.Infrastructure;
    using Usain.RequestListener.Commands;
    using Usain.RequestListener.Configuration;
    using Usain.RequestListener.Infrastructure.Hosting.Endpoints;
    using Usain.RequestListener.Infrastructure.Hosting.Endpoints.ResultGenerators;
    using Usain.RequestListener.Infrastructure.Hosting.Middlewares;
    using Usain.RequestListener.Infrastructure.Security;
    using Usain.Slack.Models.Events;
    using Usain.Slack.Models.Interactions;
    using Usain.Slack.Security;
    using Endpoint =
        Usain.RequestListener.Infrastructure.Hosting.Endpoints.Endpoint;

    public static class RequestListenerBuilderExtensions
    {
        public static IRequestListenerBuilder AddEventQueue<TEventQueue>(
            this IRequestListenerBuilder builder)
            where TEventQueue : class, IRequestQueue<EventWrapper>
        {
            builder.Services
                .TryAddSingleton<IRequestQueue<EventWrapper>, TEventQueue>();

            return builder;
        }

        public static IRequestListenerBuilder AddEventQueue<TEventQueue>(
            this IRequestListenerBuilder builder,
            Func<IServiceProvider, TEventQueue> implementationFactory)
            where TEventQueue : class, IRequestQueue<EventWrapper>
        {
            builder.Services.TryAddSingleton<IRequestQueue<EventWrapper>>(
                implementationFactory);

            return builder;
        }

        public static IRequestListenerBuilder AddInteractionQueue<TEventQueue>(
                    this IRequestListenerBuilder builder)
                    where TEventQueue : class, IRequestQueue<Interaction>
        {
            builder.Services
                .TryAddSingleton<IRequestQueue<Interaction>, TEventQueue>();

            return builder;
        }

        public static IRequestListenerBuilder AddInteractionQueue<TEventQueue>(
            this IRequestListenerBuilder builder,
            Func<IServiceProvider, TEventQueue> implementationFactory)
            where TEventQueue : class, IRequestQueue<Interaction>
        {
            builder.Services.TryAddSingleton<IRequestQueue<Interaction>>(
                implementationFactory);

            return builder;
        }

        internal static IRequestListenerBuilder AddPlatformServices(
            this IRequestListenerBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddOptions();
            builder.Services
                .AddSingleton<IConfigureOptions<RequestListenerOptions>,
                    RequestListenerOptions>();
            builder.Services.AddMediatR(typeof(ICommandResult));

            return builder;
        }

        internal static IRequestListenerBuilder AddCoreServices(
            this IRequestListenerBuilder builder)
        {
            // Add core services, they aren't substitutable.
            builder.Services
                .AddSingleton<IRequestAuthenticator, RequestAuthenticator>();

            builder.Services
                .AddTransient<
                    IEventEndpointResultGenerator<UrlVerificationEvent>,
                    UrlVerificationEventResultGenerator>();

            builder.Services
                .AddTransient<
                    IEventEndpointResultGenerator<AppRateLimitedEvent>,
                    AppRateLimitedEventResultGenerator>();

            builder.Services
                .AddTransient<
                    IEventEndpointResultGenerator<EventWrapper>,
                    CallbackEventResultGenerator>();

            builder.Services
                .AddTransient<
                    IInteractionEndpointResultGenerator<Interaction>,
                    InteractionResultGenerator>();

            builder.Services.AddTransient<IEndpointRouter, EndpointRouter>();
            builder.Services.AddTransient(CreateSignatureVerifier);
            builder.Services.AddScoped<RequestAuthenticationMiddleware>();
            builder.Services.AddScoped<RequestListenerMiddleware>();

            return builder;
        }

        internal static IRequestListenerBuilder AddDefaultEndpoints(
            this IRequestListenerBuilder builder)
        {
            builder.AddEndpoint<EventsEndpointHandler>(
                EventsEndpointHandler.EndpointName,
                EventsEndpointHandler.ProtocolRoutePath);

            builder.AddEndpoint<InteractionsEndpointHandler>(
                InteractionsEndpointHandler.EndpointName,
                InteractionsEndpointHandler.ProtocolRoutePath);

            return builder;
        }

        private static IRequestListenerBuilder AddEndpoint<TEndpointHandler>(
            this IRequestListenerBuilder builder,
            string name,
            PathString path)
            where TEndpointHandler : class, IEndpointHandler
        {
            builder.Services.AddTransient<TEndpointHandler>();
            builder.Services.AddSingleton(
                new Endpoint(
                    name,
                    path,
                    typeof(TEndpointHandler)));

            return builder;
        }

        private static ISignatureVerifier CreateSignatureVerifier(
            IServiceProvider serviceProvider)
        {
            var optionsMonitor =
                serviceProvider.GetRequiredService<
                    IOptionsMonitor<RequestListenerOptions>>();
            var options = optionsMonitor.CurrentValue;
            return new SignatureVerifier(
                options.SigningKey,
                TimeSpan.FromSeconds(options.DeltaTimeToleranceSeconds));
        }
    }
}
