using System.Threading.Tasks;
using CoreDdd.Nhibernate.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using UsefulNotifications.Website.Controllers.FilmsWithGoodRatingNotifications;

namespace UsefulNotifications.Website.IntegrationTests.FilmsWithGoodRatingNotifications.Controllers.TriggeringLocationDataDownloads
{
    [TestFixture(TypeArgs = new[] { typeof (TriggeringLocationDataDownloadWithImdbRatingSourceSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof (TriggeringLocationDataDownloadWithCsfdRatingSourceSpecification) })]
    public class when_triggering_location_data_download_with_no_location_data<TTriggeringLocationDataDownloadSpecification>
        where TTriggeringLocationDataDownloadSpecification : ITriggeringLocationDataDownloadSpecification, new()
    {
        private NhibernateUnitOfWork _unitOfWork;
        private ServiceProvider _serviceProvider;
        private IServiceScope _serviceScope;

        private IActionResult _actionResult;

        private SearchForFilmsArgs _searchForFilmsArgs;

        [SetUp]
        public async Task Context()
        {
            var specification = new TTriggeringLocationDataDownloadSpecification();

            _serviceProvider = new ServiceProviderHelper().BuildServiceProvider();

            _serviceScope = _serviceProvider.CreateScope();

            _unitOfWork = _serviceProvider.GetService<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction();

            _unitOfWork.Clear();

            var controller = new FilmsWithGoodRatingNotificationControllerBuilder(_serviceProvider).Build();

            _searchForFilmsArgs = specification.GetSearchForFilmsArgs();

            _actionResult = await controller.TriggerLocationFilmDataDownload(_searchForFilmsArgs);
        }

        [Test]
        public void location_data_download_is_triggered()
        {
            
        }

        [Test]
        public void request_is_redirected_to_wait_for_location_data_download_view()
        {
            _actionResult.ShouldBeOfType<RedirectToActionResult>();
            var redirectToActionResult = (RedirectToActionResult)_actionResult;
            redirectToActionResult.ControllerName.ShouldBeNull();
            redirectToActionResult.ActionName.ShouldBe("WaitForLocationDataDownload");
            redirectToActionResult.RouteValues.ShouldNotBeNull();
            redirectToActionResult.RouteValues.ContainsKey("searchForFilmsArgs").ShouldBeTrue();
            ((SearchForFilmsArgs)redirectToActionResult.RouteValues["searchForFilmsArgs"]).ShouldBe(_searchForFilmsArgs);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
            _serviceScope.Dispose();
            _serviceProvider.Dispose();
        }
    }
}