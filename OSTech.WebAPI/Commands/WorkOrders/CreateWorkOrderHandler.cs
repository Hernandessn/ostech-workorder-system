using MediatR;
using OSTech.Domain.Entities;
using OSTech.Domain.Exceptions;
using OSTech.Infrastructure.UnitOfWork;

namespace OSTech.WebAPI.Commands.WorkOrders;

public class CreateWorkOrderHandler : IRequestHandler<CreateWorkOrderCommand, WorkOrder>
{
    private readonly IUnitOfWork _uof;

    public CreateWorkOrderHandler(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<WorkOrder> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var technician = await _uof.TechnicianRepository.GetById(t => t.TechnicianId == request.TechnicianId);

        if (technician is null)
            throw new NotFoundDomainException("Técnico not found.");

        var workOrder = new WorkOrder(
            request.Description, request.Title, request.Amount, request.Deadline, request.OpeningDate,
            technician, request.CustomerId, request.CategoryId, request.EquipmentId); // erro ao passar o technician pois ele no construtor ele recebe o objeto tecnico, a solução seria eu colocar no CreateWorkOrderCommand o atributo Technician Technician, posso fazer?

        await _uof.WorkOrderRepository.Create(workOrder);
        await _uof.CommitAsync();

        return workOrder;
    }
}
