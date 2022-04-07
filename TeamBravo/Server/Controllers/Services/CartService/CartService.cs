using Blazored.LocalStorage;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamBravo.Client.Services.ProductService;
using TeamBravo.Shared;

namespace TeamBravo.Server.Controllers.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IProductService _productService;

        public event Action OnChange;

        public CartService(ILocalStorageService localStorage,
            IToastService toastService,
            IProductService productService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _productService = productService;
        }

        public async Task AddToCart(Product product)
        {
            var cart = await _localStorage.GetItemAsync<List<Product>>("cart");
            if (cart == null)
            {
                cart = new List<Product>();
            }

            cart.Add(product);
            await _localStorage.SetItemAsync("cart", cart);

            var product2 = await _productService.GetProduct(product.Id);
            _toastService.ShowSuccess(product.Title, "Added to cart:");

            OnChange.Invoke();
        }
    }
}
