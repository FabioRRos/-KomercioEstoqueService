using KomercioApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KomercioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensListaComprasController : ControllerBase
    {

        private readonly IItensListaComprasService _itensListaComprasService;

        public ItensListaComprasController(IItensListaComprasService itensListaComprasService)
        {
            _itensListaComprasService = itensListaComprasService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItensListaComprasDTO>>>GetListaComprasController()
        {
            var response = await _itensListaComprasService.GetListaComprasService();

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpGet("ativas")]
        public async Task<ActionResult<IEnumerable<ItensListaComprasDTO>>> GetListaComprasAtivasController()
        {
            var response = await _itensListaComprasService.GetListaComprasAtivasService();

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpGet("id/{id}")]
        public async Task<ActionResult<IEnumerable<ItensListaComprasDTO>>> GetListaComprasPorIdController(int id)
        {
            if (id <=0)
            {
                return BadRequest("id invalido!");
            }
            var response = await _itensListaComprasService.GetListaComprasPorIdService(id);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task <ActionResult<ItensListaComprasDTO>> PostAdicionarProdutoNaListaDeComprasController(ItensListaComprasDTO itensDaLista)
        {

           var response = await _itensListaComprasService.PostAdicionarProdutoNaListaDeComprasService(itensDaLista);

            if (!response.Sucesso)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }


        [HttpPut]
        public async Task<ActionResult<ItensListaComprasDTO>> UpdateAlterarProdutoNaListaDeComprasController(ItensListaComprasDTO itensDaLista)
        {
            try
            {
                var response = await _itensListaComprasService.UpdateAlterarProdutoNaListaDeComprasService(itensDaLista);

                if (!response.Sucesso)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch
            {
                return StatusCode(500, "Não consegui seguir com a solicitação");
            }

        }

    }
}
