using Microsoft.AspNetCore.Mvc;

namespace WEB_0535005_Vashkevich.Components
{
    public class Cart : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
