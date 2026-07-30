namespace OSTech.WebAPI.Dtos.Equipment
{
    public class EquipmentDTO
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
    }
}
