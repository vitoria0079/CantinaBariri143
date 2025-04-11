using System.ComponentModel.DataAnnotations;

namespace CantinaBariri143.Models
{
    public class Pedidos
    {
        public Guid PedidosId { get; set; }

        [Display(Name = "Alimentos")]
        [Required(ErrorMessage = "O campo Alimentos é obrigatório.")]
        public Guid AlimentosId { get; set; }
        public Alimentos? Alimentos { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public int Qtd { get; set; }

        [Display(Name = "Data do Pedido")]
        [Required(ErrorMessage = "O campo Data do Pedido é obrigatório.")]
        public DateTime DataPedido { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "O campo Total é obrigatório.")]
        public string Total { get; set; }
    }
}
