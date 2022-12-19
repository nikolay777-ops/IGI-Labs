using WEB_0535005_Vashkevich.Entities;

namespace WEB_0535005_Vashkevich.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }

        public Cart() 
        {
            Items = new Dictionary<int, CartItem>();
        }

        public int Count 
        {
            get { return Items.Sum(item => item.Value.Quantity); }
        }

        public float Cash
        {
            get 
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Album.Price);
            }
        }

        public virtual void AddToCart(Album album) 
        {
            if (Items.ContainsKey(album.Id))
            {
                Items[album.Id].Quantity++;
            }
            else 
            {
                Items.Add(album.Id, new CartItem { 
                    Album = album,
                    Quantity = 1
                });
            }
        }

        public virtual void RemoveOneFromCart(Album album) 
        {
            if (Items.ContainsKey(album.Id))
            {
                int count = Items[album.Id].Quantity;
                if (count > 1)
                {
                    Items[album.Id].Quantity--;
                }
                else 
                {
                    Items.Remove(album.Id);
                }
            }
        }

        public virtual void RemoveFromCart(int id) 
        {
            Items.Remove(id);
        }

        public virtual void ClearAll() 
        {
            Items.Clear();
        }
    }

    public class CartItem 
    { 
        public Album Album { get; set;}
        public int Quantity { get; set; }
    }
}
