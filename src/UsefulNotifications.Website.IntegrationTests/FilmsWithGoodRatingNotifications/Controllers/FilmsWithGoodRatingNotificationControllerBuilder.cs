using CoreDdd.Queries;
using Microsoft.Extensions.DependencyInjection;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.Controllers
{
    public class FilmsWithGoodRatingNotificationControllerBuilder
    {
        private readonly ServiceProvider _serviceProvider;

        public FilmsWithGoodRatingNotificationControllerBuilder(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public FilmsWithGoodRatingNotificationController Build()
        {
            var queryExecutor = new QueryExecutor(_serviceProvider.GetService<IQueryHandlerFactory>());
            return new FilmsWithGoodRatingNotificationController(queryExecutor, null); // todo: pass IBusRequestSender
        }
    }
}