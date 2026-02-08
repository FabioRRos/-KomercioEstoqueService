using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KomercioApi.Models.Entidade
{
    [Table("produtos")]
    public class Produto
    {
        [Key]
        [Column("idproduto")]
        public int Id { get; set; }

        [Column("nomeproduto")]
        public string Nome { get; set; } = string.Empty;

        [Column("descricaoproduto")]
        public string? Descricao { get; set; }

        [Column("codigo_barras")]
        public string? CodigoBarras { get; set; }

        [Column("grupo")]
        public string? Grupo { get; set; }

        [Column("preco_venda")]
        public decimal PrecoVenda { get; set; } 

        [Column("ativo")]
        public bool Ativo { get; set; }

        // Relacionamento (Navigation Property)
        [JsonIgnore]
        public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    }
}
