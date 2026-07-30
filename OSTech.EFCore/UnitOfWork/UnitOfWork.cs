using OSTech.EFCore.Context;
using OSTech.WebAPI.Repositories;

namespace OSTech.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITechnicianRepository? _technicianRepo;
        private ICategoryRepository? _categoryRepo;
        private IEquipmentRepository? _equipmentRepo;
        private IWorkOrderRepository? _workOrderRepo;
        private ICustomerRepository? _customerRepo;

        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public ITechnicianRepository TechnicianRepository
        {
            get
            {
                return _technicianRepo ??= new TechnicianRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepo ??= new CategoryRepository(_context);
            }
        }

        public IEquipmentRepository EquipmentRepository
        {
            get
            {
                return _equipmentRepo ??= new EquipmentRepository(_context);
            }
        }

        public IWorkOrderRepository WorkOrderRepository
        {
            get
            {
                return _workOrderRepo ??= new WorkOrderRepository(_context);
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepo ??= new CustomerRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
