using OSTech.Domain.Entities;
using OSTech.WebAPI.Filters;
using OSTech.WebAPI.Pagination;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public interface ITechnicianRepository : IRepository<Technician>
    {
        Task<Technician?> Update(Technician technician);
        Task<PagedList<Technician>> GetTechnicians(TechnicianParameters technicianParams);
        Task<IEnumerable<Technician>> Filter(TechnicianFilterParameters parameters);
    }
}
