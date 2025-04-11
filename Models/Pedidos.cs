namespace CantinaBariri143.Models
{
    public class Pedidos
    {
        public Guid PedidosId { get; set; }

        public Guid AlimentosId { get; set; }
        public Alimentos? Alimentos { get; set; }

        public int Qtd { get; set; }
        public DateTime DataPedido { get; set; }
        public string Total { get; set; }
    }
}
