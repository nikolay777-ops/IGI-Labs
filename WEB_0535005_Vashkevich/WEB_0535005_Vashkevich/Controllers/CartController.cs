using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_0535005_Vashkevich.Areas.Identity.Data;
using WEB_0535005_Vashkevich.Models;
using WEB_0535005_Vashkevich.Extensions;

namespace WEB_0535005_Vashkevich.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart) 
        {
            _context = context;
            _cart = cart;
        }

        [Route("Cart")]
        public IActionResult Index()
        {
            return View(_cart.Items.Values);
        }

        [Authorize]
        public IActionResult Add(int id, string returnUrl)
        {
            var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            var album = _context.Albums.Find(id);
            if (album != null)
            {
                cart.AddToCart(album);
                HttpContext.Session.Set<Cart>("cart", cart);
            }
            return Redirect(returnUrl);
        }

        [Authorize]
        public IActionResult RemoveOne(int id, string returnUrl) 
        {
            var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            var album = _context.Albums.Find(id);

            if (album != null)
            {
                cart.RemoveOneFromCart(album);
                HttpContext.Session.Set<Cart>("cart", cart);
            }
            return Redirect(returnUrl);
        }

        [Authorize]
        public IActionResult Delete(int id) 
        {
            _cart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult ClearAll(int id)
        {
            _cart.ClearAll();
            return RedirectToAction("Index");
        }

        public int Count() => _cart.Count;

        public float Price() => _cart.Cash;
    }
}
