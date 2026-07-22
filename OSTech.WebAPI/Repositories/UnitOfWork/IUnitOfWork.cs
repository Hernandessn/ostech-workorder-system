namespace OSTech.WebAPI.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITechnicianRepository TechnicianRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IEquipmentRepository EquipmentRepository { get; }
        IWorkOrderRepository WorkOrderRepository { get; }   
        ICustomerRepository CustomerRepository { get; }
        Task CommitAsync();
    }
}
