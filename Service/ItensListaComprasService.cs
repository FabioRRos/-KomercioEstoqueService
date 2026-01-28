using KomercioApi.Interface;
using KomercioApi.Models;
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

        public async Task<ServiceResponse<IEnumerable<ItensListaComprasDTO>>> GetListaComprasService()
        {

            var response = new ServiceResponse<IEnumerable<ItensListaComprasDTO>>();

            response.Dados = await _listaCompraRepository.GetListaComprasRepository();

            if (response.Dados == null)
            {
                response.Mensagem = "Não conseguimos carregar a lista, tente novamente";
                response.Sucesso = false;
            }

            response.Mensagem = "ok";
            response.Sucesso = true;

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ItensListaComprasDTO>>> GetListaComprasAtivasService()
        {
            var response = new ServiceResponse<IEnumerable<ItensListaComprasDTO>>();


            response.Dados = await _listaCompraRepository.GetListaComprasAtivasRepository();


            if (response.Dados == null)
            {
                response.Mensagem = "Não conseguimos carregar a lista, tente novamente";
                response.Sucesso = false;
            }

            response.Mensagem = "ok";
            response.Sucesso = true;

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ItensListaComprasDTO>>> GetListaComprasPorIdService(int id)
        {
            var response = new ServiceResponse<IEnumerable<ItensListaComprasDTO>>();


            if (id >= 0)
            {
                response.Mensagem = "Id invalido, nulo ou inexistente.";
                response.Sucesso = false;

                return response;
            }

            response.Dados = await _listaCompraRepository.GetListaComprasPorIdRepository(id);

            if (response.Dados == null)
            {
                response.Mensagem = "Não conseguimos carregar a lista, tente novamente";
                response.Sucesso = false;
            }

            response.Mensagem = "ok";
            response.Sucesso = true;

            return response;

        }

        // Sessão Insert

        public async Task<ServiceResponse<ItensListaComprasDTO>> PostAdicionarProdutoNaListaDeComprasService(ItensListaComprasDTO listaComprasDTO)
        {

            var response = new ServiceResponse<ItensListaComprasDTO>();

            try
            {
                response = ItensListaComprasDTO.ValidaItemListaCompras(listaComprasDTO);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
                return response;
            }

            try
            {
            response.Dados = await _listaCompraRepository.PostAdicionarProdutoNaListaDeComprasRepository(listaComprasDTO);

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Não consegui adicionar o produto: {ex}";
                response.Sucesso = false;
                return response;
            }

            if (response.Dados == null)
            {
                response.Mensagem = "Não consegui adicionar o item da lista";
                response.Sucesso = false;
                return response;
            }


            response.Mensagem = "Item adicionado com sucesso!";
            response.Sucesso = true;

            return response;

        }

        // Sessão Update

        public async Task<ServiceResponse<ItensListaComprasDTO>> UpdateAlterarProdutoNaListaDeComprasService(ItensListaComprasDTO listaComprasDTO)
        {
            var response = new ServiceResponse<ItensListaComprasDTO>();

            try
            {
                response = ItensListaComprasDTO.ValidaItemListaCompras(listaComprasDTO);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
                return response;
            }

            response.Dados =  await _listaCompraRepository.UpdateAlterarProdutoNaListaDeComprasRepository(listaComprasDTO);

            if (response.Dados == null)
            {
                response.Mensagem = "Não consegui atualizar o item na lista";
                response.Sucesso = false;
                return response;
            }


            response.Mensagem = $"{response.Dados.DescricaoProduto} atualizado com sucesso!";
            response.Sucesso = true;

            return response;

        }
    }
}

