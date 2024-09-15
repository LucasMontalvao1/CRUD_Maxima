using Moq;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using API.Models.DTOs;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Tests
{
    public class ProductControllerGetTests
    {
        // Mock para o serviço de produtos
        private readonly Mock<IProductService> _productServiceMock;

        // Mock para o logger
        private readonly Mock<ILogger<ProductsController>> _loggerMock;

        // Instância do controlador a ser testado
        private readonly ProductsController _controller;

        public ProductControllerGetTests()
        {
            // Inicializa os mocks
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();

            // Cria uma instância do controlador com os mocks
            _controller = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithListOfProducts()
        {
            // Arrange: Prepara o ambiente do teste
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Codigo = "P001",
                    Descricao = "Produto Teste 1",
                    Preco = 100.0m,
                    Status = true,
                    CodigoDepartamento = "D001"
                },
                new Product
                {
                    Id = 2,
                    Codigo = "P002",
                    Descricao = "Produto Teste 2",
                    Preco = 150.0m,
                    Status = false,
                    CodigoDepartamento = "D002"
                }
            };

            // Configura o mock para retornar a lista esperada quando o método GetAllAsync for chamado
            _productServiceMock
                .Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedProducts);

            // Act: Executa o método a ser testado
            var result = await _controller.GetAll() as OkObjectResult;

            // Assert: Verifica se o resultado é o esperado
            result.Should().NotBeNull(); // Verifica se o resultado não é nulo
            result.StatusCode.Should().Be(200); // Verifica se o código de status HTTP é 200 OK
            var resultValue = result.Value as IEnumerable<Product>; // Obtém o valor do resultado
            resultValue.Should().NotBeNull(); // Verifica se o valor não é nulo
            resultValue.Should().BeEquivalentTo(expectedProducts); // Verifica se o valor é equivalente à lista esperada
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithEmptyListOfProducts_WhenNoProductsExist()
        {
            // Arrange: Prepara o ambiente do teste
            var expectedProducts = new List<Product>();

            // Configura o mock para retornar uma lista vazia quando o método GetAllAsync for chamado
            _productServiceMock
                .Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedProducts);

            // Act: Executa o método a ser testado
            var result = await _controller.GetAll() as OkObjectResult;

            // Assert: Verifica se o resultado é o esperado
            result.Should().NotBeNull(); // Verifica se o resultado não é nulo
            result.StatusCode.Should().Be(200); // Verifica se o código de status HTTP é 200 OK
            var resultValue = result.Value as IEnumerable<Product>; // Obtém o valor do resultado
            resultValue.Should().NotBeNull(); // Verifica se o valor não é nulo
            resultValue.Should().BeEmpty(); // Verifica se o valor é uma lista vazia
        }

        [Fact]
        public async Task GetAll_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange: Prepara o ambiente do teste
            _productServiceMock
                .Setup(service => service.GetAllAsync())
                .ThrowsAsync(new Exception("Erro interno"));

            // Act: Executa o método a ser testado
            var result = await _controller.GetAll() as StatusCodeResult;

            // Assert: Verifica se o resultado é o esperado
            result.Should().NotBeNull(); // Verifica se o resultado não é nulo
            result.StatusCode.Should().Be(500); // Verifica se o código de status HTTP é 500 Internal Server Error
        }

        [Fact]
        public async Task GetAll_ShouldLogInformation_WhenNoProductsFound()
        {
            // Arrange: Prepara o ambiente do teste
            var expectedProducts = new List<Product>();

            // Configura o mock para retornar uma lista vazia quando o método GetAllAsync for chamado
            _productServiceMock
                .Setup(service => service.GetAllAsync())
                .ReturnsAsync(expectedProducts);

            // Act: Executa o método a ser testado
            await _controller.GetAll();

            // Assert: Verifica se o logger registrou a mensagem esperada
            _loggerMock.Verify(logger => logger.Log(
                LogLevel.Information, // Verifica se o nível de log é Information
                It.IsAny<EventId>(), // Ignora o EventId
                It.Is<It.IsAnyType>((state, t) => state.ToString() == "Nenhum produto encontrado."), // Verifica se a mensagem de log é "Nenhum produto encontrado."
                It.IsAny<Exception>(), // Ignora a exceção
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), // Ignora o formato da mensagem
                Times.Once); // Verifica se o log foi chamado exatamente uma vez
        }
    }
}
