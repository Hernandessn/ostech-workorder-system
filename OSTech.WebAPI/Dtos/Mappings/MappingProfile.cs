using AutoMapper;
using OSTech.Domain.Entities;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Dtos.Customer;
using OSTech.WebAPI.Dtos.Equipment;
using OSTech.WebAPI.Dtos.Technician;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.WebAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Technician, TechnicianDTO>();

            CreateMap<Category, CategoryDTO>();

            CreateMap<Customer, CustomerDTO>();

            CreateMap<Equipment, EquipmentDTO>();

            CreateMap<WorkOrder, WorkOrderDTO>();
        }
    }
}
