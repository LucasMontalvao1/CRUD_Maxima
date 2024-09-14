using API.Models;
using API.Models.DTOs;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllAsync();

                if (products == null || !products.Any())
                {
                    _logger.LogInformation("Nenhum produto encontrado.");
                    return Ok(products);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar todos os produtos.");
                return StatusCode(500, "Ocorreu um erro ao recuperar os produtos.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogInformation($"Produto com ID {id} não encontrado.");
                    return NotFound($"Produto com ID {id} não encontrado.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao recuperar o produto com ID {id}.");
                return StatusCode(500, $"Ocorreu um erro ao recuperar o produto com ID {id}.");
            }
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            try
            {
                var product = await _productService.GetByCodigoAsync(codigo);
                if (product == null)
                {
                    _logger.LogInformation($"Produto com código {codigo} não encontrado.");
                    return NotFound($"Produto com código {codigo} não encontrado.");
                }

                return Ok(product);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, $"Código de produto inválido: {codigo}.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao recuperar o produto com código {codigo}.");
                return StatusCode(500, $"Ocorreu um erro ao recuperar o produto com código {codigo}.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductAddDTO productDto)
        {
            if (productDto == null)
            {
                _logger.LogWarning("Dados do produto são nulos.");
                return BadRequest("Os dados do produto não podem ser nulos.");
            }

            try
            {
                var productId = await _productService.AddProductAsync(productDto);
                return CreatedAtAction(nameof(GetById), new { id = productId }, productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar novo produto.");
                return StatusCode(500, $"Erro ao adicionar o produto: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDTO productDto)
        {
            if (productDto == null || id != productDto.Id)
            {
                _logger.LogWarning("Dados de produto inválidos ou ID incorreto.");
                return BadRequest("Dados de produto inválidos ou ID incorreto.");
            }

            try
            {
                await _productService.UpdateProductAsync(id, productDto);
                return Ok("Produto atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o produto com ID {id}.");
                return StatusCode(500, $"Erro ao atualizar o produto: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                return Ok(result.Message);  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar o produto com ID {id}.");
                return StatusCode(500, $"Erro ao deletar o produto: {ex.Message}");
            }
        }
    }
}
