using KomercioApi.Data;
using KomercioApi.Interface;
using KomercioApi.Models.DTO;
using KomercioApi.Models.Entidade;
using Microsoft.EntityFrameworkCore;

namespace KomercioApi.Service
{
    public class EstoqueService : IEstoqueService
    {
        private readonly AppDbContext _context;

        public EstoqueService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Entrada de um produto em teoria (tanto em estoque quanto na mivimentação).
        /// </summary>
        /// <param name="produtoId"></param>
        /// <param name="quantidade"></param>
        /// <param name="precoCusto"></param>
        /// <param name="numeroNota"></param>
        /// <returns></returns>
        public async Task RegistrarEntradaAsync(RegistrarEntradaDto dto)
        {
            var novoLote = new Lote
            {
                IdProduto = dto.ProdutoId,
                QuantidadeOriginal = dto.Quantidade, //A quantidade de entrada em estoque é a mesma.
                QuantidadeAtual = dto.Quantidade, // quantidade do contador. Depois com a venda vou tirando.
                PrecoCompra = dto.PrecoCusto,
                DataEntrada = DateTime.UtcNow,
                Observacao = $"NF: {dto.NumeroNota}"
            };

            _context.Lotes.Add(novoLote);


            //O ID do lote só existe após o SaveChanges, então salvamos em duas etapas ou usamos transação
            try
            {
                // AQUI É A HORA DA VERDADE
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Isso pega o erro original do PostgreSQL (Ex: "column 'precocompra' does not exist")
                var mensagemBanco = ex.InnerException?.Message ?? ex.Message;

                // Joga o erro na tela do  Swagger. IMPORTANTE!!! Barrar esse erro no golang.
                throw new Exception($"ERRO DETALHADO DO BANCO: {mensagemBanco}");
            }

            var movimento = new Movimentacao
            {
                IdProduto = dto.ProdutoId,
                IdLote = novoLote.Id,
                Tipo = "COMPRA", // Entrada de Estoque
                Quantidade = dto.Quantidade,
                ValorUnitario = dto.PrecoCusto,
                IdReferencia = dto.NumeroNota,
                DataMovimentacao = DateTime.UtcNow
            };

            _context.Movimentacoes.Add(movimento);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// venda e baixa no estoque (First in fist out).
        /// </summary>
        /// <param name="produtoId"></param>
        /// <param name="quantidade"></param>
        /// <param name="idVenda"></param>
        /// <returns></returns>
        public async Task RealizarVendaAsync(int produtoId, int quantidade, string idVenda)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Busca lotes com saldo > 0 ordenados por data (O mais velho primeiro)
                var lotes = await _context.Lotes
                    .Where(l => l.IdProduto == produtoId && l.QuantidadeAtual > 0)
                    .OrderBy(l => l.DataEntrada)
                    .ToListAsync();

                int qtdRestante = quantidade;

                foreach (var lote in lotes)
                {
                    if (qtdRestante == 0) break;

                    int qtdBaixar = Math.Min(lote.QuantidadeAtual, qtdRestante);

                    // Atualiza o lote
                    lote.QuantidadeAtual -= qtdBaixar;
                    _context.Lotes.Update(lote);

                    // Gera Movimentação de Saída
                    var movimento = new Movimentacao
                    {
                        IdProduto = produtoId,
                        IdLote = lote.Id,
                        Tipo = "VENDA",
                        Quantidade = -qtdBaixar, // Negativo na saída
                        ValorUnitario = lote.PrecoCompra, // Mantém o custo histórico (Importante para lucro!)
                        IdReferencia = idVenda,
                        DataMovimentacao = DateTime.UtcNow
                    };
                    _context.Movimentacoes.Add(movimento);

                    qtdRestante -= qtdBaixar;
                }

                if (qtdRestante > 0)
                    throw new Exception($"Estoque insuficiente! Faltam {qtdRestante} unidades.");

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Devolução do estoque (posso usar para trocas também).
        /// </summary>
        /// <param name="produtoId"></param>
        /// <param name="quantidade"></param>
        /// <param name="idVendaOriginal"></param>
        /// <returns></returns>
        public async Task RealizarDevolucaoAsync(int produtoId, int quantidade, string idVendaOriginal)
        {
            // 1. Tenta achar o custo original daquela venda específica
            var movimentoOriginal = await _context.Movimentacoes
                .FirstOrDefaultAsync(m => m.IdReferencia == idVendaOriginal
                                       && m.IdProduto == produtoId
                                       && m.Tipo == "VENDA");

            decimal custoConsiderado;

            if (movimentoOriginal != null)
            {
                // Se deu bom: Achamos a venda original
                custoConsiderado = movimentoOriginal.ValorUnitario;
            }
            else
            {
                // Não achou o valor original, e agora?
                // Busca o CUSTO da ÚLTIMA entrada (Lote mais recente)
                var ultimoLote = await _context.Lotes
                    .Where(l => l.IdProduto == produtoId)
                    .OrderByDescending(l => l.DataEntrada) // Pega o mais novo
                    .FirstOrDefaultAsync();

                // Se tiver lote, usa o preço dele. Se nunca houve entrada, infelizmente é 0.
                custoConsiderado = ultimoLote?.PrecoCompra ?? 0;
            }

            // 2. Cria o novo lote de devolução com esse custo
            var loteDevolucao = new Lote
            {
                IdProduto = produtoId,
                QuantidadeOriginal = quantidade,
                QuantidadeAtual = quantidade,
                PrecoCompra = custoConsiderado, // <--- Aqui entra o valor corrigido
                DataEntrada = DateTime.UtcNow,
                Observacao = $"Devolução Venda {idVendaOriginal} (Ref: {(movimentoOriginal == null ? "Custo Atual" : "Custo Original")})"
            };

            _context.Lotes.Add(loteDevolucao);
            await _context.SaveChangesAsync();

            // 3. Registra na movimentação.
            var mov = new Movimentacao
            {
                IdProduto = produtoId,
                IdLote = loteDevolucao.Id,
                Tipo = "DEVOLUCAO",
                Quantidade = quantidade,
                ValorUnitario = custoConsiderado,
                IdReferencia = idVendaOriginal,
                DataMovimentacao = DateTime.UtcNow
            };

            _context.Movimentacoes.Add(mov);
            await _context.SaveChangesAsync();
        }

        // busca o estoque do produto.
        public async Task<int> ObterSaldoAtualAsync(int produtoId)
        {
            return await _context.Lotes
                .Where(l => l.IdProduto == produtoId)
                .SumAsync(l => l.QuantidadeAtual);
        }
    }
}
