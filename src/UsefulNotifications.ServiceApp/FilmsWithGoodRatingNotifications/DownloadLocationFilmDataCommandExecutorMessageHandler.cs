using CoreDdd.Commands;
using Rebus.Bus;
using UsefulNotifications.Commands.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications
{
    public class DownloadLocationFilmDataCommandExecutorMessageHandler 
        : CommandExecutorMessageHandler<DownloadLocationFilmDataCommand>
    {
        public DownloadLocationFilmDataCommandExecutorMessageHandler(ICommandExecutor commandExecutor, IBus bus) 
            : base(commandExecutor, bus)
        {
        }
    }
}