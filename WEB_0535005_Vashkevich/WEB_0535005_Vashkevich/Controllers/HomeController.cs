using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_0535005_Vashkevich.Controllers
{
    public class HomeController : Controller
    {
        private List<ListDemo> _listDemo;

        public HomeController()
        {
            _listDemo = new List<ListDemo> { new ListDemo { ListItemValue = 1, ListItemText = "Item1"},
            new ListDemo {ListItemValue = 2, ListItemText = "Item2"},
            new ListDemo {ListItemValue = 3, ListItemText = "Item3"}};
        }

        public IActionResult Index()
        {
            ViewData["Lst"] = new SelectList(_listDemo, "ListItemValue", "ListItemText");
            ViewData["Text"] = "Лабораторная работа 2";
            return View();
        }
    }

    public class ListDemo
    { 
        public int ListItemValue { get; set; }
        public string ListItemText { get; set; } = string.Empty;
    }
}
