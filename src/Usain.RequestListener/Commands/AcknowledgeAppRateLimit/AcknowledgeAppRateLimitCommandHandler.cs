namespace Usain.RequestListener.Commands.AcknowledgeAppRateLimit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    internal class AcknowledgeAppRateLimitCommandHandler
        : ICommandHandler<AcknowledgeAppRateLimitCommand,
            CommandResult>
    {
        private readonly ILogger _logger;

        public AcknowledgeAppRateLimitCommandHandler(
            ILogger<AcknowledgeAppRateLimitCommandHandler> logger)
            => _logger =
                logger ?? throw new ArgumentNullException(nameof(logger));

        public Task<CommandResult> Handle(
            AcknowledgeAppRateLimitCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogCommandHandling(request.ToString());

            if (cancellationToken
                .IsCancellationRequested)
            {
                _logger.LogCommandCancelling(request.ToString());
                return
                    Task.FromResult(
                        new CommandResult(
                            request.Id,
                            CommandResultType.Aborted));
            }

            _logger.LogCommandHandled(request.ToString());

            return Task.FromResult(new CommandResult(request.Id));
        }
    }
}
