namespace OSTech.WebAPI.Dtos.Technician
{
    public class CreateTechnicianDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public bool Availability { get; set; }

        public DateOnly HiringDate { get; set; }
    }
}
