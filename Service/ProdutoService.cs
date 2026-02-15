using KomercioApi.Data;
using KomercioApi.Interface;
using KomercioApi.Models.DTO;
using KomercioApi.Models.Entidade;
using Microsoft.EntityFrameworkCore;

namespace KomercioApi.Service
{
    public class ProdutoService: IProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GET Produtos (ativos)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos
                                 .Where(p => p.Ativo) // Só traz os ativos
                                 .Include(p => p.Lotes)
                                 .ToListAsync();
        }
        /// <summary>
        /// GET Produtos por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        /// <summary>
        /// GET por código de barras.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public async Task<ProdutoVendaDTO?> ObterPorCodigosync(string codigo)
        {
            return await _context.Produtos
        .Where(p => p.CodigoBarras == codigo)
        .Select(p => new ProdutoVendaDTO
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.PrecoVenda,
            CodigoBarras = p.CodigoBarras,
            Grupo = p.Grupo,
            SaldoDisponivel = _context.Lotes
                .Where(l => l.Id == p.Id && l.QuantidadeAtual > 0)
                .Sum(l => (int?)l.QuantidadeAtual) ?? 0,
            Status = p.Ativo
        })
        .FirstOrDefaultAsync();
        }
        /// <summary>
        /// Adiciona produtos ao estoque (Vulgo entrada estoque)
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>"
        public async Task AdicionarAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Atualiza produtos
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public async Task AtualizarAsync(Produto produto)
        {
          //  bool existe = await ExisteCodigoBarrasAsync(produto.CodigoBarras);
         //   if (existe)
          //  {
               
         //       throw new ($"Já existe um produto cadastrado com o código de barras: {produto.CodigoBarras}");
         //   }

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Serve para buscar o codigo de barras (pra não duplicar)
        /// </summary>
        /// <param name="codigoBarras"></param>
        /// <returns></returns>
        public async Task<bool> ExisteCodigoBarrasAsync(string codigoBarras)
        {
            // Retorna true se encontrar algum produto com esse código
            return await _context.Produtos
                .AnyAsync(p => p.CodigoBarras == codigoBarras);
        }
        /// <summary>
        /// Desativar produtos (teoricamente é o delet logico pra não perder histórico).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeativarAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                produto.Ativo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
