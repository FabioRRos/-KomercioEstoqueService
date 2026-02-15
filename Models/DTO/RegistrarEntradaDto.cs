using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomercioApi.Models.DTO
{
    public class RegistrarEntradaDto
    {
        [Required]
        [JsonPropertyName("CodBar")]
        public string? CodigoBarras { get; init; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        [JsonPropertyName("quantidade")]
        public int Quantidade { get; init; }

        [Required]
       /// [Range(0.01, double.MaxValue, ErrorMessage = "O preço de custo deve ser maior que zero")]
        [JsonPropertyName("preco_custo")]
        public decimal PrecoCusto { get; init; }

        [Required]
        [JsonPropertyName("numero_nota")]
        public string NumeroNota { get; init; } = string.Empty;

    }
}
