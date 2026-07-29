using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.Tests.UnitTests.Technician;
using OSTech.Tests.UnitTests.WorkOder;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.Technician;
using OSTech.WebAPI.Dtos.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Tests.UnitTests.WorkOrder
{
    public class PutWorkOrderUnitTests : IClassFixture<WorkOrderUnitTestController>
    {
        private readonly WorkOrderController _controller;

        public PutWorkOrderUnitTests(WorkOrderUnitTestController controller)
        {
            _controller = new WorkOrderController(NullLogger<WorkOrderController>.Instance, controller.repository, controller.mapper);
        }
        [Fact]
        public async Task PutWorkOrder_Return_OkResult()
        {
            // Arrange
            var id = 1;

            var updateWorkOrder = new UpdateWorkOrderDTO
            {
                Description = "Troca da tela do notebook alterado",
                Title = "Manutenção de notebook alterado",
                Amount = 400.00m,
                Deadline = new DateOnly(2026, 8, 30),
                OpeningDate = DateOnly.FromDateTime(DateTime.Today),
                TechnicianId = 1,
                CustomerId = 1,
                CategoryId = 1,
                EquipmentId = 1
            };

            // Act
            var result = await _controller.Put(id, updateWorkOrder);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task PutWorkOrder_Return_BadRequest()
        {
            var id = 0;

            var myWorkOrder = new UpdateWorkOrderDTO
            {
                Description = "Troca da tela do notebook alterado",
                Title = "Manutenção de notebook alterado",
                Amount = 400.00m,
                Deadline = new DateOnly(2026, 8, 30),
                OpeningDate = DateOnly.FromDateTime(DateTime.Today),
                TechnicianId = 1,
                CustomerId = 1,
                CategoryId = 1,
                EquipmentId = 1
            };

            var data = await _controller.Put(id, myWorkOrder);

            data.Result.Should().BeOfType<BadRequestResult>()
                .Which.StatusCode.Should().Be(400);
        }
    }
}
