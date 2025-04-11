namespace CantinaBariri143.Models
{
    public class Vendas
    {
        public Guid VendasId { get; set; }

        public Guid ClientesId { get; set; }
        public Clientes? Clientes { get; set; }

        public Guid PedidosId { get; set; }
        public Pedidos? Pedidos { get; set; }

        public DateTime DataVenda { get; set; }
        public string Total { get; set; }
    }
}
