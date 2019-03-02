using System.Threading.Tasks;
using CoreDdd.Commands;
using Rebus.Bus;
using Rebus.Handlers;
using UsefulNotifications.Commands;

namespace UsefulNotifications.ServiceApp
{
    public abstract class CommandExecutorMessageHandler<TCommand> : IHandleMessages<TCommand>
        where TCommand: ICommand
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IBus _bus;

        protected CommandExecutorMessageHandler(
            ICommandExecutor commandExecutor,
            IBus bus
        )
        {
            _bus = bus;
            _commandExecutor = commandExecutor;
        }

        public async Task Handle(TCommand command)
        {
            object commandExecutedArgs = null;
            _commandExecutor.CommandExecuted += args => commandExecutedArgs = args.Args;
            await _commandExecutor.ExecuteAsync(command);

            await _bus.Reply(new CommandExecutedReply { CommandExecutedArgs = commandExecutedArgs });
        }
    }
}