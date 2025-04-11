namespace CantinaBariri143.Models
{
    public class Compras
    {
        public Guid ComprasId { get; set; }

        public Guid FornecedoresId { get; set; }
        public Fornecedores? Fornecedores { get; set; }

        public Guid AlimentosId { get; set; }
        public Alimentos? Alimentos { get; set; }

        public double PrecoUnitario { get; set; }
        public DateTime DataCompra { get; set; }
        public int QtdUnitaria { get; set; }

    }
}
