namespace Usain.CommandListener.Commands.IngestInteraction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Slack.Models.Interactions;

    internal class IngestShortcutCommandHandler
        : ICommandHandler<IngestShortcutCommand, CommandResult>
    {
        private readonly ILogger _logger;
        private readonly IEventQueue<GlobalShortcut> _shortcutQueue;

        public IngestShortcutCommandHandler(
            ILogger<IngestShortcutCommandHandler> logger,
            IEventQueue<GlobalShortcut> shortcutQueue)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _shortcutQueue = shortcutQueue
                ?? throw new ArgumentNullException(nameof(shortcutQueue));
        }

        public async Task<CommandResult> Handle(
            IngestShortcutCommand request,
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

            var @interaction = request.Shortcut;
            _logger.LogIngestingInteractionOfType(@interaction.InteractionType);
            try
            {
                await _shortcutQueue.EnqueueAsync(
                    @interaction,
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
