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
    public class GetCategoryUnitTests : IClassFixture<CategoryUnitTestController>
    {
        private readonly CategoryController _controller;
        private readonly IMemoryCache _memoryCache;

        public GetCategoryUnitTests(CategoryUnitTestController controller)
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            _controller = new CategoryController(NullLogger<CategoryController>.Instance, 
                controller.repository, controller.mapper, _memoryCache);
        }

        [Fact]
        public async Task GetCategoryById_OkResult()
        {
            //Arrange
            var id = 2;

            //Act
            var data = await _controller.Get(id);

            //Assert (xunit)
            var okResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task GetCategoryById_Returns_NotFound()
        {
            //Arrange
            var id = 999;

            //Act
            var data = await _controller.Get(id);

            //Assert
            data.Result.Should().BeOfType<NotFoundObjectResult>()
                       .Which.StatusCode.Should().Be(404);
        }
        [Fact]
        public async Task GetCategory_Returns_ListOfCategoryDTO()
        {
            //Act
            var data = await _controller.Get();

            //Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                       .Which.Value.Should()
                       .BeAssignableTo<IEnumerable<CategoryDTO>>();
        }
        [Fact]
        public async Task GetCategoryById_Returns_NotFoundResult()
        {
            //Arrange
            var id = 0;

            //Act 
            var data = await _controller.Get(id);

            //Assert
            data.Result.Should().BeOfType<NotFoundObjectResult>()
                       .Which.StatusCode.Should().Be(404);
        }
    }
}
