namespace Usain.InteractionProcessor.InteractionReactions
{
    using System.Threading.Tasks;

    public interface IInteractionReaction
    {
        Task ReactAsync();
    }

    public interface IInteractionReaction<out TInteraction> : IInteractionReaction
    {
        public TInteraction Interaction { get; }
    }
}
