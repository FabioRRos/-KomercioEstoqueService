using KomercioApi.Models;

namespace KomercioApi.Interface
{
    public interface IListaComprasService
    {
        // Create
        Task<ServiceResponse<ListaComprasDTO>> CriarListaComprasService(ListaComprasDTO novaLista);

        // Read
        Task<ServiceResponse<IEnumerable<ListaComprasDTO>>> GetTodasAsListasDeComprasService();
        Task<ServiceResponse<ListaComprasDTO>> GetListasDeComprasByIdService(int id);
        Task<ServiceResponse<IEnumerable<ListaComprasDTO>>> GetListasDeComprasAtivasService();
        Task<ServiceResponse<IEnumerable<ListaComprasDTO>>> GetListasDeComprasInativasService();

        // Update
        Task<ServiceResponse<ListaComprasDTO>> AlterarListaDeCompraByIdService(ListaComprasDTO listaAtualizada);
    }
}
