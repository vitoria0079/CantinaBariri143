namespace CantinaBariri143.Models
{
    public class Alimentos
    {
        public Guid AlimentosId { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public DateTime Validade { get; set; }
        public int QtdEstoque { get; set; }
        public string Restricoes { get; set; }
    }
}
