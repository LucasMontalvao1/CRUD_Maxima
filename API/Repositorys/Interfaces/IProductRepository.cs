using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositorys.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task<Product> GetByCodigoAsync(string codigo);

        Task<int> AddProductAsync(Product product);

        Task UpdateProductAsync(int id, Product product);

        Task DeleteProductAsync(int id);
    }
}
