namespace KomercioApi.Interface
{
    public interface IItensListaComprasService
    {
        // Select
        Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasService();
        Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasAtivasService();
        Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasPorIdService(int id);

        // Post
        Task<ItensListaComprasDTO> PostAdicionarProdutoNaListaDeComprasService(ItensListaComprasDTO listaComprasDTO);

        // Update
        Task<ItensListaComprasDTO> UpdateAlterarProdutoNaListaDeComprasService(ItensListaComprasDTO listaComprasDTO);
    }
}
