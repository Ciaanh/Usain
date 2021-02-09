namespace Usain.CommandListener.Commands
{
    using System;

    internal interface ICommandResult
    {
        Guid CommandId { get; }
    }
}
