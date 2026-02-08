using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomercioApi.Models.DTO
{
    public class RealizarVendaDto
    {
        [Required]
        [JsonPropertyName("produto_id")]
        public int ProdutoId { get; init; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        [JsonPropertyName("quantidade")]
        public int Quantidade { get; init; }

        [Required]
        [JsonPropertyName("id_venda")]
        public string IdVenda { get; init; } = string.Empty;
    }
}
