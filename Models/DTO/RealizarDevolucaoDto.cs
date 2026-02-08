using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomercioApi.Models.DTO
{
    public class RealizarDevolucaoDto
    {
        [Required]
        [JsonPropertyName("produto_id")]
        public int ProdutoId { get; init; }

        [Required]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("quantidade")]
        public int Quantidade { get; init; }

        [Required]
        [JsonPropertyName("id_venda_original")]
        public string IdVendaOriginal { get; init; } = string.Empty;
    }
}
