using Microsoft.AspNetCore.Mvc;

namespace selemstore_test.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Products() => View();
        public IActionResult Orders() => View();
        public IActionResult Users() => View();
        public IActionResult Settings() => View();
        public IActionResult Logout()
        {
            // تسجيل الخروج
            return RedirectToAction("Index", "Home");
        }
    }
}
