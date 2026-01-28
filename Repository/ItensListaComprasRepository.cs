using KomercioApi.Data;
using KomercioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KomercioApi.Repository
{
    public interface IListaCompraRepository
    {
        //Select
        Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasRepository();
        Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasAtivasRepository();
        Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasPorIdRepository(int id);

        //Post
        Task<ItensListaComprasDTO> PostAdicionarProdutoNaListaDeComprasRepository(ItensListaComprasDTO listaComprasDTO);

        //Update
        Task<ItensListaComprasDTO> UpdateAlterarProdutoNaListaDeComprasRepository(ItensListaComprasDTO listaComprasDTO);

    }

    public class ItensListaComprasRepository : IListaCompraRepository
    {

        private readonly AppDbContext _appDbContext;

        public ItensListaComprasRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Sessão Select

        /// <summary>
        /// Retorna todas as listas de compras independente do id ou status.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasRepository()
        {
            var lista = await _appDbContext.listadecompras.ToListAsync();
            return lista;
        }

        /// <summary>
        /// Retorna todas as listas de compras ativas.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasAtivasRepository()
        {
            var lista = await _appDbContext.listadecompras
        .Where(x => x.StatusItem == true)
        .ToListAsync();
            return lista;
        }

        /// <summary>
        /// Retorna a lista do id informado (bom para pegar uma lista especifica).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItensListaComprasDTO>> GetListaComprasPorIdRepository(int id)
        {
            var lista = await _appDbContext.listadecompras
        .Where(x => x.IdLista == id)
        .ToListAsync();
            return lista;
        }


        // Sessão Insert

        /// <summary>
        /// Adiciona um novo item na lista de produtos.
        /// </summary>
        /// <param name="listaComprasDTO"></param>
        /// <returns></returns>
        public async Task<ItensListaComprasDTO> PostAdicionarProdutoNaListaDeComprasRepository(ItensListaComprasDTO listaComprasDTO)
        {
            var novaCompra = new ItensListaComprasDTO
            {
                IdLista = listaComprasDTO.IdLista,
                DescricaoProduto = listaComprasDTO.DescricaoProduto,
                CodBar = listaComprasDTO.CodBar,
                Quantidade = listaComprasDTO.Quantidade,
                StatusItem = listaComprasDTO.StatusItem,
                Obs = listaComprasDTO.Obs
            };

            _appDbContext.listadecompras.Add(novaCompra);

            await _appDbContext.SaveChangesAsync();

            listaComprasDTO.IdItemCompra = novaCompra.IdItemCompra;

            return listaComprasDTO;

        }

        //Sessão Update

        /// <summary>
        /// Atualiza um produto da lista pelo Id do item na lista de compras.
        /// </summary>
        /// <param name="listaComprasDTO"></param>
        /// <returns></returns>

        public async Task<ItensListaComprasDTO> UpdateAlterarProdutoNaListaDeComprasRepository(ItensListaComprasDTO listaComprasDTO)
        {
            var lista = await _appDbContext.listadecompras.FindAsync(listaComprasDTO.IdItemCompra);

            if (lista == null)
            {
                return listaComprasDTO;
            }
            _appDbContext.Entry(listaComprasDTO).CurrentValues.SetValues(listaComprasDTO);

            await _appDbContext.SaveChangesAsync();

            return listaComprasDTO;
        }


        /// <summary>
        /// Atualiza os itens da lista pelo seu Id.
        /// </summary>
        /// <param name="idLista"></param>
        /// <param name="listaComprasDTO"></param>
        /// <returns></returns>
        public async Task<ItensListaComprasDTO> UpdateAlterarStatusProdutoNaListaDeComprasRepository(int idLista,ItensListaComprasDTO listaComprasDTO)
        {
            var lista = await _appDbContext.listadecompras.FindAsync(idLista);

            if (lista == null)
            {
                return listaComprasDTO;
            }
            _appDbContext.Entry(listaComprasDTO).CurrentValues.SetValues(listaComprasDTO);

            await _appDbContext.SaveChangesAsync();

            return listaComprasDTO;
        }

    }
}
