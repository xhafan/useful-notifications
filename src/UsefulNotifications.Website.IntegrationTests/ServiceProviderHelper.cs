using CoreDdd.Nhibernate.Register.DependencyInjection;
using CoreDdd.Queries;
using CoreDdd.Register.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UsefulNotifications.Infrastructure;
using UsefulNotifications.Queries.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests
{
    public class ServiceProviderHelper
    {
        public ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddCoreDdd();
            services.AddCoreDddNhibernate<NhibernateConfigurator>();

            // register command handlers, query handlers
            services.Scan(scan => scan

                .FromAssemblyOf<GetFilmsQuery>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()

            );
            return services.BuildServiceProvider();
        }
    }
}