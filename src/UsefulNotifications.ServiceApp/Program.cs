using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CoreDdd.Commands;
using CoreDdd.Domain.Events;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Register.Castle;
using CoreDdd.Rebus.UnitOfWork;
using CoreDdd.Register.Castle;
using CoreDdd.UnitOfWorks;
using Rebus.CastleWindsor;
using Rebus.Config;
using UsefulNotifications.Commands.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Infrastructure;
using UsefulNotifications.ServiceApp.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.ServiceApp
{
    class Program
    {
        static void Main() // todo: clean this
        {
            using (var windsorContainer = new WindsorContainer())
            {
                CoreDddNhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerRebusMessage());

                windsorContainer.Install(
                    FromAssembly.Containing<CoreDddInstaller>(),
                    FromAssembly.Containing<CoreDddNhibernateInstaller>()
                );

                windsorContainer.Register(
                    Component.For<INhibernateConfigurator>()
                        .ImplementedBy<NhibernateConfigurator>()
                        .LifeStyle.Singleton
                );

                windsorContainer.Register(
                    Component.For<ILocationFilmDataDownloadRequestRepository>()
                        .ImplementedBy<LocationFilmDataDownloadRequestRepository>()
                        .LifeStyle.Transient
                );                

                // register command handlers
                windsorContainer.Register(
                    Classes
                        .FromAssemblyContaining<DownloadLocationFilmDataCommandHandler>()
                        .BasedOn(typeof(ICommandHandler<>))
                        .WithService.FirstInterface()
                        .Configure(x => x.LifestyleTransient()));

//                // register domain event handlers
                windsorContainer.Register(
                    Classes
                        .FromAssemblyContaining<FilmDataDownloadRequestedDomainEventHandler>()
                        .BasedOn(typeof(IDomainEventHandler<>))
                        .WithService.FirstInterface()
                        .Configure(x => x.LifestyleTransient()));

                // register message handlers
                windsorContainer.AutoRegisterHandlersFromAssemblyOf<DownloadLocationFilmDataCommandExecutorMessageHandler>();

                DomainEvents.Initialize(windsorContainer.Resolve<IDomainEventHandlerFactory>());

                RebusUnitOfWork.Initialize(
                    unitOfWorkFactory: windsorContainer.Resolve<IUnitOfWorkFactory>(),
                    isolationLevel: System.Data.IsolationLevel.ReadCommitted
                );

                var rebusConfigurer = Configure.With(new CastleWindsorContainerAdapter(windsorContainer))
                    .Transport(x => x.UseRabbitMq("amqp://admin:password01@localhost", "UsefulNotifications.ServiceApp")) // todo: move this to a config file
                    .Options(o =>
                    {
                        o.EnableUnitOfWork(
                            RebusUnitOfWork.Create,
                            RebusUnitOfWork.Commit,
                            RebusUnitOfWork.Rollback,
                            RebusUnitOfWork.Cleanup
                        );
                    });
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
