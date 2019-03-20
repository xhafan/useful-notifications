using System;
using Castle.Windsor;
using Rebus.CastleWindsor;
using Rebus.Config;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.DownloadApp.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.DownloadApp
{
    class Program
    {
        static void Main()
        {
            using (var windsorContainer = new WindsorContainer())
            {
                // register message handlers
                windsorContainer.AutoRegisterHandlersFromAssemblyOf<FilmDataDownloadRequestedDomainEventMessageHandler>();

                var rebusConfigurer = Configure.With(new CastleWindsorContainerAdapter(windsorContainer))
                    .Transport(x => x.UseRabbitMq("amqp://admin:password01@localhost", "UsefulNotifications.DownloadApp") // todo: move this to a config file
                    );
                using (var bus = rebusConfigurer.Start())
                {
                    bus.Subscribe<FilmDataDownloadRequestedDomainEvent>().Wait();

                    Console.WriteLine("Press enter to quit");
                    Console.ReadLine();
                }
            }
        }
    }
}
