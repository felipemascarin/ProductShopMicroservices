using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<ProductModel> GetById(long productId);
        Task<ProductModel> PostProduct(ProductModel model);
        Task<ProductModel> PutProduct(ProductModel model);
        Task<bool> Delete(long productId);
    }
}