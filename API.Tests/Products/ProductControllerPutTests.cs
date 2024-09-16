using API.Controllers;
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
    public class ProductControllerPutTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ILogger<ProductsController>> _loggerMock;
        private readonly ProductsController _controller;

        public ProductControllerPutTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Put_ShouldReturnBadRequest_WhenProductDtoIsNullOrIdDoesNotMatch()
        {
            // Arrange
            int id = 1;
            ProductDTO productDto = null;

            // Act
            var result = await _controller.Put(id, productDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().BeEquivalentTo(new { message = "Dados de produto inválidos ou ID incorreto." });
        }


        [Fact]
        public async Task Put_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            int id = 1;
            var productDto = new ProductDTO
            {
                Id = id,
                Codigo = "P001",
                Descricao = "Produto Atualizado",
                Preco = 200.0m,
                Status = true,
                CodigoDepartamento = "D001"
            };

            var exceptionMessage = "Erro ao atualizar o produto";

            // Configura o mock para lançar uma exceção
            _productServiceMock
                .Setup(service => service.UpdateProductAsync(id, productDto))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.Put(id, productDto);

            // Assert
            var statusCodeResult = result as ObjectResult;
            statusCodeResult.Should().NotBeNull();
            statusCodeResult.StatusCode.Should().Be(500);
            statusCodeResult.Value.Should().BeEquivalentTo(new { message = $"Erro ao atualizar o produto: {exceptionMessage}" });
        }
    }
}
