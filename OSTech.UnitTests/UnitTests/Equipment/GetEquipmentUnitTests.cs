using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Customer;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Dtos.Customer;
using OSTech.WebAPI.Dtos.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Equipment
{
    public class GetEquipmentUnitTests : IClassFixture<EquipmentUnitTestController>
    {
        private readonly EquipmentController _controller;

        public GetEquipmentUnitTests(EquipmentUnitTestController controller)
        {
            _controller = new EquipmentController(NullLogger<EquipmentController>.Instance, controller.repository, controller.mapper);
        }
        [Fact]
        public async Task GetEquipmentById_OkResult()
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
        public async Task GetEquipmentById_Returns_NotFound()
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
        public async Task GetEquipment_Returns_ListOfEquipmentDTO()
        {
            // Act
            var data = await _controller.Get();

            // Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeAssignableTo<IEnumerable<EquipmentDTO>>();
        }
        [Fact]
        public async Task GetEquipmentById_Returns_NotFoundResult()
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
