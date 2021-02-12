namespace Usain.RequestListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System.Threading;
    using System.Threading.Tasks;
    using Results;
    using Usain.Slack.Models.Interactions;

    internal interface IInteractionEndpointResultGenerator<in TInteraction>
        where TInteraction : Interaction
    {
        Task<IEndpointResult> GenerateResult(
            TInteraction @interaction,
            CancellationToken cancellationToken);
    }
}
