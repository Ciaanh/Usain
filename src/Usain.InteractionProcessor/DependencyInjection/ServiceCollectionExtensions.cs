// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IInteractionProcessorBuilder AddUsainInteractionProcessor(
            this IServiceCollection services)
        {
            var builder = services.AddUsainInteractionProcessorBuilder();

            builder
                .AddPlatformServices()
                .AddCoreServices()
                .AddPluggableServices();

            return builder;
        }

        public static IInteractionProcessorBuilder AddUsainInteractionProcessorBuilder(
            this IServiceCollection services)
            => new InteractionProcessorBuilder(services);
    }
}
