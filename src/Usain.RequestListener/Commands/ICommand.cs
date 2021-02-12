namespace Usain.RequestListener.Commands
{
    using MediatR;

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
