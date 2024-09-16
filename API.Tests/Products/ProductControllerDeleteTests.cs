using API.Controllers;
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
    public class ProductControllerDeleteTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ILogger<ProductsController>> _loggerMock;
        private readonly ProductsController _controller;

        public ProductControllerDeleteTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnOk_WhenProductIsDeletedSuccessfully()
        {
            // Arrange
            int id = 1;
            var expectedMessage = "Produto deletado com sucesso.";

            // Configura o mock para o método DeleteProductAsync
            _productServiceMock
                .Setup(service => service.DeleteProductAsync(id))
                .ReturnsAsync((id, expectedMessage)); // Simula a conclusão da tarefa com o retorno esperado

            // Act
            var result = await _controller.DeleteProduct(id);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull(); // Verifica se o resultado não é nulo
            okResult.StatusCode.Should().Be(200); // Verifica se o código de status HTTP é 200 OK
            okResult.Value.Should().Be(expectedMessage); // Verifica a mensagem retornada
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            var exceptionMessage = "Erro ao deletar o produto";

            // Configura o mock para lançar uma exceção
            _productServiceMock
                .Setup(service => service.DeleteProductAsync(id))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.DeleteProduct(id);

            // Assert
            var statusCodeResult = result as ObjectResult;
            statusCodeResult.Should().NotBeNull();
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().Be($"Erro ao deletar o produto: {exceptionMessage}");
        }
    }
}
