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
    public class PostTechnicianUnitTests : IClassFixture<TechnicianUnitTestController>
    {
        private readonly TechnicianController _controller;

        public PostTechnicianUnitTests(TechnicianUnitTestController controller)
        {
            _controller = new TechnicianController(NullLogger<TechnicianController>.Instance, controller.repository, controller.mapper);

        }
        [Fact]
        public async Task PosEquipment_CreatedStatusCode()
        {
            // Arrange
            var newTechnicianDto = new CreateTechnicianDTO
            {
                Name = "Novo técnico",
                Availability = true,
                Contact = "Contato do novo técnico",
                HiringDate = new DateOnly(2025, 7, 28),
                Specialty = "Eletricista"
            };

            // Act
            var data = await _controller.Post(newTechnicianDto);

            // Assert
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
        [Fact]
        public async Task PosEquipment_Return_BadRequest()
        {
            // Arrange
            CreateTechnicianDTO dto = null;

            // Act
            var data = await _controller.Post(dto);

            // Assert
            data.Result.Should().BeOfType<BadRequestResult>()
                       .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
