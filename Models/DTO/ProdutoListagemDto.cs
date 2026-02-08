using System.Text.Json.Serialization;

namespace KomercioApi.Models.DTO
{
    public class ProdutoListagemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("nome")]
        // Se no Go estiver 'product_name', mude aqui para "product_name"
        public string Nome { get; init; } = string.Empty;

        [JsonPropertyName("codigo_barras")]
        public string CodigoBarras { get; init; } = string.Empty;

        [JsonPropertyName("preco_venda")]
        public decimal PrecoVenda { get; init; }

        [JsonPropertyName("grupo_id")]
        public int? GrupoId { get; init; }

        // O campo mágico calculado (Soma dos lotes)
        [JsonPropertyName("saldo_total")]
        public int SaldoTotal { get; init; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; init; }
    }
}
