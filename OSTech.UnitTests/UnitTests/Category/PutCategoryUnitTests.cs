using FluentAssertions;
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
    public class PutCategoryUnitTests : IClassFixture<CategoryUnitTestController>
    {
        private readonly CategoryController _controller;
        private readonly IMemoryCache _memoryCache;

        public PutCategoryUnitTests(CategoryUnitTestController controller)
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            _controller = new CategoryController(NullLogger<CategoryController>.Instance, controller.repository, controller.mapper, _memoryCache);
        }
        [Fact]
        public async Task PutCategory_Return_OkResult()
        {
            // Arrange
            var id = 1;

            var updatedCategory = new UpdateCategoryDTO
            {
                Name = "Nova Categoria Alterada",
                Description = "Descrição da nova Categoria Alterada"
            };

            // Act
            var result = await _controller.Put(id, updatedCategory);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task PutCategory_Return_BadRequest()
        {
            var id = 0;

            var myCategory = new UpdateCategoryDTO
            {
                Name = "Nova Categoria Alterada",
                Description = "Descrição da nova Categoria Alterada"
            };

            var data = await _controller.Put(id, myCategory);

            data.Result.Should().BeOfType<BadRequestResult>()
                .Which.StatusCode.Should().Be(400);
        }
    }
}
