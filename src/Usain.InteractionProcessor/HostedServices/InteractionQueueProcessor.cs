namespace Usain.InteractionProcessor.HostedServices
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using InteractionReactions;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Interactions;

    internal class InteractionQueueProcessor : IInteractionQueueProcessor
    {
        private readonly ILogger _logger;
        private readonly IEventQueue<Interaction> _interactionQueue;
        private readonly IInteractionReactionGenerator _interactionReactionGenerator;

        public InteractionQueueProcessor(
            ILogger<InteractionQueueProcessor> logger,
            IEventQueue<Interaction> interactionQueue,
            IInteractionReactionGenerator interactionReactionGenerator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _interactionQueue = interactionQueue
                ?? throw new ArgumentNullException(nameof(interactionQueue));
            _interactionReactionGenerator = interactionReactionGenerator
                ?? throw new ArgumentNullException(
                    nameof(interactionReactionGenerator));
        }

        public async Task ProcessQueueAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogProcessingQueue();
            var @interaction = await _interactionQueue.DequeueAsync(cancellationToken);
            if (@interaction != null)
            {
                _logger.LogInteractionHasBeenDequeued(@interaction.InteractionType);
                await ReactToInteractionAsync(@interaction);
            }

            _logger.LogProcessedQueue();
        }

        private async Task ReactToInteractionAsync(Interaction? shortcut)
        {
            if (shortcut == null) { return; }

            var reaction = _interactionReactionGenerator.Generate(shortcut);
            await reaction.ReactAsync();
        }
    }
}
