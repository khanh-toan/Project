using Microsoft.AspNetCore.Mvc;

namespace client.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController() { }
        public IActionResult Index()
        {
            return View("Login");
        }
    }
}
