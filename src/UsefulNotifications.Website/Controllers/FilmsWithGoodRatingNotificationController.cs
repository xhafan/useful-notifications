using Microsoft.AspNetCore.Mvc;

namespace UsefulNotifications.Website.Controllers
{
    public class FilmsWithGoodRatingNotificationController : Controller
    {
        public IActionResult Index() // todo: test me
        {
            return View();
        }
    }
}