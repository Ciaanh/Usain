namespace Usain.CommandListener.Commands.VerifyUrl
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    internal class VerifyUrlCommandHandler
        : ICommandHandler<VerifyUrlCommand, VerifyUrlCommandResult>
    {
        private readonly ILogger<VerifyUrlCommandHandler> _logger;

        public VerifyUrlCommandHandler(
            ILogger<VerifyUrlCommandHandler> logger)
            => _logger = logger;

        public Task<VerifyUrlCommandResult> Handle(
            VerifyUrlCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogCommandHandling(request.ToString());

            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogCommandCancelling(request.ToString());
                return Task.FromResult(
                    new VerifyUrlCommandResult(
                        request.Challenge,
                        request.Id,
                        CommandResultType.Aborted));
            }

            var commandResult = string.IsNullOrEmpty(request.Challenge)
                ? new VerifyUrlCommandResult(
                    string.Empty,
                    request.Id,
                    CommandResultType.Failure)
                : new VerifyUrlCommandResult(request.Challenge, request.Id);

            _logger.LogCommandHandled(request.ToString());

            return Task.FromResult(commandResult);
        }
    }
}
