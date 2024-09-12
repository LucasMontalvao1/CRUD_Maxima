using API.Models;
using API.Models.DTOs;
using API.Repositorys.Interfaces;
using API.Services.Interfaces;
using System.Collections.Generic;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product GetByCodigo(string codigo)
        {
            return _productRepository.GetByCodigo(codigo);
        }

        public void AddProduct(ProductAddDTO productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Produto não pode ser nulo.");
            }

            _productRepository.AddProduct(productDto);
        }

        public void UpdateProduct(int id, ProductDTO productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Produto não pode ser nulo.");
            }

            _productRepository.UpdateProduct(id, productDto);
        }

        public void DeleteProduct(int id)
        {
            _productRepository.IsDeleted(id);
        }
    }
}
