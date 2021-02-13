namespace Usain.InteractionProcessor.InteractionReactions
{
    using System;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Interactions;

    public class DefaultInteractionReactionFactory<TInteraction>
        : IInteractionReactionFactory<TInteraction>
        where TInteraction : Interaction, new()
    {
        private readonly ILogger<NoopInteractionReaction<TInteraction>> _logger;

        public DefaultInteractionReactionFactory(ILoggerFactory loggerFactory)
            => _logger = loggerFactory
                    .CreateLogger<NoopInteractionReaction<TInteraction>>()
                ?? throw new ArgumentNullException(nameof(loggerFactory));

        public IInteractionReaction<TInteraction> Create(Interaction interaction)
            => new NoopInteractionReaction<TInteraction>(
                _logger,
                interaction);
    }
}
