using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.WebAPI.Controllers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Category
{
    public class DeleteCategoryUnitTests : IClassFixture<CategoryUnitTestController>
    {
        private readonly CategoryController _controller;
        private readonly IMemoryCache _memoryCache;
        public DeleteCategoryUnitTests(CategoryUnitTestController controller)
        {

            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            _controller = new CategoryController(NullLogger<CategoryController>.Instance, 
                                                 controller.repository, controller.mapper, _memoryCache);
        }

        [Fact]
        public async Task DeleteCategoryById_Return_NoContent()
        {
            var id = 1;

            // Act
            var result = await _controller.Delete(id);

            // Assert
            result.Should().BeOfType<NoContentResult>()
                  .Which.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteCategoryById_Return_NotFound()
        {
            var id = 999;

            var result = await _controller.Delete(id);

            result.Should().BeOfType<NotFoundObjectResult>()
                  .Which.StatusCode.Should().Be(404);
        }
    }
}
