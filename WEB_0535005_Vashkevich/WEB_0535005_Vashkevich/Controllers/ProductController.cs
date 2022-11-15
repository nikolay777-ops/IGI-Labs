using Microsoft.AspNetCore.Mvc;

namespace WEB_0535005_Vashkevich.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
