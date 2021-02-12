namespace Usain.InteractionProcessor.InteractionReactions
{
    using System;
    using Slack.Models.Interactions;

    internal sealed class InteractionReactionGenerator
        : IInteractionReactionGenerator
    {
        private readonly IInteractionReactionFactory<Interaction>
            _noopInteractionReactionFactory;

        private readonly IInteractionReactionFactory<GlobalShortcut>
            _shortcutInteractionReactionFactory;

        public InteractionReactionGenerator(
            IInteractionReactionFactory<Interaction> noopInteractionReactionFactory,
            IInteractionReactionFactory<GlobalShortcut>
                shortcutInteractionReactionFactory)
        {
            _noopInteractionReactionFactory = noopInteractionReactionFactory
                ?? throw new ArgumentNullException(
                    nameof(noopInteractionReactionFactory));
            _shortcutInteractionReactionFactory = shortcutInteractionReactionFactory
                ?? throw new ArgumentNullException(
                    nameof(shortcutInteractionReactionFactory));
        }

        public IInteractionReaction Generate(
            Interaction interaction)
        {
            if (interaction == null)
            {
                throw new InvalidOperationException("Interaction is not defined");
            }

            return interaction switch
            {
                GlobalShortcut _ => _shortcutInteractionReactionFactory.Create(interaction),
                _ => _noopInteractionReactionFactory.Create(interaction),
            };
        }
    }
}
