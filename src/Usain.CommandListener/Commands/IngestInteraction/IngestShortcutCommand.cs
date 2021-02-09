namespace Usain.CommandListener.Commands.IngestInteraction
{
    using Slack.Models.Interactions;

    internal class IngestShortcutCommand
        : Command<CommandResult>
    {
        public GlobalShortcut Shortcut { get; }

        public IngestShortcutCommand(
            GlobalShortcut @interaction)
            => Shortcut = @interaction;
    }
}
