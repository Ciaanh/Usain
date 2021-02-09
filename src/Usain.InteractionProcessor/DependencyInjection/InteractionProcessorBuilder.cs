// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public class InteractionProcessorBuilder : IInteractionProcessorBuilder
    {
        /// <summary>
        /// Gets the services
        /// </summary>
        /// <value>The services</value>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionProcessorBuilder"/> class
        /// </summary>
        /// <param name="services"></param>
        public InteractionProcessorBuilder(
            IServiceCollection services)
            => Services = services;
    }
}
