using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Events;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;

namespace ContosoCrafts.WebSite.Components
{
    public class ProductListBase : ComponentBase
    {
        [Inject]
        protected IProductService ProductService { get; set; }

        [Inject]
        private IEventAggregator EventAggregator { get; set; }

        [Inject]
        private IHttpClientFactory ClientFactory { get; set; }

        protected IEnumerable<Product> products = null;
        protected Product selectedProduct;
        protected string selectedProductId;

        protected override async Task OnInitializedAsync()
        {
            if (products == null)
                products = await ProductService.GetProducts();
        }
        protected async Task SelectProduct(string productId)
        {
            selectedProductId = productId;
            selectedProduct = (await ProductService.GetProducts()).First(x => x.Id == productId);
        }

        protected async Task SubmitRating(int rating)
        {
            await ProductService.AddRating(selectedProductId, rating);
            await SelectProduct(selectedProductId);
            StateHasChanged();
        }

        protected async Task AddToCart(string productId, string title)
        {
            // get state
            var client = ClientFactory.CreateClient("dapr");
            var resp = await client.GetAsync($"v1.0/state/{Constants.STORE_NAME}/cart");

            if (!resp.IsSuccessStatusCode) return;

            Dictionary<string, CartItem> state = null;
            if (resp.StatusCode == HttpStatusCode.NoContent)
            {
                // Empty cart
                state = new Dictionary<string, CartItem> { [productId] = new CartItem { Title = title, Quantity = 1 } };
            }
            else if (resp.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await resp.Content.ReadAsStringAsync();
                state = JsonSerializer.Deserialize<Dictionary<string, CartItem>>(responseBody);
                if (state.ContainsKey(productId))
                {
                    // Product already in cart
                    CartItem selectedItem = state[productId];
                    selectedItem.Quantity++;
                    state[productId] = selectedItem;
                }
                else
                {
                    // Add product to car
                    state[productId] = new CartItem { Title = title, Quantity = 1 };
                }
            }

            // persist state in dapr
            var payload = JsonSerializer.Serialize(new[] {
                new { key = "cart", value = state }
            });

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            await client.PostAsync($"v1.0/state/{Constants.STORE_NAME}", content);
            await EventAggregator.PublishAsync(new ShoppingCartUpdated { ItemCount = state.Keys.Count });
        }
    }
}
