namespace OSTech.WebAPI.Dtos.WorkOrder
{
    public class CreateWorkOrderDTO
    {
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateOnly Deadline { get; set; }
        public DateOnly OpeningDate { get; set; }

        public int TechnicianId { get; set; }
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
        public int EquipmentId { get; set; }
    }
}
