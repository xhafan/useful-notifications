using Microsoft.AspNetCore.Mvc;

namespace UsefulNotifications.Website.Controllers
{
    public class FilmsWithGoodRatingNotificationController : Controller
    {
        public IActionResult Index() // todo: test me
        {
            return View();
        }

        public IActionResult Subscribe() // todo: test me
        {
            return RedirectToAction("SubscriptionSuccessful");

        }

        public IActionResult SubscriptionSuccessful() // todo: test me
        {
            return View();
        }
    }
}