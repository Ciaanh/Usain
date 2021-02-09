namespace Usain.InteractionProcessor.InteractionReactions
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Interactions;

    internal class NoopInteractionReaction<TInteraction>
        : IInteractionReaction<
            TInteraction>
        where TInteraction : Interaction, new()
    {
        private readonly ILogger _logger;
        private readonly Interaction _interaction;

        public TInteraction Interaction { get; }

        public NoopInteractionReaction(
            ILogger<NoopInteractionReaction<TInteraction>> logger,
            Interaction interaction)
        {
            _logger = logger;
            _interaction = interaction;
            Interaction = interaction as TInteraction ?? new TInteraction();
        }

        public Task ReactAsync()
        {
            string eventType = _interaction.InteractionType ?? "unknown_type";
            _logger.LogNoReaction(eventType);
            return Task.CompletedTask;
        }
    }
}
