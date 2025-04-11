using System.ComponentModel.DataAnnotations;

namespace CantinaBariri143.Models
{
    public class Vendas
    {
        public Guid VendasId { get; set; }

        [Display(Name = "Clientes")]
        [Required(ErrorMessage = "O campo Clientes é obrigatório.")]
        public Guid ClientesId { get; set; }
        public Clientes? Clientes { get; set; }

        [Display(Name = "Pedidos")]
        [Required(ErrorMessage = "O campo Pedidos é obrigatório.")]
        public Guid PedidosId { get; set; }
        public Pedidos? Pedidos { get; set; }

        [Display(Name = "Data da Venda")]
        [Required(ErrorMessage = "O campo Data da Venda é obrigatório.")]
        public DateTime DataVenda { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "O campo Total é obrigatório.")]
        public string Total { get; set; }
    }
}
