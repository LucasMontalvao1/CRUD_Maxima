using API.Controllers;
using API.Models;
using API.Models.DTOs;
using API.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ProductControllerPostTests
    {
        // Mock para o serviço de produtos
        private readonly Mock<IProductService> _productServiceMock;

        // Mock para o logger
        private readonly Mock<ILogger<ProductsController>> _loggerMock;

        // Instância do controlador a ser testado
        private readonly ProductsController _controller;

        public ProductControllerPostTests()
        {
            // Inicializa os mocks
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();

            // Cria uma instância do controlador com os mocks
            _controller = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenProductDtoIsNull()
        {
            // Arrange
            ProductAddDTO productDto = null;

            // Act
            var result = await _controller.Post(productDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult; // Verifica se o resultado é um BadRequestObjectResult
            badRequestResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            badRequestResult.StatusCode.Should().Be(400); // Verifica se o código de status HTTP é 400 Bad Request
            badRequestResult.Value.Should().Be("Os dados do produto não podem ser nulos."); // Verifica se a mensagem de erro está correta
        }

        
        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenValidationErrorOccurs()
        {
            // Arrange
            var productDto = new ProductAddDTO
            {
                Codigo = "P001",
                Descricao = "Produto Teste",
                Preco = 100.0m,
                Status = true,
                CodigoDepartamento = "D001"
            };

            // Configura o mock para lançar uma ArgumentException quando o método AddProductAsync for chamado
            _productServiceMock
                .Setup(service => service.AddProductAsync(productDto))
                .ThrowsAsync(new ArgumentException("Erro de validação ao adicionar o produto."));

            // Act
            var result = await _controller.Post(productDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult; // Verifica se o resultado é um BadRequestObjectResult
            badRequestResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            badRequestResult.StatusCode.Should().Be(400); // Verifica se o código de status HTTP é 400 Bad Request
            badRequestResult.Value.Should().Be("Erro de validação ao adicionar o produto."); // Verifica se a mensagem de erro está correta
        }

        [Fact]
        public async Task Post_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            var productDto = new ProductAddDTO
            {
                Codigo = "P001",
                Descricao = "Produto Teste",
                Preco = 100.0m,
                Status = true,
                CodigoDepartamento = "D001"
            };

            var exceptionMessage = "Dados incorretos para adicioanr o produto"; // Corrigido para coincidir com o texto do método

            // Configura o mock para lançar uma exceção quando o método AddProductAsync for chamado
            _productServiceMock
                .Setup(service => service.AddProductAsync(productDto))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.Post(productDto);

            // Assert
            var statusCodeResult = result as ObjectResult; // Verifica se o resultado é um ObjectResult
            statusCodeResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            statusCodeResult.StatusCode.Should().Be(500); // Verifica se o código de status HTTP é 500 Internal Server Error
            statusCodeResult.Value.Should().Be(exceptionMessage); // Verifica se a mensagem de erro está correta
        }
    }
}
