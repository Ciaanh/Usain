namespace Usain.RequestListener.Commands.IngestInteraction
{
    using Slack.Models.Interactions;

    internal class IngestInteractionCommand
        : Command<CommandResult>
    {
        public Interaction Interaction { get; }

        public IngestInteractionCommand(
            Interaction @interaction)
            => Interaction = @interaction;
    }
}
