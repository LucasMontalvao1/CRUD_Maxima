using API.Models;
using API.Models.DTOs;
using System.Collections.Generic;

namespace API.Repositorys.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        Product GetByCodigo(string codigo);

        void AddProduct(ProductAddDTO productDto);

        void UpdateProduct(int id, ProductDTO productDto);

        void IsDeleted(int id);
    }
}
