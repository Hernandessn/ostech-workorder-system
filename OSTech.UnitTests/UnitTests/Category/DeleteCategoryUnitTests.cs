using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Domain.Services;
using OSTech.Tests.Helpers;
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
        private readonly CategoryDomainService _categoryDomainService;
        private readonly CategoryController _controller;
        private readonly IMemoryCache _memoryCache;

        public DeleteCategoryUnitTests(CategoryUnitTestController controller)
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            var domainAdapter = new WorkOrderRepositoryDomainAdapter(controller.repository.WorkOrderRepository);
            _categoryDomainService = new CategoryDomainService(domainAdapter);

            _controller = new CategoryController(NullLogger<CategoryController>.Instance,
                                                 controller.repository, controller.mapper, _memoryCache, _categoryDomainService);
        }
        [Fact]
        public async Task DeleteCategoryById_Return_NoContent()
        {
            var id = 4;

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
