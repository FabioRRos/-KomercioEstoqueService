using KomercioApi.Interface;
using KomercioApi.Models;
using KomercioApi.Repository;

namespace KomercioApi.Service
{
    public class ListaComprasService: IListaComprasService
    {
        private readonly IItensListaComprasRepository _repository;

        public ListaComprasService(IItensListaComprasRepository repository)
        {
            _repository = repository;
        }

        // Create
        /// <summary>
        /// Cria uma nova lista. Com essa lista criada eu consigo o ID para cadastrar os itens desta lista.
        /// </summary>
        /// <param name="novaLista"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ListaComprasDTO>> CriarListaComprasService(ListaComprasDTO novaLista)
        {
            var response = new ServiceResponse<ListaComprasDTO>();
            // chamo a validação. Se der certo GG se não, eu retorno erro com a mensagem do que deu errado.
            try
            {
                response = ListaComprasDTO.ValidaLista(novaLista);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
                return response;
            }

            // tento salvar no banco
            response.Dados =  await _repository.CriarListaComprasRepository(novaLista);
            // se der ruim, falo pra tentar de novo
            if (response.Dados == null)
            {
                response.Mensagem = "Não consegui criar a lista, tente novamente";
                response.Sucesso = false;

                return response;
            }
            // se não, sucesso GGWP.
            response.Sucesso = true;
            response.Mensagem = "Lista criada com sucesso!";

            return response;

        }

        // Read
        /// <summary>
        /// Busca todas as litas (ativas e inativas).
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<IEnumerable<ListaComprasDTO>>> GetTodasAsListasDeComprasService()
        {

            var response = new ServiceResponse<IEnumerable<ListaComprasDTO>>();


            response.Dados =   await _repository.GetTodasAsListasDeComprasRepository();

            if (response.Dados == null)
            {
                response.Sucesso = false;
                response.Mensagem = "Não consegui trazer as listas";
                return response;
            }


            response.Sucesso = true;
            response.Mensagem = "Listas carregadas com sucesso!";

            return response;
        }

        /// <summary>
        /// Busca a lista pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ListaComprasDTO>> GetListasDeComprasByIdService(int id)
        {
            var response = new ServiceResponse<ListaComprasDTO>();

            if (id == 0)
            {
                response.Sucesso = false;
                response.Mensagem = "Id nulo ou invalido";
                response.Dados = new ListaComprasDTO();
                return response;

            }


            response.Dados =  await _repository.GetListasDeComprasById(id);


            if (response.Dados == null)
            {
                response.Sucesso= false;
                response.Mensagem = "Não consegui carregar a lista dos dados";
                return response;
            }



            response.Sucesso = true;
            response.Mensagem = $"Lista {response.Dados.NomeDaLista} carregada com sucesso!";
            return response;
        }

        /// <summary>
        /// Retorna as listas ativas (status = true).
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<IEnumerable<ListaComprasDTO>>> GetListasDeComprasAtivasService()
        {
            var response = new ServiceResponse<IEnumerable<ListaComprasDTO>>();

            response.Dados = await _repository.GetListasDeComprasAtivas();

            if (response.Dados == null)
            {
                response.Sucesso = false;
                response.Mensagem = "Não consegui trazer as listas";
                return response;
            }


            response.Sucesso = true;
            response.Mensagem = "Listas carregadas com sucesso!";

            return response;
        }

        /// <summary>
        /// Retorna as listas inativas (status = false).
        /// </summary>
        /// <returns></returns>

        public async Task<ServiceResponse<IEnumerable<ListaComprasDTO>>> GetListasDeComprasInativasService()
        {
            var response = new ServiceResponse<IEnumerable<ListaComprasDTO>>();

            response.Dados = await _repository.GetListasDeComprasInativas();


            if (response.Dados == null)
            {
                response.Sucesso = false;
                response.Mensagem = "Não consegui trazer as listas";
                return response;
            }


            response.Sucesso = true;
            response.Mensagem = "Listas carregadas com sucesso!";

            return response;
        }

        // Update
        /// <summary>
        /// Atualiza o objeto como um todo, não item a item.
        /// </summary>
        /// <param name="listaAtualizada"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ListaComprasDTO>> AlterarListaDeCompraByIdService(ListaComprasDTO listaAtualizada)
        {

           var response = new ServiceResponse<ListaComprasDTO>();
            // chamo a validação. Se der certo GG se não, eu retorno erro com a mensagem do que deu errado.
            try
            {
                response = ListaComprasDTO.ValidaLista(listaAtualizada);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
                return response;
            }


            response.Dados =  await _repository.AlterarListaDeCompraById(listaAtualizada);

            if (response.Dados == null)
            {
                response.Mensagem = "Não consegui criar a lista, tente novamente";
                response.Sucesso = false;

                return response;
            }

            response.Sucesso = true;
            response.Mensagem = "Lista atualizada com sucesso!";

            return response;

        }
    }
}
