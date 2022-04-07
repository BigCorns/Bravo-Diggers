using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamBravo.Shared;

namespace TeamBravo.Server.Controllers.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(Product product);
    }
}
