using System.ComponentModel.DataAnnotations;

namespace OSTech.WebMVC.Models
{
    public class TechnicianViewModel
    {
        public int TechnicianId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [Display(Name = "Especialidade")]
        public string Specialty { get; set; } = string.Empty;

        [Required(ErrorMessage = "O contato é obrigatório.")]
        [Display(Name = "Contato")]
        public string Contact { get; set; } = string.Empty;

        [Display(Name = "Disponível")]
        public bool Availability { get; set; }

        [Required(ErrorMessage = "A data de contratação é obrigatória.")]
        [Display(Name = "Data de Contratação")]
        [DataType(DataType.Date)]
        public DateOnly HiringDate { get; set; }
    }
}