using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Category
{
    public class PostCategoryUnitTests : IClassFixture<CategoryUnitTestController>
    {
        private readonly CategoryController _controller;
        private readonly IMemoryCache _memoryCache;

        public PostCategoryUnitTests(CategoryUnitTestController controller)
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            _controller = new CategoryController(NullLogger<CategoryController>.Instance, controller.repository, controller.mapper, _memoryCache);

        }
        [Fact]
        public async Task PostCategory_CreatedStatusCode()
        {
            // Arrange
            var newCategoryDto = new CreateCategoryDTO
            {
                Name = "Nova Categoria",
                Description = "Descrição da nova Categoria"
            };

            // Act
            var data = await _controller.Post(newCategoryDto);

            // Assert
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
        [Fact]
        public async Task PostCategory_Return_BadRequest()
        {
            // Arrange
            CreateCategoryDTO dto = null;

            // Act
            var data = await _controller.Post(dto);

            // Assert
            data.Result.Should().BeOfType<BadRequestResult>()
                       .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
