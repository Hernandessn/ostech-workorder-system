using Microsoft.EntityFrameworkCore;
using OSTech.Domain.Entities;
using OSTech.EFCore.Context;
using OSTech.WebAPI.Filters;
using OSTech.WebAPI.Pagination;
using OSTech.WebAPI.Repositories.Generic;

namespace OSTech.WebAPI.Repositories
{
    public class TechnicianRepository : Repository<Technician>, ITechnicianRepository
    {
        public TechnicianRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Technician>> Filter(
           TechnicianFilterParameters parameters)
        {
            var query = _context.Technicians
                .AsNoTracking()
                .AsQueryable();

            if (parameters.Availability.HasValue)
                query = query.Where(p => p.Availability == parameters.Availability);

            if (!string.IsNullOrWhiteSpace(parameters.Specialty))
                query = query.Where(p => p.Specialty == parameters.Specialty);

            if (!string.IsNullOrWhiteSpace(parameters.Name))
                query = query.Where(p => p.Name == parameters.Name);

            return await query.ToListAsync();
        }

        public async Task<PagedList<Technician>> GetTechnicians(TechnicianParameters technicianParams)
        {
            var technicians = _context.Technicians
                .AsNoTracking()
                .OrderBy(t => t.TechnicianId);

            return await PagedList<Technician>.ToPagedListAsync(
                technicians,
                technicianParams.PageNumber,
                technicianParams.PageSize);
        }

        public async Task<Technician?> Update(Technician technician)
        {
            var technicianDb = await _context.Technicians
                .FirstOrDefaultAsync(t => t.TechnicianId == technician.TechnicianId);

            if (technicianDb is null)
                return null;

            technicianDb.SetName(technician.Name);
            technicianDb.SetSpecialty(technician.Specialty);
            technicianDb.SetContact(technician.Contact);
            technicianDb.SetAvailability(technician.Availability);
            technicianDb.SetHiringDate(technician.HiringDate);

            return technicianDb;
        }

    }
}
