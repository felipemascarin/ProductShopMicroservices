using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string basePath = "api/v1/product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ProductModel> PostProduct(ProductModel model)
        {
            var response = await _httpClient.PostAsJson(basePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Something went wrong then calling API");
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync(basePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> GetById(long productId)
        {
            var response = await _httpClient.GetAsync($"{basePath}/{productId}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> PutProduct(ProductModel model)
        {
            var response = await _httpClient.PutAsJson(basePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Something went wrong then calling API");
        }

        public async Task<bool> Delete(long productId)
        {
            var response = await _httpClient.DeleteAsync($"{basePath}/{productId}");
            if (response.IsSuccessStatusCode) return true;
            else throw new Exception("Something went wrong then calling API");
        }
    }
}
