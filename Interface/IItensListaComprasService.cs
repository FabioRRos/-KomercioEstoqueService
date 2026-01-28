using KomercioApi.Models;

namespace KomercioApi.Interface
{
    public interface IItensListaComprasService
    {
        // Select
        Task<ServiceResponse<IEnumerable<ItensListaComprasDTO>>> GetListaComprasService();
        Task<ServiceResponse<IEnumerable<ItensListaComprasDTO>>> GetListaComprasAtivasService();
        Task<ServiceResponse<IEnumerable<ItensListaComprasDTO>>> GetListaComprasPorIdService(int id);

        // Post
        Task<ServiceResponse<ItensListaComprasDTO>> PostAdicionarProdutoNaListaDeComprasService(ItensListaComprasDTO listaComprasDTO);

        // Update
        Task<ServiceResponse<ItensListaComprasDTO>> UpdateAlterarProdutoNaListaDeComprasService(ItensListaComprasDTO listaComprasDTO);
    }
}
