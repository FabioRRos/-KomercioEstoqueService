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
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("nomeproduto")]
        [JsonPropertyName("product_name")]

        public string Nome { get; set; } = string.Empty;

        [Column("descricaoproduto")]
        public string? Descricao { get; set; }

        [Column("codigo_barras")]
        [JsonPropertyName("product_codbar")]

        public string? CodigoBarras { get; set; }

        [Column("grupo")]
        [JsonPropertyName("product_group")]

        public string? Grupo { get; set; }

        [Column("preco_venda")]
        [JsonPropertyName("product_price")]

        public decimal PrecoVenda { get; set; } 

        [Column("ativo")]
        [JsonPropertyName("product_status")]

        public bool Ativo { get; set; }

        // Relacionamento (Navigation Property)
        [JsonIgnore]
        public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    }
}
