using FluentAssertions;
using OSTech.Domain;
using OSTech.Domain.Entities;
using OSTech.Domain.Enums;
using OSTech.Domain.Exceptions;
using OSTech.Domain.Services;
using Xunit;

namespace OSTech.Tests.UnitTests.Domain.Services;

// Fake simples, sem banco, sem biblioteca de mock — só implementa o contrato
public class FakeWorkOrderRepository : IDomainWorkOrderRepository
{
    private readonly List<OSTech.Domain.Entities.WorkOrder> _workOrders;

    public FakeWorkOrderRepository(List<OSTech.Domain.Entities.WorkOrder> workOrders)
    {
        _workOrders = workOrders;
    }

    public Task<IEnumerable<OSTech.Domain.Entities.WorkOrder>> GetByCategoryId(int categoryId)
    {
        var result = _workOrders.Where(w => w.CategoryId == categoryId);
        return Task.FromResult(result);
    }
}

public class CategoryDomainServiceTests
{
    [Fact]
    public async Task EnsureCategoryCanBeDeletedAsync_ThrowsException_WhenOpenWorkOrderExists()
    {
        var technician = new OSTech.Domain.Entities.Technician("João", "Hardware", "119999", true, new DateOnly(2020, 1, 1));
        typeof(OSTech.Domain.Entities.Technician)
            .GetProperty(nameof(OSTech.Domain.Entities.Technician.TechnicianId))!
            .SetValue(technician, 1);
        var workOrder = new OSTech.Domain.Entities.WorkOrder(
            "Desc", "Titulo", 100m,
            DateOnly.FromDateTime(DateTime.Today).AddDays(10),
            DateOnly.FromDateTime(DateTime.Today),
            technician, 1, categoryId: 5, 1);

        var fakeRepo = new FakeWorkOrderRepository(new List<OSTech.Domain.Entities.WorkOrder> { workOrder });
        var service = new CategoryDomainService(fakeRepo);

        Func<Task> act = () => service.EnsureCategoryCanBeDeletedAsync(5);

        await act.Should().ThrowAsync<CategoryDeletionBlockedException>();
    }

    [Fact]
    public async Task EnsureCategoryCanBeDeletedAsync_DoesNotThrow_WhenNoBlockingWorkOrders()
    {
        var fakeRepo = new FakeWorkOrderRepository(new List<OSTech.Domain.Entities.WorkOrder>());
        var service = new CategoryDomainService(fakeRepo);

        Func<Task> act = () => service.EnsureCategoryCanBeDeletedAsync(999);

        await act.Should().NotThrowAsync();
    }
}