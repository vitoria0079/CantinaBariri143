using System.ComponentModel.DataAnnotations;

namespace CantinaBariri143.Models
{
    public class Compras
    {
        public Guid ComprasId { get; set; }

        [Display(Name = "Fornecedores")]
        [Required(ErrorMessage = "O campo Fornecedores é obrigatório.")]
        public Guid FornecedoresId { get; set; }
        public Fornecedores? Fornecedores { get; set; }

        [Display(Name = "Alimentos")]
        [Required(ErrorMessage = "O campo Alimentos é obrigatório.")]
        public Guid AlimentosId { get; set; }
        public Alimentos? Alimentos { get; set; }

        [Display(Name = "Preço Unitário")]
        [Required(ErrorMessage = "O campo Preço Unitário é obrigatório.")]
        public double PrecoUnitario { get; set; }

        [Display(Name = "Data da Compra")]
        [Required(ErrorMessage = "O campo Data da Compra é obrigatório.")]
        public DateTime DataCompra { get; set; }

        [Display(Name = "Quantidade Unitária")]
        [Required(ErrorMessage = "O campo Quantidade Unitária é obrigatório.")]
        public int QtdUnitaria { get; set; }

    }
}
