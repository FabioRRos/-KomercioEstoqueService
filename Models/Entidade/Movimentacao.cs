using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KomercioApi.Models.Entidade
{
    [Table("movimentacoes")]
    public class Movimentacao
    {
        [Key]
        [Column("idmovimentacoes")]
        public int Id { get; set; }

        [Column("id_produto")]
        public int IdProduto { get; set; }

        [Column("id_lote")]
        public int? IdLote { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; } = string.Empty; // Ou use um Enum convertido

        [Column("quantidade")]
        public int Quantidade { get; set; }

        [Column("valor_unitario")]
        public decimal ValorUnitario { get; set; }

        [Column("id_referencia")]
        public string? IdReferencia { get; set; }

        [Column("data_movimentacao")]
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;
    }
}
