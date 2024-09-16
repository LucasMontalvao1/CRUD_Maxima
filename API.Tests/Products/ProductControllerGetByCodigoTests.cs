using API.Controllers;
using API.Models;
using API.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Products
{
    public class ProductControllerGetByCodigoTests
    {
        // Mock para o serviço de produtos
        private readonly Mock<IProductService> _productServiceMock;

        // Mock para o logger
        private readonly Mock<ILogger<ProductsController>> _loggerMock;

        // Instância do controlador a ser testado
        private readonly ProductsController _controller;

        public ProductControllerGetByCodigoTests()
        {
            // Inicializa os mocks
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();

            // Cria uma instância do controlador com os mocks
            _controller = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetByCodigo_ShouldReturnOkResult_WithProduct_WhenProductExists()
        {
            // Arrange
            var codigo = "P001";
            var expectedProduct = new Product
            {
                Id = 1,
                Codigo = codigo,
                Descricao = "Produto Teste",
                Preco = 100.0m,
                Status = true,
                CodigoDepartamento = "D001"
            };

            // Configura o mock para retornar o produto esperado quando o método GetByCodigoAsync for chamado
            _productServiceMock
                .Setup(service => service.GetByCodigoAsync(codigo))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await _controller.GetByCodigo(codigo) as OkObjectResult;

            // Assert
            result.Should().NotBeNull(); // Verifica se o resultado não é nulo
            result.StatusCode.Should().Be(200); // Verifica se o código de status HTTP é 200 OK
            var resultValue = result.Value as Product; // Obtém o valor do resultado
            resultValue.Should().NotBeNull(); // Verifica se o valor não é nulo
            resultValue.Should().BeEquivalentTo(expectedProduct); // Verifica se o valor é equivalente ao produto esperado
        }

        [Fact]
        public async Task GetByCodigo_ShouldReturnNotFoundObjectResult_WithMessage_WhenProductDoesNotExist()
        {
            // Arrange
            var codigo = "P001";

            // Configura o mock para retornar null quando o método GetByCodigoAsync for chamado
            _productServiceMock
                .Setup(service => service.GetByCodigoAsync(codigo))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetByCodigo(codigo);

            // Assert
            var notFoundResult = result as NotFoundObjectResult; // Verifica se o resultado é um NotFoundObjectResult
            notFoundResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            notFoundResult.StatusCode.Should().Be(404); // Verifica se o código de status HTTP é 404 Not Found
            notFoundResult.Value.Should().Be($"Produto com código {codigo} não encontrado."); // Verifica se a mensagem de erro está correta
        }

        [Fact]
        public async Task GetByCodigo_ShouldReturnNotFoundObjectResult_WithMessage_WhenCodeIsInvalid()
        {
            // Arrange
            var invalidCodigo = "INVALID_CODE";

            // Configura o mock para lançar uma ArgumentException quando o método GetByCodigoAsync for chamado com um código inválido
            _productServiceMock
                .Setup(service => service.GetByCodigoAsync(invalidCodigo))
                .ThrowsAsync(new ArgumentException("Código de produto inválido"));

            // Act
            var result = await _controller.GetByCodigo(invalidCodigo);

            // Assert
            var notFoundResult = result as NotFoundObjectResult; // Verifica se o resultado é um NotFoundObjectResult
            notFoundResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            notFoundResult.StatusCode.Should().Be(404); // Verifica se o código de status HTTP é 404 Not Found
            notFoundResult.Value.Should().Be("Código de produto inválido"); // Verifica se a mensagem de erro está correta
        }

        [Fact]
        public async Task GetByCodigo_ShouldReturnInternalServerError_WithMessage_WhenExceptionIsThrown()
        {
            // Arrange
            var codigo = "P001";
            var exceptionMessage = $"Produto com código {codigo} não encontrado.";

            // Configura o mock para lançar uma exceção quando o método GetByCodigoAsync for chamado
            _productServiceMock
                .Setup(service => service.GetByCodigoAsync(codigo))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetByCodigo(codigo);

            // Assert
            var statusCodeResult = result as ObjectResult; // Verifica se o resultado é um ObjectResult
            statusCodeResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            statusCodeResult.StatusCode.Should().Be(500); // Verifica se o código de status HTTP é 500 Internal Server Error
            statusCodeResult.Value.Should().Be(exceptionMessage); // Verifica se a mensagem de erro está correta
        }
    }
}
