using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.WebAPI.Controllers;
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

        public DeleteCategoryUnitTests(CategoryUnitTestController controller)
        {
            _controller = new CategoryController(NullLogger<CategoryController>.Instance, controller.repository, controller.mapper);
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
