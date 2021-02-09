namespace Usain.CommandListener.Commands.IngestEvent
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Events;

    internal class IngestEventCommandHandler
        : ICommandHandler<IngestEventCommand, CommandResult>
    {
        private readonly ILogger _logger;
        private readonly IEventQueue<EventWrapper> _eventQueue;

        public IngestEventCommandHandler(
            ILogger<IngestEventCommandHandler> logger,
            IEventQueue<EventWrapper> eventQueue)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventQueue = eventQueue
                ?? throw new ArgumentNullException(nameof(eventQueue));
        }

        public async Task<CommandResult> Handle(
            IngestEventCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogCommandHandling(request.ToString());
            if (cancellationToken
                .IsCancellationRequested)
            {
                _logger.LogCommandCancelling(request.ToString());
                return new CommandResult(
                    request.Id,
                    CommandResultType.Aborted);
            }

            var @event = request.Event;
            _logger.LogIngestingEventOfType(@event.EventType);
            try
            {
                await _eventQueue.EnqueueAsync(
                    @event,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCommandFailed(
                    request.ToString(),
                    ex);
                return new CommandResult(
                    request.Id,
                    CommandResultType.Failure);
            }

            _logger.LogCommandHandled(request.ToString());
            return new CommandResult(request.Id);
        }
    }
}
