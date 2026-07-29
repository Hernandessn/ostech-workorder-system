using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Category;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Dtos.Technician;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Technician
{
    public class GetTechnicianUnitTests : IClassFixture<TechnicianUnitTestController>
    {
        private readonly TechnicianController _controller;

        public GetTechnicianUnitTests(TechnicianUnitTestController controller)
        {
            _controller = new TechnicianController(NullLogger<TechnicianController>.Instance, controller.repository, controller.mapper);
        }

        [Fact]
        public async Task GetTechnicianById_OkResult()
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
        public async Task GetTechnicianById_Returns_NotFound()
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
        public async Task GetTechnicians_Returns_ListOfTechnicianDTO()
        {
            //Act
            var data = await _controller.Get();

            //Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                       .Which.Value.Should()
                       .BeAssignableTo<IEnumerable<TechnicianDTO>>();
        }
    }
}
