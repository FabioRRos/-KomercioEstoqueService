using KomercioApi.Interface;
using KomercioApi.Models.DTO;
using KomercioApi.Models.Entidade;
using Microsoft.AspNetCore.Mvc;

namespace KomercioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IEstoqueService _estoqueService;

        public ProdutosController(IProdutoService produtoService, IEstoqueService estoqueService)
        {
            _produtoService = produtoService;
            _estoqueService = estoqueService;
        }

        // GET: api/produtos
        // Retorna a lista para o Grid JÁ COM O SALDO SOMADO (Via DTO)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoListagemDto>>> ListarTodos()
        {
            // Este método no service deve fazer a projeção (Select) somando os lotes
            var produtos = await _produtoService.ObterTodosAsync();
            return Ok(produtos);
        }

        // GET: api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> ObterPorId(int id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        // GET: api/produtos/5/saldo
        // Endpoint específico se o Front quiser checar só o saldo de um item em tempo real
        [HttpGet("{id}/saldo")]
        public async Task<ActionResult<int>> ObterSaldo(int id)
        {
            var saldo = await _estoqueService.ObterSaldoAtualAsync(id);
            return Ok(new { produtoId = id, saldoTotal = saldo });
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<ActionResult> Criar([FromBody] Produto produto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _produtoService.AdicionarAsync(produto);
            return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
        }

        // PUT: api/produtos/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id) return BadRequest("ID do corpo difere do ID da URL");

            await _produtoService.AtualizarAsync(produto);
            return NoContent();
        }

        // DELETE: api/produtos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Desativar(int id)
        {
            await _produtoService.DeativarAsync(id);
            return NoContent();
        }
    }
}
