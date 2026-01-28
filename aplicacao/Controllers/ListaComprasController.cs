using KomercioApi.Interface;
using KomercioApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KomercioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListaComprasController : ControllerBase
    {

        private readonly IListaComprasService _listaComprasService;

        public ListaComprasController(IListaComprasService listaComprasService)
        {
            _listaComprasService = listaComprasService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarListaComprasController(ListaComprasDTO listaCompras)
        {
            var response = await _listaComprasService.CriarListaComprasService(listaCompras);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaComprasDTO>>> GetTodasAsListasDeComprasController()
        {
            var response = await _listaComprasService.GetTodasAsListasDeComprasService();

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<ListaComprasDTO>> GetListasDeComprasByIdController(int id)
        {
            var response = await _listaComprasService.GetListasDeComprasByIdService(id);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("ativas")]
        public async Task<ActionResult<IEnumerable<ListaComprasDTO>>> GetListasDeComprasAtivasController()
        {
            var response = await _listaComprasService.GetListasDeComprasAtivasService();

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("inativas")]
        public async Task<ActionResult<IEnumerable<ListaComprasDTO>>> GetListasDeComprasInativasController()
        {
            var response = await _listaComprasService.GetListasDeComprasInativasService();

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpPut]
        public async Task<ActionResult<ListaComprasDTO>> AlterarListaDeCompraByIdController(ListaComprasDTO listaAtualizada)
        {
            var response = await _listaComprasService.AlterarListaDeCompraByIdService(listaAtualizada);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }






    }
}
