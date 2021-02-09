namespace Usain.CommandListener.Commands
{
    using MediatR;

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
