namespace Usain.InteractionProcessor.InteractionReactions
{
    using Slack.Models.Interactions;

    public interface IInteractionReactionFactory<out TInteraction>
        where TInteraction : class, new()
    {
        IInteractionReaction<TInteraction> Create(
            Interaction interaction);
    }
}
