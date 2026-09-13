using OSTech.Domain.Enums;
using OSTech.Domain.Exceptions;

namespace OSTech.Domain.Services;

public class CategoryDomainService
{
    private readonly IDomainWorkOrderRepository _workOrderRepository;

    public CategoryDomainService(IDomainWorkOrderRepository workOrderRepository)
    {
        _workOrderRepository = workOrderRepository;
    }

    public async Task EnsureCategoryCanBeDeletedAsync(int categoryId)
    {
        var workOrders = await _workOrderRepository.GetByCategoryId(categoryId);

        var hasBlockingOrders = workOrders.Any(w =>
            w.Status == StatusWorkOrder.Open ||
            w.Status == StatusWorkOrder.InProgress);

        if (hasBlockingOrders)
            throw new CategoryDeletionBlockedException(
                "Categoria possui ordens de serviço abertas ou em andamento e não pode ser excluída.");
    }
}