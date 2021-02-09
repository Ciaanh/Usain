namespace Usain.InteractionProcessor.HostedServices
{
    using System.Threading;
    using System.Threading.Tasks;

    internal interface IInteractionQueueProcessor
    {
        Task ProcessQueueAsync(CancellationToken cancellationToken);
    }
}
