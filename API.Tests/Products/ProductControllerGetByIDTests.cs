using API.Controllers;
using API.Models;
using API.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Products
{
    public class ProductControllerGetByIdTests
    {
        // Mock para o serviço de produtos
        private readonly Mock<IProductService> _productServiceMock;

        // Mock para o logger
        private readonly Mock<ILogger<ProductsController>> _loggerMock;

        // Instância do controlador a ser testado
        private readonly ProductsController _controller;

        public ProductControllerGetByIdTests()
        {
            // Inicializa os mocks
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();

            // Cria uma instância do controlador com os mocks
            _controller = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WithProduct_WhenProductExists()
        {
            // Arrange: Prepara o ambiente do teste
            var productId = 1;
            var expectedProduct = new Product
            {
                Id = productId,
                Codigo = "P001",
                Descricao = "Produto Teste",
                Preco = 100.0m,
                Status = true,
                CodigoDepartamento = "D001"
            };

            // Configura o mock para retornar o produto esperado quando o método GetByIdAsync for chamado
            _productServiceMock
                .Setup(service => service.GetByIdAsync(productId))
                .ReturnsAsync(expectedProduct);

            // Act: Executa o método a ser testado
            var result = await _controller.GetById(productId) as OkObjectResult;

            // Assert: Verifica se o resultado é o esperado
            result.Should().NotBeNull(); // Verifica se o resultado não é nulo
            result.StatusCode.Should().Be(200); // Verifica se o código de status HTTP é 200 OK
            var resultValue = result.Value as Product; // Obtém o valor do resultado
            resultValue.Should().NotBeNull(); // Verifica se o valor não é nulo
            resultValue.Should().BeEquivalentTo(expectedProduct); // Verifica se o valor é equivalente ao produto esperado
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFoundObjectResult_WithMessage_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 1;

            // Configura o mock para retornar null quando o método GetByIdAsync for chamado
            _productServiceMock
                .Setup(service => service.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult; // Verifica se o resultado é um NotFoundObjectResult
            notFoundResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            notFoundResult.StatusCode.Should().Be(404); // Verifica se o código de status HTTP é 404 Not Found
            notFoundResult.Value.Should().Be($"Produto com ID {productId} não encontrado."); // Verifica se a mensagem de erro está correta
        }

        [Fact]
        public async Task GetById_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange: Prepara o ambiente do teste
            var productId = 1;

            // Configura o mock para lançar uma exceção quando o método GetByIdAsync for chamado
            _productServiceMock
                .Setup(service => service.GetByIdAsync(productId))
                .ThrowsAsync(new Exception("Erro interno"));

            // Act: Executa o método a ser testado
            var result = await _controller.GetById(productId);

            // Assert: Verifica se o resultado é o esperado
            var statusCodeResult = result as ObjectResult;
            statusCodeResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            statusCodeResult.StatusCode.Should().Be(500); // Verifica se o código de status HTTP é 500 Internal Server Error
        }

        [Fact]
        public async Task GetById_ShouldLogInformation_WhenProductDoesNotExist()
        {
            // Arrange: Prepara o ambiente do teste
            var productId = 1;

            // Configura o mock para retornar null quando o método GetByIdAsync for chamado
            _productServiceMock
                .Setup(service => service.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            // Act: Executa o método a ser testado
            await _controller.GetById(productId);

            // Assert: Verifica se o logger registrou a mensagem esperada
            _loggerMock.Verify(logger => logger.Log(
                LogLevel.Information, // Verifica se o nível de log é Information
                It.IsAny<EventId>(), // Ignora o EventId
                It.Is<It.IsAnyType>((state, t) => state.ToString() == $"Produto com ID {productId} não encontrado."), // Verifica se a mensagem de log é "Produto com ID {id} não encontrado."
                It.IsAny<Exception>(), // Ignora a exceção
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), // Ignora o formato da mensagem
                Times.Once); // Verifica se o log foi chamado exatamente uma vez
        }
    }
}
