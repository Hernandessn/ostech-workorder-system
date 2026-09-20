using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.Tests.UnitTests.WorkOder
{
    public class PostWorkOrderUnitTests : IClassFixture<WorkOrderUnitTestController>
    {
        private readonly WorkOrderController _controller;

        public PostWorkOrderUnitTests(WorkOrderUnitTestController controller)
        {
            _controller = new WorkOrderController(
                NullLogger<WorkOrderController>.Instance,
                controller.repository,
                controller.mapper,
                controller.mediator); // precisa existir no fixture
        }

        [Fact]
        public async Task PostWorkOrder_CreatedStatusCode()
        {
            var newWorkOrderDto = new CreateWorkOrderDTO
            {
                Description = "Troca da tela do notebook",
                Title = "Manutenção de notebook",
                Amount = 450.00m,
                OpeningDate = DateOnly.FromDateTime(DateTime.Today),
                Deadline = DateOnly.FromDateTime(DateTime.Today).AddDays(30),
                TechnicianId = 1,
                CustomerId = 1,
                CategoryId = 1,
                EquipmentId = 1
            };

            var data = await _controller.Post(newWorkOrderDto);

            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task PosWorkOrder_Return_BadRequest()
        {
            CreateWorkOrderDTO dto = null;

            var data = await _controller.Post(dto);

            data.Result.Should().BeOfType<BadRequestResult>()
                       .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}