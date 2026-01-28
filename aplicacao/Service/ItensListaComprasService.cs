using KomercioApi.Interface;
using KomercioApi.Repository;

namespace KomercioApi.Service
{
    public class ItensListaComprasService : IItensListaComprasService
    {
        private readonly IListaCompraRepository _listaCompraRepository;

        public ItensListaComprasService(IListaCompraRepository listaCompraRepository)
        {
            _listaCompraRepository = listaCompraRepository;
        }

        // Sessão Select

        public async Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasService()
        {
            return await _listaCompraRepository.GetListaComprasRepository();
        }

        public async Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasAtivasService()
        {
            return await _listaCompraRepository.GetListaComprasAtivasRepository();
        }

        public async Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasPorIdService(int id)
        {
            return await _listaCompraRepository.GetListaComprasPorIdRepository(id);
        }

        // Sessão Insert

        public async Task<ItensListaComprasDTO> PostAdicionarProdutoNaListaDeComprasService(ItensListaComprasDTO listaComprasDTO)
        {
            return await _listaCompraRepository.PostAdicionarProdutoNaListaDeComprasRepository(listaComprasDTO);
        }

        // Sessão Update

        public async Task<ItensListaComprasDTO> UpdateAlterarProdutoNaListaDeComprasService(ItensListaComprasDTO listaComprasDTO)
        {
            return await _listaCompraRepository.UpdateAlterarProdutoNaListaDeComprasRepository(listaComprasDTO);
        }
    }
}

