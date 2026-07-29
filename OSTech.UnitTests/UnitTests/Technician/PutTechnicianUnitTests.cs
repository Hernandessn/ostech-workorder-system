using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Equipment;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Equipment;
using OSTech.WebAPI.Dtos.Technician;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.Technician
{
    public class PutTechnicianUnitTests : IClassFixture<TechnicianUnitTestController>
    {
        private readonly TechnicianController _controller;

        public PutTechnicianUnitTests(TechnicianUnitTestController controller)
        {
            _controller = new TechnicianController(NullLogger<TechnicianController>.Instance, controller.repository, controller.mapper);
        }
        [Fact]
        public async Task PutTechnician_Return_OkResult()
        {
            // Arrange
            var id = 1;

            var updateTechnician = new UpdateTechnicianDTO
            {
                Name = "Novo técnico alterado",
                Availability = true,
                Contact = "Contato do novo técnico alterado",
                HiringDate = new DateOnly(2025, 7, 28),
                Specialty = "Eletricista alterado"
            };

            // Act
            var result = await _controller.Put(id, updateTechnician);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task PutTechnician_Return_BadRequest()
        {
            var id = 0;

            var myTechnician = new UpdateTechnicianDTO
            {
                Name = "Novo técnico alterado",
                Availability = true,
                Contact = "Contato do novo técnico alterado",
                HiringDate = new DateOnly(2025, 7, 28),
                Specialty = "Eletricista alterado"
            };

            var data = await _controller.Put(id, myTechnician);

            data.Result.Should().BeOfType<BadRequestResult>()
                .Which.StatusCode.Should().Be(400);
        }
    }
}
