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

        // GET: api/produtos (ok no go)
        // Retorna a lista para o Grid JÁ COM O SALDO SOMADO (Via DTO)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoListagemDto>>> ListarTodos()
        {// 1. Busca os protudos do banco (vem com todos os dados)
            var produtosEntidade = await _produtoService.ObterTodosAsync();

            // 2.(Converte Entidade -> DTO)
            var produtosDto = produtosEntidade.Select(p => new ProdutoListagemDto
            {
                Id = p.Id,
                Nome = p.Nome,
                PrecoVenda = p.PrecoVenda,
                CodigoBarras = p.CodigoBarras,
                Grupo = p.Grupo,
                // O LINQ soma a coluna 'QuantidadeAtual' de todos os lotes deste produto
                SaldoTotal = p.Lotes != null ? p.Lotes.Sum(l => l.QuantidadeAtual) : 0,
                Ativo = p.Ativo
            });

            return Ok(produtosDto);
        }

        // GET: api/produtos//5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> ObterPorId(int id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        // GET: api/produtos/5
        [HttpGet("cod/{cod}")]
        public async Task<ActionResult<ProdutoVendaDTO>> ObterPorcod(string cod)
        {
            var produto = await _produtoService.ObterPorCodigosync(cod);
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
        public async Task<ActionResult> Criar([FromBody] CriarProdutoDto dto)
        {

                if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // Mapeamento DTO -> Entidade
                var novoProduto = new Produto
                {
                    Nome = dto.Nome,
                    Descricao = dto.Descricao,
                    CodigoBarras = dto.CodigoBarras,
                    Grupo = dto.Grupo, 
                    PrecoVenda = dto.PrecoVenda,
                    Ativo = dto.Ativo
                };


                await _produtoService.AdicionarAsync(novoProduto);

                return CreatedAtAction(nameof(ObterPorId), new { id = novoProduto.Id }, novoProduto);
            }
            catch (Exception ex)
            {
                // Retorna 400 Bad Request com a sua frase personalizada
                return BadRequest(new { mensagem = ex.Message });
            }

        }

        // PUT: api/produtos/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] Produto produto)
        {
            try
            {
                if (id != produto.Id) return BadRequest("ID do corpo difere do ID da URL");

                await _produtoService.AtualizarAsync(produto);

                return StatusCode(200, produto);
            }
            catch
            {
                return NoContent();
            }
           

            
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
