using API.Models;
using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task<Product> GetByCodigoAsync(string codigo);

        Task<int> AddProductAsync(ProductAddDTO productDto);

        Task<int> UpdateProductAsync(int id, ProductDTO productDto);

        Task<(int Id, string Message)> DeleteProductAsync(int id);
    }
}
