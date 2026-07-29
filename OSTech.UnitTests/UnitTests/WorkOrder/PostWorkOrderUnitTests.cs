using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
    public class PostWorkOrderUnitTests : IClassFixture<WorkOrderUnitTestController>
    {
        private readonly WorkOrderController _controller;

        public PostWorkOrderUnitTests(WorkOrderUnitTestController controller)
        {
            _controller = new WorkOrderController(NullLogger<WorkOrderController>.Instance, controller.repository, controller.mapper);

        }
        [Fact]
        public async Task PosWorkOrder_CreatedStatusCode()
        {
            // Arrange
            var newWorkOrderDto = new CreateWorkOrderDTO
            {
                Description = "Troca da tela do notebook",
                Title = "Manutenção de notebook",
                Amount = 450.00m,
                Deadline = new DateOnly(2026, 8, 30),
                OpeningDate = DateOnly.FromDateTime(DateTime.Today),
                TechnicianId = 1,
                CustomerId = 1,
                CategoryId = 1,
                EquipmentId = 1
            };

            // Act
            var data = await _controller.Post(newWorkOrderDto);

            // Assert
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
        [Fact]
        public async Task PosWorkOrder_Return_BadRequest()
        {
            // Arrange
            CreateWorkOrderDTO dto = null;

            // Act
            var data = await _controller.Post(dto);

            // Assert
            data.Result.Should().BeOfType<BadRequestResult>()
                       .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
