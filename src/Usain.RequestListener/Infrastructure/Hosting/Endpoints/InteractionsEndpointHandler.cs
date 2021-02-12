namespace Usain.RequestListener.Infrastructure.Hosting.Endpoints
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Results;
    using Usain.RequestListener.Infrastructure.Hosting.Endpoints.ResultGenerators;
    using Usain.Slack.Models.Interactions;

    internal class InteractionsEndpointHandler : IEndpointHandler
    {
        private readonly ILogger _logger;
        private readonly IInteractionEndpointResultGenerator<Interaction> _interactionResultGenerator;

        public const string ProtocolRoutePath = "/interactions";
        public const string EndpointName = "InteractionsApi";

        public InteractionsEndpointHandler(
            ILogger<InteractionsEndpointHandler> logger,
            IInteractionEndpointResultGenerator<Interaction> shortcutInteractionResultGenerator)
        {
            _logger = logger;
            _interactionResultGenerator =
                shortcutInteractionResultGenerator
                ?? throw new ArgumentNullException(
                    nameof(shortcutInteractionResultGenerator));
        }

        public async Task<IEndpointResult> ProcessAsync(
            HttpContext context,
            CancellationToken cancellationToken)
        {
            _logger.LogProcessingInteraction();

            if (!HttpMethods.IsPost(context.Request.Method))
            {
                _logger.LogMethodNotAllowed(context.Request.Method);
                return new StatusCodeEndpointResult(
                    StatusCodes
                        .Status405MethodNotAllowed);
            }

            var incomingInteraction =
                await context.Request.ReadJsonAsync<Interaction>()!;
            if (incomingInteraction == null)
            {
                _logger.LogJsonDeserializationReturnNull();
                return new StatusCodeEndpointResult(
                    StatusCodes.Status422UnprocessableEntity);
            }

            _logger.LogProcessingEventOfType(incomingInteraction.InteractionType);

            return incomingInteraction switch
            {
                GlobalShortcut @interaction =>
                await _interactionResultGenerator.GenerateResult(
                    @interaction,
                    cancellationToken),
                _ => new StatusCodeEndpointResult(
                    StatusCodes.Status422UnprocessableEntity),
            };
        }
    }
}
