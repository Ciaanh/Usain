namespace Usain.CommandListener.Commands.IngestEvent
{
    using Slack.Models.Events;

    internal class IngestEventCommand
        : Command<CommandResult>
    {
        public EventWrapper Event { get; }

        public IngestEventCommand(
            EventWrapper @event)
            => Event = @event;
    }
}
