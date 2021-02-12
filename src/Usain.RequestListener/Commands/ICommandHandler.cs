namespace Usain.RequestListener.Commands
{
    using MediatR;

    internal interface ICommandHandler<in TCommand, TResult>
        : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult: ICommandResult
    {
    }
}
