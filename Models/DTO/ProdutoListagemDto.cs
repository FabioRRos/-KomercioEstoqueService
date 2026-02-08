using System.Text.Json.Serialization;

namespace KomercioApi.Models.DTO
{
    public class ProdutoListagemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("product_name")]
        public string Nome { get; init; } = string.Empty;

        [JsonPropertyName("product_price")]
        public decimal PrecoVenda { get; init; }
        [JsonPropertyName("product_codbar")]
        public string CodigoBarras { get; init; } = string.Empty;

        [JsonPropertyName("product_group")]
        public string? Grupo { get; init; }

        // O campo mágico calculado (Soma dos lotes)
        [JsonPropertyName("product_stock")]
        public int SaldoTotal { get; init; }
        [JsonPropertyName("product_status")]
        public bool Ativo { get; init; }
    }
}
