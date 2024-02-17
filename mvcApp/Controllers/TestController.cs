using Microsoft.AspNetCore.Mvc;

namespace mvcApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Test()
        {
            return View();
        }
    }
}
