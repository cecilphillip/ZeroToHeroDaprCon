using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileProductService : IProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var fileContent = await jsonFileReader.ReadToEndAsync();

            return JsonSerializer.Deserialize<Product[]>(fileContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
        }

        public async Task AddRating(string productId, int rating)
        {
            var products = await GetProducts();
            var p = products.First(x => x.Id == productId);

            if (p.Ratings == null)
            {
                p.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = p.Ratings.ToList();
                ratings.Add(rating);
                p.Ratings = ratings.ToArray();
            }

            using var outputStream = File.OpenWrite(JsonFileName);
            await JsonSerializer.SerializeAsync(outputStream, products,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                });
        }
    }
}