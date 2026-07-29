using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Domain.Entities;
using OSTech.Tests.UnitTests.Customer;
using OSTech.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Equipment
{
    public class DeleteEquipmentUnitTests : IClassFixture<EquipmentUnitTestController>
    {
        private readonly EquipmentController _controller;

        public DeleteEquipmentUnitTests(EquipmentUnitTestController controller)
        {
            _controller = new EquipmentController(NullLogger<EquipmentController>.Instance, controller.repository, controller.mapper);
        }

        [Fact]
        public async Task DeleteEquipmentById_Return_NoContent()
        {
            var id = 2;

            // Act
            var result = await _controller.Delete(id);

            // Assert
            result.Should().BeOfType<NoContentResult>()
                  .Which.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteEquipmentById_Return_NotFound()
        {
            var id = 999;

            var result = await _controller.Delete(id);

            result.Should().BeOfType<NotFoundObjectResult>()
                  .Which.StatusCode.Should().Be(404);
        }
    }
}
