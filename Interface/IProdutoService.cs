using KomercioApi.Models.DTO;
using KomercioApi.Models.Entidade;

namespace KomercioApi.Interface
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task DeativarAsync(int id);
        Task<bool> ExisteCodigoBarrasAsync(string codigoBarras);
        Task<ProdutoVendaDTO?> ObterPorCodigosync(string codigo);
    }
    public interface IEstoqueService
    {
        // Entrada de mercadoria (Compra)
        Task RegistrarEntradaAsync(RegistrarEntradaDto dto);
        // Saída de mercadoria (Venda FIFO)
        Task RealizarVendaAsync(int produtoId, int quantidade, string idVenda);

        // Devolução (Cliente devolvendo)
        Task RealizarDevolucaoAsync(int produtoId, int quantidade, string idVendaOriginal);

        // Consulta de saldo
        Task<int> ObterSaldoAtualAsync(int produtoId);
    }
}
