using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomercioApi.Models.DTO
{
    public record CriarProdutoDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [JsonPropertyName("product_name")]
        public string Nome { get; init; } = string.Empty;

        [JsonPropertyName("descricao")]
        public string? Descricao { get; init; }

        [Required]
        [JsonPropertyName("product_codbar")]
        public string CodigoBarras { get; init; } = string.Empty;

        [JsonPropertyName("product_group")]
        public string? Grupo { get; init; }

        [Required]
        [JsonPropertyName("product_price")]
        public decimal PrecoVenda { get; init; }

        [JsonPropertyName("product_status")]
        public bool Ativo { get; init; } = true;
    }
}
