using KomercioApi.Interface;
using KomercioApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KomercioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService _estoqueService;

        public EstoqueController(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        // POST: api/estoque/entrada
        // Usado quando chega Nota Fiscal (Cria novo Lote)
        [HttpPost("entrada")]
        public async Task<IActionResult> RegistrarEntrada([FromBody] RegistrarEntradaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _estoqueService.RegistrarEntradaAsync(dto);
                return Ok(new { message = "Entrada registrada e lotes criados com sucesso." });
            }
            catch (Exception ex)
            {
                // Em produção, use um logger aqui (ex: Serilog)
                return StatusCode(500,new { error = ex.Message });
            }
        }

        // POST: api/estoque/venda
        // O Gateway chama isso ao finalizar o pedido. Roda o algoritmo FIFO.
        [HttpPost("venda")]
        public async Task<IActionResult> RealizarVenda([FromBody] RealizarVendaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _estoqueService.RealizarVendaAsync(dto.ProdutoId, dto.Quantidade, dto.IdVenda);
                return Ok(new { message = "Baixa de estoque (FIFO) realizada com sucesso." });
            }
            catch (Exception ex)
            {
                // Retorna erro se não tiver saldo suficiente nos lotes
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: api/estoque/devolucao
        // Usado quando o cliente devolve o item (Recompõe estoque e ajusta custo)
        [HttpPost("devolucao")]
        public async Task<IActionResult> RealizarDevolucao([FromBody] RealizarDevolucaoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _estoqueService.RealizarDevolucaoAsync(dto.ProdutoId, dto.Quantidade, dto.IdVendaOriginal);
                return Ok(new { message = "Devolução processada e estoque recomposto." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
