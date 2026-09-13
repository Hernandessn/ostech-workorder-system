using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.WebAPI.Controllers;


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
