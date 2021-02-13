namespace Usain.InteractionProcessor.InteractionReactions
{
    using Slack.Models.Interactions;

    internal interface IInteractionReactionGenerator
    {
        IInteractionReaction Generate(Interaction interaction);
    }
}
