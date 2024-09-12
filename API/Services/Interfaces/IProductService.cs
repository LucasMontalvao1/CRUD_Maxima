using API.Models;
using API.Models.DTOs;
using System.Collections.Generic;

namespace API.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        Product GetByCodigo(string codigo);

        void AddProduct(ProductAddDTO productDto);

        void UpdateProduct(int id, ProductDTO productDto);

        void DeleteProduct(int id);
    }
}
