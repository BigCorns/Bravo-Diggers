using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamBravo.Shared;

namespace TeamBravo.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(Product product);
        Task<List<CartItem>> GetCartItems();

        Task DeleteItem(CartItem item);

        Task<string> Checkout();
    }
}

