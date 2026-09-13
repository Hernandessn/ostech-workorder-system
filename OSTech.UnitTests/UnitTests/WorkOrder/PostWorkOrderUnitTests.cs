using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using OSTech.WebAPI.Controllers;
using OSTech.WebAPI.Dtos.WorkOrder;
using OSTech.WebAPI.Services;


namespace OSTech.Tests.UnitTests.WorkOder
{
    public class PostWorkOrderUnitTests : IClassFixture<WorkOrderUnitTestController>
    {
        private readonly WorkOrderController _controller;
        private readonly WorkOrderApplicationService _workOrderApplicationService;

        public PostWorkOrderUnitTests(WorkOrderUnitTestController controller)
        {
            _controller = new WorkOrderController(NullLogger<WorkOrderController>.Instance, controller.repository, controller.mapper);
            _workOrderApplicationService = new WorkOrderApplicationService(controller.repository);
        }
        [Fact]
        public async Task PostWorkOrder_CreatedStatusCode()
        {
            // Arrange
            var newWorkOrderDto = new CreateWorkOrderDTO
            {
                Description = "Troca da tela do notebook",
                Title = "Manutenção de notebook",
                Amount = 450.00m,
                OpeningDate = DateOnly.FromDateTime(DateTime.Today),              // abertura hoje
                Deadline = DateOnly.FromDateTime(DateTime.Today).AddDays(30),     // prazo no futuro
                TechnicianId = 1,
                CustomerId = 1,
                CategoryId = 1,
                EquipmentId = 1
            };

            // Act
            var data = await _controller.Post(newWorkOrderDto, _workOrderApplicationService);

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
            var data = await _controller.Post(dto, _workOrderApplicationService);

            // Assert
            data.Result.Should().BeOfType<BadRequestResult>()
                       .Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
