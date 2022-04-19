using Blazored.LocalStorage;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TeamBravo.Client.Services.ProductService;
using TeamBravo.Shared;

namespace TeamBravo.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IProductService _productService;
        private readonly HttpClient _http;

        public event Action OnChange;

        public CartService(ILocalStorageService localStorage,
            IToastService toastService,
            IProductService productService,
            HttpClient http)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _productService = productService;
            _http = http;
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

            //var product2 = await _productService.GetProduct(product.Id);
            _toastService.ShowSuccess(product.Title, "Added to cart:");

            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var result = new List<CartItem>();
            var cart = await _localStorage.GetItemAsync<List<Product>>("cart");
            if (cart == null)
            {
                return result;
            }

            foreach(var item in cart)
            {
                var product = await _productService.GetProduct(item.Id);
                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    Price = product.Price,
                    ProductTitle = product.Title,
                    imgUrl = product.ImgUrl
                };

                result.Add(cartItem);
            }
            return result;
        }

        public async Task DeleteItem(CartItem item)
        {
            var cart = await _localStorage.GetItemAsync<List<Product>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.Id == item.ProductId);
            cart.Remove(cartItem);

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();

        }

        public async Task<string> Checkout()
        {
            var result = await _http.PostAsJsonAsync("api/payment/checkout", await GetCartItems());
            var url = await result.Content.ReadAsStringAsync();
            return url;
        }
    }
}
