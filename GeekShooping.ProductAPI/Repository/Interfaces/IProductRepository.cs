using GeekShooping.ProductAPI.Data.ValueObjects;

namespace GeekShooping.ProductAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> GetById(long pruductId);
        Task<ProductDto> Post(ProductDto product);
        Task<ProductDto> Put(ProductDto product);
        Task<bool> Delete(long pruductId);
    }
}