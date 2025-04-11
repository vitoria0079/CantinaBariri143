using System.ComponentModel.DataAnnotations;

namespace CantinaBariri143.Models
{
    public class Fornecedores
    {
        public Guid FornecedoresId { get; set; }

        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "O campo Razão Social é obrigatório.")]
        public string RazaoSocial { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        public string CNPJ { get; set; }
    }
}
