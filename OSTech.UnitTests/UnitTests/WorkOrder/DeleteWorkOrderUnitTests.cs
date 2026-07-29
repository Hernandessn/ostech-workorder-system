using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Technician;
using OSTech.Tests.UnitTests.WorkOder;
using OSTech.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.WorkOrder
{
    public class DeleteWorkOrderUnitTests : IClassFixture<WorkOrderUnitTestController>
    {
        private readonly WorkOrderController _controller;

        public DeleteWorkOrderUnitTests(WorkOrderUnitTestController controller)
        {
            _controller = new WorkOrderController(NullLogger<WorkOrderController>.Instance, controller.repository, controller.mapper);
        }

        [Fact]
        public async Task DeleteWorkOrderById_Return_NoContent()
        {
            var id = 1;

            // Act
            var result = await _controller.Delete(id);

            // Assert
            result.Should().BeOfType<NoContentResult>()
                  .Which.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteWorkOrderById_Return_NotFound()
        {
            var id = 999;

            var result = await _controller.Delete(id);

            result.Should().BeOfType<NotFoundObjectResult>()
                  .Which.StatusCode.Should().Be(404);
        }
    }
}
