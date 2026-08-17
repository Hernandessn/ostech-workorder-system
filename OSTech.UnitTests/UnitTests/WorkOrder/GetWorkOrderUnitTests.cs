using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Technician;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Technician;
using OSTech.WebAPI.Dtos.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.WorkOder
{
    public class GetWorkOrderUnitTests : IClassFixture<WorkOrderUnitTestController>
    {
        private readonly WorkOrderController _controller;

        public GetWorkOrderUnitTests(WorkOrderUnitTestController controller)
        {
            _controller = new WorkOrderController(NullLogger<WorkOrderController>.Instance, controller.repository, controller.mapper);
        }

        [Fact]
        public async Task GetWorkOrderById_OkResult()
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
        public async Task GetWorkOrderById_Returns_NotFound()
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
        public async Task GetWorkOrder_Returns_ListOfWorkOrderDTO()
        {
            //Act
            var data = await _controller.Get();

            //Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                       .Which.Value.Should()
                       .BeAssignableTo<IEnumerable<WorkOrderDTO>>();
        }
        [Fact]
        public async Task GetWorkOrderById_Returns_NotFoundResult()
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
