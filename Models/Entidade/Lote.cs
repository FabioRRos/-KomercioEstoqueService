using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KomercioApi.Models.Entidade
{
    [Table("lotes")]
    public class Lote
    {
        [Key]
        [Column("idlote")]
        public int Id { get; set; }

        [Column("id_produto")]
        public int IdProduto { get; set; }

        [Column("preco_compra")]
        public decimal PrecoCompra { get; set; }

        [Column("quantidade_original")]
        public int QuantidadeOriginal { get; set; }

        [Column("quantidade_atual")]
        public int QuantidadeAtual { get; set; }

        [Column("data_entrada")]
        public DateTimeOffset DataEntrada { get; set; }

        [Column("observacao")]
        public string? Observacao { get;set;  }


        [ForeignKey("IdProduto")] 
        public Produto? Produto { get; set; }
    }
}
