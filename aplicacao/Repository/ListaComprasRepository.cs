using KomercioApi.Data;
using KomercioApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace KomercioApi.Repository
{

    public interface IItensListaComprasRepository
    {
        //Create
        Task<ListaComprasDTO> CriarListaComprasRepository(ListaComprasDTO novaLista);

        //Read
        Task<IEnumerable<ListaComprasDTO>> GetTodasAsListasDeComprasRepository();
        Task<ListaComprasDTO> GetListasDeComprasById(int id);
        Task<IEnumerable<ListaComprasDTO>> GetListasDeComprasAtivas();
        Task<IEnumerable<ListaComprasDTO>> GetListasDeComprasInativas();

        //Post
        Task<ListaComprasDTO> AlterarListaDeCompraById(ListaComprasDTO listaAtualizada);

    }

    public class ListaComprasRepository: IItensListaComprasRepository
    {
        private readonly AppDbContext _appDbContext;

        public ListaComprasRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Create
        /// <summary>
        /// Crio uma nova lista de vendas.
        /// </summary>
        /// <param name="novaLista"></param>
        /// <returns></returns>

        public async Task<ListaComprasDTO> CriarListaComprasRepository(ListaComprasDTO novaLista)
        {
            var listaResponse = new ListaComprasDTO
            {
                NomeDaLista = novaLista.NomeDaLista,
                DataCriacaoLista = novaLista.DataCriacaoLista,
                StatusLista = true

            };

            _appDbContext.listas.Add(listaResponse);

            await _appDbContext.SaveChangesAsync();

            novaLista.IdListaCompra = listaResponse.IdListaCompra;

            return novaLista; 

        }


        //Read (leitura ou GET).

        /// <summary>
        /// Busca todas as listas de compras.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ListaComprasDTO>> GetTodasAsListasDeComprasRepository()
        {
            var response = await _appDbContext.listas.ToListAsync();

            if (response == null)
            {
                return response;// Para criar depois os logs.
            }

            return response;
        }

        /// <summary>
        /// Busca as listas por ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ListaComprasDTO> GetListasDeComprasById(int id)
        {
            var response = await _appDbContext.listas
        .FirstOrDefaultAsync(x => x.IdListaCompra == id);

          if (response == null)
            {
                return response;// para criar logs depois.
            }
            
            return response;
        }

        /// <summary>
        /// Busca listas abertas (não concluidas)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ListaComprasDTO>> GetListasDeComprasAtivas()
        {
            var response = await _appDbContext.listas
        .Where(x => x.StatusLista == true).ToListAsync();

            if (response == null)
            {
                return response;// para criar logs depois.
            }

            return response;
        }

        /// <summary>
        /// Busca listas fechadas (concluidas ou canceladas)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ListaComprasDTO>> GetListasDeComprasInativas()
        {
            var response = await _appDbContext.listas
        .Where(x => x.StatusLista == false).ToListAsync();

            if (response == null)
            {
                return response;// para criar logs depois.
            }

            return response;
        }



        //Update

        /// <summary>
        /// Atualiza a lista (todos os itens) por Id.
        /// </summary>
        /// <param name="listaAtualizada"></param>
        /// <returns></returns>
        public async Task<ListaComprasDTO>AlterarListaDeCompraById(ListaComprasDTO listaAtualizada)
        {

            var lista = await _appDbContext.listas.FindAsync(listaAtualizada.IdListaCompra);

            if (lista == null)
            {
                return listaAtualizada;
            }
            _appDbContext.Entry(listaAtualizada).CurrentValues.SetValues(listaAtualizada);

            await _appDbContext.SaveChangesAsync();

            return listaAtualizada;

        }

    }
}
