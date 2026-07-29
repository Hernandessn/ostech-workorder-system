using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Customer;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Customer;
using OSTech.WebAPI.Dtos.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Equipment
{
    public class PutEquipmentUnitTests : IClassFixture<EquipmentUnitTestController>
    {
        private readonly EquipmentController _controller;

        public PutEquipmentUnitTests(EquipmentUnitTestController controller)
        {
            _controller = new EquipmentController(NullLogger<EquipmentController>.Instance, controller.repository, controller.mapper);
        }
        [Fact]
        public async Task PutEquipment_Return_OkResult()
        {
            // Arrange
            var id = 1;

            var updatedEquipment = new UpdateEquipmentDTO
            {
                Name = "Novo equipamento alterado",
                Model = "Modelo do novo equipamento alterado",
                Brand = "Marca do novo equipamento alterado",
                SerialNumber = "Número de série do novo equipamento alterado"
            };

            // Act
            var result = await _controller.Put(id, updatedEquipment);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task PutEquipment_Return_BadRequest()
        {
            var id = 0;

            var myEquipment = new UpdateEquipmentDTO
            {
                Name = "Novo equipamento alterado",
                Model = "Modelo do novo equipamento alterado",
                Brand = "Marca do novo equipamento alterado",
                SerialNumber = "Número de série do novo equipamento alterado"
            };

            var data = await _controller.Put(id, myEquipment);

            data.Result.Should().BeOfType<BadRequestResult>()
                .Which.StatusCode.Should().Be(400);
        }
    }
}
