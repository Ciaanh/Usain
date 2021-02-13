// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public class RequestListenerBuilder : IRequestListenerBuilder
    {
        /// <summary>
        /// Gets the services
        /// </summary>
        /// <value>The services</value>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestListenerBuilder"/> class
        /// </summary>
        /// <param name="services"></param>
        public RequestListenerBuilder(IServiceCollection services)
            => Services = services;
    }
}
