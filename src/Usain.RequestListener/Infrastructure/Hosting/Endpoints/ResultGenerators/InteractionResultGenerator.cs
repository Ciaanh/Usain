namespace Usain.RequestListener.Infrastructure.Hosting.Endpoints.ResultGenerators
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Results;
    using Usain.RequestListener.Commands.IngestInteraction;
    using Usain.Slack.Models.Interactions;

    internal class InteractionResultGenerator
        : IInteractionEndpointResultGenerator<Interaction>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public InteractionResultGenerator(
            ILogger<InteractionResultGenerator> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IEndpointResult> GenerateResult(
            Interaction interaction,
            CancellationToken cancellationToken)
        {
            var commandResult =
                await _mediator.Send(
                    new IngestInteractionCommand(interaction),
                    cancellationToken);
            if (!commandResult.IsSuccess)
            {
                _logger.LogUnsuccessfulCommandResult(commandResult);
                return new StatusCodeEndpointResult(
                    StatusCodes.Status422UnprocessableEntity);
            }

            return new OkEndpointResult();
        }
    }
}
