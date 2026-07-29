using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Category;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Dtos.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Equipment
{
    public class PostEquipmentUnitTests : IClassFixture<EquipmentUnitTestController>
    {
        private readonly EquipmentController _controller;

        public PostEquipmentUnitTests(EquipmentUnitTestController controller)
        {
            _controller = new EquipmentController(NullLogger<EquipmentController>.Instance, controller.repository, controller.mapper);

        }
        [Fact]
        public async Task PosEquipment_CreatedStatusCode()
        {
            // Arrange
            var newEquipmentDto = new CreateEquipmentDTO
            {
                Name = "Novo equipamento",
                Model = "Modelo do novo equipamento",
                Brand = "Marca do novo equipamento",
                SerialNumber = "Número de série do novo equipamento"
            };

            // Act
            var data = await _controller.Post(newEquipmentDto);

            // Assert
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
        [Fact]
        public async Task PosEquipment_Return_BadRequest()
        {
            // Arrange
            CreateEquipmentDTO dto = null;

            // Act
            var data = await _controller.Post(dto);

            // Assert
            data.Result.Should().BeOfType<BadRequestResult>()
                       .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
