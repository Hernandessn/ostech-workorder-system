using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Equipment;
using OSTech.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Technician
{
    public class DeleteTechnicianUnitTests : IClassFixture<TechnicianUnitTestController>
    {
        private readonly TechnicianController _controller;
        public DeleteTechnicianUnitTests(TechnicianUnitTestController controller)
        {
            _controller = new TechnicianController(NullLogger<TechnicianController>.Instance, controller.repository, controller.mapper);
        }

        [Fact]
        public async Task DeleteTechnicianById_Return_NoContent()
        {
            var id = 2;

            // Act
            var result = await _controller.Delete(id);

            // Assert
            result.Should().BeOfType<NoContentResult>()
                  .Which.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteTechnicianById_Return_NotFound()
        {
            var id = 999;

            var result = await _controller.Delete(id);

            result.Should().BeOfType<NotFoundObjectResult>()
                  .Which.StatusCode.Should().Be(404);
        }
    }
}
