using WEB_0535005_Vashkevich.Models;
using WEB_0535005_Vashkevich.Entities;
using WEB_0535005_Vashkevich.Extensions;
using Newtonsoft.Json;

namespace WEB_0535005_Vashkevich.Services
{
    public class CartService: Cart
    {
        private string sessionKey = "cart";

        [JsonIgnore]
        ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider sp) 
        {
            var session = sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

            var cart = session?.Get<CartService>("cart")
                       ?? new CartService();
            cart.Session = session;
            return cart;
        }

        public override void AddToCart(Album album)
        {
            base.AddToCart(album);
            Session?.Set<CartService>(sessionKey, this);
        }

        public override void RemoveOneFromCart(Album album)
        {
            base.RemoveOneFromCart(album);
            Session.Set<CartService>(sessionKey, this);
        }
        public override void RemoveFromCart(int id)
        {
            base.RemoveFromCart(id);
            Session?.Set<CartService>(sessionKey, this);
        }
        public override void ClearAll()
        {
            base.ClearAll();
            Session?.Set<CartService>(sessionKey, this);
        }
    }
}
