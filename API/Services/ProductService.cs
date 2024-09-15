using API.Models;
using API.Models.DTOs;
using API.Repositorys.Interfaces;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ProductService(IProductRepository productRepository, IDepartmentRepository departmentRepository)
        {
            _productRepository = productRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                if (products == null)
                {
                    throw new ApplicationException("Nenhum produto encontrado.");
                }
                return products;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter todos os produtos.", ex);
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");
                }
                return product;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao obter o produto com ID {id}.", ex);
            }
        }

        public async Task<Product> GetByCodigoAsync(string codigo)
        {
            try
            {
                var product = await _productRepository.GetByCodigoAsync(codigo);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Produto com código {codigo} não encontrado.");
                }
                return product;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao obter o produto com código {codigo}.", ex);
            }
        }

        public async Task<object> AddProductAsync(ProductAddDTO productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto));

            var product = new Product
            {
                Codigo = productDto.Codigo,
                Descricao = productDto.Descricao,
                Preco = productDto.Preco,
                Status = productDto.Status,
                CodigoDepartamento = productDto.CodigoDepartamento
            };

            if (product.Preco <= 0)
                throw new ArgumentException("O preço deve ser maior que zero.", nameof(product.Preco));

            try
            {
                var departmentExists = await _departmentRepository.ExistsAsync(product.CodigoDepartamento);
                if (!departmentExists)
                    throw new ArgumentException("O código do departamento não é válido.", nameof(product.CodigoDepartamento));


                var productId = await _productRepository.AddProductAsync(product);

                return new
                {
                    id = productId,
                    product.Codigo,
                    product.Descricao,
                    product.Preco,
                    product.Status,
                    product.CodigoDepartamento
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao adicionar o produto.", ex);
            }
        }

        public async Task<int> UpdateProductAsync(int id, ProductDTO productDto)
        {
            if (productDto == null || id != productDto.Id)
                throw new ArgumentException("Dados de produto inválidos ou ID incorreto.", nameof(productDto));

            var product = new Product
            {
                Id = productDto.Id ?? throw new ArgumentNullException(nameof(productDto.Id), "O ID do produto não pode ser nulo."),
                Codigo = productDto.Codigo,
                Descricao = productDto.Descricao,
                Preco = productDto.Preco,
                Status = productDto.Status,
                CodigoDepartamento = productDto.CodigoDepartamento
            };

            if (product.Preco <= 0)
                throw new ArgumentException("O preço deve ser maior que zero.", nameof(product.Preco));

            try
            {
                var departmentExists = await _departmentRepository.ExistsAsync(product.CodigoDepartamento);
                if (!departmentExists)
                    throw new ArgumentException("O código do departamento não é válido.", nameof(product.CodigoDepartamento));

                var existingProduct = await _productRepository.GetByIdAsync(id);
                if (existingProduct == null)
                    throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");

                await _productRepository.UpdateProductAsync(id, product);
                return id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao atualizar o produto com ID {id}.", ex);
            }
        }




        public async Task<(int Id, string Message)> DeleteProductAsync(int id)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);
                if (existingProduct == null)
                    throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");

                await _productRepository.DeleteProductAsync(id);
                return (id, $"Produto com ID {id} foi deletado com sucesso.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao deletar o produto com ID {id}.", ex);
            }
        }
    }
}
