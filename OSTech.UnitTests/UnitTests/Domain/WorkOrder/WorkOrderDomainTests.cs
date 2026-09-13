using FluentAssertions;
using OSTech.Domain.Exceptions;

namespace OSTech.Tests.UnitTests.Domain.WorkOrder;

public class WorkOrderDomainTests
{
    private static OSTech.Domain.Entities.Technician CreateValidTechnician(bool available = true)
    {
        var technician = new OSTech.Domain.Entities.Technician(
            "João Técnico", "Hardware", "11999999999", available, new DateOnly(2020, 1, 1));

        // TechnicianId só é setado pelo EF Core na persistência real;
        // aqui, simulamos isso via reflection, só para permitir teste em memória.
        typeof(OSTech.Domain.Entities.Technician)
            .GetProperty(nameof(OSTech.Domain.Entities.Technician.TechnicianId))!
            .SetValue(technician, 1);

        return technician;
    }

    private static OSTech.Domain.Entities.WorkOrder CreateValidWorkOrder(OSTech.Domain.Entities.Technician? technician = null)
    {
        return new OSTech.Domain.Entities.WorkOrder(
            "Descrição de teste",
            "Título de teste",
            150m,
            DateOnly.FromDateTime(DateTime.Today).AddDays(30),
            DateOnly.FromDateTime(DateTime.Today),
            technician ?? CreateValidTechnician(),
            customerId: 1,
            categoryId: 1,
            equipmentId: 1
        );
    }

    [Fact]
    public void Constructor_CreatesWorkOrder_WithStatusOpen()
    {
        var workOrder = CreateValidWorkOrder();

        workOrder.Status.Should().Be(OSTech.Domain.Enums.StatusWorkOrder.Open);
    }

    [Fact]
    public void AssignTechnician_ThrowsDomainException_WhenTechnicianIsNull()
    {
        Action act = () => CreateValidWorkOrder(null!).AssignTechnician(null!);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void AssignTechnician_ThrowsDomainException_WhenTechnicianUnavailable()
    {
        var unavailableTechnician = CreateValidTechnician(available: false);

        Action act = () => new OSTech.Domain.Entities.WorkOrder(
            "Desc", "Titulo", 100m,
            DateOnly.FromDateTime(DateTime.Today).AddDays(10),
            DateOnly.FromDateTime(DateTime.Today),
            unavailableTechnician, 1, 1, 1);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Start_ChangesStatusToInProgress_WhenOpen()
    {
        var workOrder = CreateValidWorkOrder();

        workOrder.Start();

        workOrder.Status.Should().Be(OSTech.Domain.Enums.StatusWorkOrder.InProgress);
    }

    [Fact]
    public void Start_ThrowsInvalidWorkOrderStatusException_WhenNotOpen()
    {
        var workOrder = CreateValidWorkOrder();
        workOrder.Start(); // agora está InProgress

        Action act = () => workOrder.Start();

        act.Should().Throw<InvalidWorkOrderStatusException>();
    }

    [Fact]
    public void Complete_ThrowsInvalidWorkOrderStatusException_WhenNotInProgress()
    {
        var workOrder = CreateValidWorkOrder(); // ainda Open

        Action act = () => workOrder.Complete();

        act.Should().Throw<InvalidWorkOrderStatusException>();
    }

    [Fact]
    public void Complete_ChangesStatusToCompleted_WhenInProgress()
    {
        var workOrder = CreateValidWorkOrder();
        workOrder.Start();

        workOrder.Complete();

        workOrder.Status.Should().Be(OSTech.Domain.Enums.StatusWorkOrder.Completed);
    }

    [Fact]
    public void Cancel_ThrowsInvalidWorkOrderStatusException_WhenAlreadyCancelled()
    {
        var workOrder = CreateValidWorkOrder();
        workOrder.Cancel();

        Action act = () => workOrder.Cancel();

        act.Should().Throw<InvalidWorkOrderStatusException>();
    }

    [Fact]
    public void Cancel_ThrowsInvalidWorkOrderStatusException_WhenCompleted()
    {
        var workOrder = CreateValidWorkOrder();
        workOrder.Start();
        workOrder.Complete();

        Action act = () => workOrder.Cancel();

        act.Should().Throw<InvalidWorkOrderStatusException>();
    }

    [Fact]
    public void ChangeDeadline_ThrowsDomainException_WhenDeadlineBeforeOpeningDate()
    {
        var workOrder = CreateValidWorkOrder();

        Action act = () => workOrder.ChangeDeadline(DateOnly.FromDateTime(DateTime.Today).AddDays(-1));

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void ChangeDeadline_ThrowsInvalidWorkOrderStatusException_WhenNotOpen()
    {
        var workOrder = CreateValidWorkOrder();
        workOrder.Start();

        Action act = () => workOrder.ChangeDeadline(DateOnly.FromDateTime(DateTime.Today).AddDays(45));

        act.Should().Throw<InvalidWorkOrderStatusException>();
    }
}
