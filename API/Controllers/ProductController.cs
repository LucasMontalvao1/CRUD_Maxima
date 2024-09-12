using API.Models.DTOs;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();

            if (products == null || !products.Any())
                return NotFound("Nenhum produto encontrado.");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound("Produto não encontrado.");

            return Ok(product);
        }

        [HttpGet("codigo/{codigo}")]
        public IActionResult GetByCodigo(string codigo)
        {
            try
            {
                var product = _productService.GetByCodigo(codigo);
                if (product == null)
                    return NotFound("Produto não encontrado.");

                return Ok(product);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductAddDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("O produto não pode ser nulo.");
            }

            try
            {
                _productService.AddProduct(productDto);
                return Ok("Produto cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar o produto: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductDTO productDto)
        {
            if (productDto == null || id != productDto.Id)
            {
                return BadRequest("Dados de produto inválidos.");
            }

            try
            {
                _productService.UpdateProduct(id, productDto);
                return Ok("Produto atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar o produto: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return Ok($"Produto com ID {id} foi marcado como deletado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar o produto: {ex.Message}");
            }
        }

    }
}
