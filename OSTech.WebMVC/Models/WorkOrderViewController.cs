using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OSTech.WebMVC.Models
{
    public class WorkOrderViewModel
    {
        public int WorkOrderId { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Data de Abertura")]
        [DataType(DataType.Date)]
        public DateOnly OpeningDate { get; set; }

        [Required]
        [Display(Name = "Prazo")]
        [DataType(DataType.Date)]
        public DateOnly Deadline { get; set; }

        [Required]
        [Display(Name = "Status")]
        public StatusWorkOrder Status { get; set; }

        [Required]
        [Display(Name = "Técnico")]
        public int TechnicianId { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Equipamento")]
        public int EquipmentId { get; set; }

        public IEnumerable<SelectListItem> Technicians { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Customers { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Equipments { get; set; } = new List<SelectListItem>();
    }
}