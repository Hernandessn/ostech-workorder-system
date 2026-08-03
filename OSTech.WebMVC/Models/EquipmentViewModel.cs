using System.ComponentModel.DataAnnotations;

namespace OSTech.WebMVC.Models
{
    public class EquipmentViewModel
    {
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "O nome do equipamento é obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [Display(Name = "Marca")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [Display(Name = "Modelo")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número de série é obrigatório.")]
        [Display(Name = "Número de Série")]
        public string SerialNumber { get; set; } = string.Empty;
    }
}