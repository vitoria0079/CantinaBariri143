using System.ComponentModel.DataAnnotations;

namespace CantinaBariri143.Models
{
    public class Clientes
    {

        public Guid ClientesId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O Nome deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string CPF { get; set; }

        [Display(Name = "Restrição")]
        [Required(ErrorMessage = "O campo Restrição é obrigatório.")]
        public string Restricao { get; set; }
    }
}
