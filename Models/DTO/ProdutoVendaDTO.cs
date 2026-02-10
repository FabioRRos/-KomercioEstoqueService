using System.Text.Json.Serialization;

namespace KomercioApi.Models.DTO
{
    public class ProdutoVendaDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("product_name")]
        public string Nome { get; set; }
        [JsonPropertyName("product_price")]
        public decimal Preco { get; set; }
        [JsonPropertyName("product_codbar")]
        public string? CodigoBarras { get; set; }
        [JsonPropertyName("product_group")]
        public string? Grupo { get; set; }
        [JsonPropertyName("product_stock")]
        public int SaldoDisponivel { get; set; } // Esse campo é calculado
        [JsonPropertyName("product_status")]
        public bool Status { get; set; }
    }
}
