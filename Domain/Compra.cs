namespace ApiTeste.Domain
{
    public class Compra
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCompra { get; set; } = DateTime.UtcNow;
    }
}
