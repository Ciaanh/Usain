namespace Usain.RequestListener.Commands
{
    using System;

    internal interface ICommandResult
    {
        Guid CommandId { get; }
    }
}
