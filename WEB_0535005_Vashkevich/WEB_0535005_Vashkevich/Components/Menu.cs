using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using WEB_0535005_Vashkevich.Models;
using System.Security.Policy;

namespace WEB_0535005_Vashkevich.Components
{
    public class Menu: ViewComponent
    {
        private List<MenuItem> _menuItems = new List<MenuItem>() {
            new MenuItem{Controller="Home", Action="Index", Text="Lab 3" },
            new MenuItem{Controller="Product",Action ="Index", Text="Каталог"},
            new MenuItem{IsPage=true, Area="Admin", Page="/Index", Text="Администрирование"}
        };

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var controller = ViewContext.RouteData.Values["controller"];
            var area = ViewContext.RouteData.Values["area"];
            foreach (var item in _menuItems)
            {
                if (controller != null && controller.Equals(item.Controller)) {
                    item.Active = "active";
                    break;
                }
                else if (area != null && area.Equals(item.Area))
                {
                    item.Active = "active";

                }
                else
                {
                    item.Active = "";
                }
            }
            return View(_menuItems);
        }
    }
}
