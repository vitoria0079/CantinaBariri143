using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CantinaBariri143.Models
{
    public class Alimentos
    {
        public Guid AlimentosId { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [StringLength(100, ErrorMessage = "A Descrição deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "A Descrição deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "A Descrição deve ter no máximo 100 caracteres.")]
        public string Descricao { get; set; }

        [Display(Name = "Preço Unitário")]
        [Required(ErrorMessage = "O campo Preço Unitário é obrigatório.")]
        public double PrecoUnitario { get; set; }

        [Display(Name = "Data de Validade")]
        [Required(ErrorMessage = "O campo Data de Validade é obrigatório.")]
        public DateTime Validade { get; set; }

        [Display(Name = "Quantidade do Estoque")]
        [Required(ErrorMessage = "O campo Quantidade do Estoque é obrigatório.")]
        public int QtdEstoque { get; set; }

        [Display(Name = "Restrições")]
        [Required(ErrorMessage = "O campo Restrições é obrigatório.")]
        public string Restricoes { get; set; }

    }
}
