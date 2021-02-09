// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// EventListener builder Interface
    /// </summary>
    public interface IInteractionProcessorBuilder
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        IServiceCollection Services { get; }
    }
}
