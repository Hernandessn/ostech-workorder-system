using OSTech.Domain.Entities;
using OSTech.WebAPI.Dtos.Category;
using OSTech.WebAPI.Dtos.Customer;
using OSTech.WebAPI.Dtos.Equipment;
using OSTech.WebAPI.Dtos.Technician;
using OSTech.WebAPI.Dtos.WorkOrder;

namespace OSTech.Application.Mappings
{
    /// <summary>
    /// Entity -> DTO mappings written by hand (replaces AutoMapper, see GHSA-rvv3-g6hj-g44x).
    /// Collection overloads materialize the result with ToList().
    /// </summary>
    public static class MappingExtensions
    {
        public static CategoryDTO ToDto(this Category source) => new()
        {
            CategoryId = source.CategoryId,
            Name = source.Name,
            Description = source.Description
        };

        public static List<CategoryDTO> ToDto(this IEnumerable<Category> source) =>
            source.Select(x => x.ToDto()).ToList();

        public static CustomerDTO ToDto(this Customer source) => new()
        {
            CustomerId = source.CustomerId,
            Name = source.Name,
            // Same behavior as the previous AutoMapper profile: null when the value object is null.
            Email = source.Email?.Address!,
            Phone = source.Phone,
            Document = source.Document?.Number!
        };

        public static List<CustomerDTO> ToDto(this IEnumerable<Customer> source) =>
            source.Select(x => x.ToDto()).ToList();

        public static EquipmentDTO ToDto(this Equipment source) => new()
        {
            EquipmentId = source.EquipmentId,
            Name = source.Name,
            Brand = source.Brand,
            Model = source.Model,
            SerialNumber = source.SerialNumber
        };

        public static List<EquipmentDTO> ToDto(this IEnumerable<Equipment> source) =>
            source.Select(x => x.ToDto()).ToList();

        public static TechnicianDTO ToDto(this Technician source) => new()
        {
            TechnicianId = source.TechnicianId,
            Name = source.Name,
            Specialty = source.Specialty,
            Contact = source.Contact,
            Availability = source.Availability,
            HiringDate = source.HiringDate
        };

        public static List<TechnicianDTO> ToDto(this IEnumerable<Technician> source) =>
            source.Select(x => x.ToDto()).ToList();

        public static WorkOrderDTO ToDto(this WorkOrder source) => new()
        {
            WorkOrderId = source.WorkOrderId,
            Title = source.Title,
            Description = source.Description,
            Amount = source.Amount,
            Deadline = source.Deadline,
            OpeningDate = source.OpeningDate,
            Status = source.Status,
            TechnicianId = source.TechnicianId,
            CustomerId = source.CustomerId,
            CategoryId = source.CategoryId,
            EquipmentId = source.EquipmentId
        };

        public static List<WorkOrderDTO> ToDto(this IEnumerable<WorkOrder> source) =>
            source.Select(x => x.ToDto()).ToList();
    }
}
