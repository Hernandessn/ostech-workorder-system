using System.ComponentModel.DataAnnotations;

namespace OSTech.WebMVC.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Display(Name = "Telefone")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O documento é obrigatório.")]
        [Display(Name = "Documento")]
        public string Document { get; set; } = string.Empty;
    }
}