// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using Extensions;
    using Options;
    using Usain.Core.Infrastructure;
    using Usain.InteractionProcessor.Configuration;
    using Usain.InteractionProcessor.HostedServices;
    using Usain.InteractionProcessor.InteractionReactions;
    using Usain.Slack.Models.Interactions;

    public static class InteractionProcessorBuilderExtensions
    {
        public static IInteractionProcessorBuilder AddEventQueue<TInteractionQueue>(
            this IInteractionProcessorBuilder builder)
            where TInteractionQueue : class, IEventQueue<GlobalShortcut>
        {
            builder.Services
                .TryAddSingleton<IEventQueue<GlobalShortcut>, TInteractionQueue>();

            return builder;
        }

        public static IInteractionProcessorBuilder AddEventQueue<TInteractionQueue>(
            this IInteractionProcessorBuilder builder,
            Func<IServiceProvider, TInteractionQueue> implementationFactory)
            where TInteractionQueue : class, IEventQueue<GlobalShortcut>
        {
            builder.Services.TryAddSingleton<IEventQueue<GlobalShortcut>>(
                implementationFactory);

            return builder;
        }

        internal static IInteractionProcessorBuilder AddPlatformServices(
            this IInteractionProcessorBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddOptions();
            builder.Services
                .AddSingleton<IConfigureOptions<InteractionProcessorOptions>,
                    InteractionProcessorOptions>();

            return builder;
        }

        // Add core services, they aren't substitutable.
        internal static IInteractionProcessorBuilder AddCoreServices(
            this IInteractionProcessorBuilder builder)
        {
            builder.Services
                .AddTransient<IInteractionReactionGenerator, InteractionReactionGenerator>();
            builder.Services
                .AddTransient<IInteractionQueueProcessor, InteractionQueueProcessor>();
            builder.Services.AddHostedService<InteractionProcessorService>();
            return builder;
        }

        // Add pluggable services, they are substitutable.
        internal static IInteractionProcessorBuilder AddPluggableServices(
            this IInteractionProcessorBuilder builder)
        {
            // IInteractionReactionFactory is the interface you will implement for registering your own custom InteractionReaction.
            builder.Services
                 .TryAddEnumerable(ServiceDescriptor.Transient(typeof(IInteractionReactionFactory<>), typeof(DefaultInteractionReactionFactory<>)));
            return builder;
        }
    }
}
