using Microsoft.AspNetCore.Mvc;
using WEB_0535005_Vashkevich.Models;

namespace WEB_0535005_Vashkevich.Components
{
    public class CartViewComponent : ViewComponent
    {
        private Cart _cart;

        public CartViewComponent(Cart cart) 
        {
            _cart = cart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_cart);
        }
    }
}
