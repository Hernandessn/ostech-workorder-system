using AutoMapper;
using OSTech.Domain.Entities;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Dtos.Customer;
using OSTech.WebAPI.Dtos.Equipment;
using OSTech.WebAPI.Dtos.Technician;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Technician, TechnicianDTO>();

            CreateMap<Category, CategoryDTO>();

            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src =>
                    src.Email != null
                        ? src.Email.Address
                        : null
                ))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src =>
                    src.Document != null
                        ? src.Document.Number
                        : null
                ));

            CreateMap<Equipment, EquipmentDTO>();

            CreateMap<WorkOrder, WorkOrderDTO>();
        }
    }
}
