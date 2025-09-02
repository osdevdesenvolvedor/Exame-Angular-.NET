using Exame.Application.DTOs;
using Exame.Domain.Entities;
using Exame.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Exame.Application.Services;

public class MovimentoManualService : IMovimentoManualService
{
    private readonly IMovimentoManualRepository _movRepo;
    private readonly IRepository<Produto> _produtoRepo;
    private readonly IRepository<ProdutoCosif> _cosifRepo;

    public MovimentoManualService(
        IMovimentoManualRepository movRepo,
        IRepository<Produto> produtoRepo,
        IRepository<ProdutoCosif> cosifRepo)
    {
        _movRepo = movRepo;
        _produtoRepo = produtoRepo;
        _cosifRepo = cosifRepo;
    }

    public async Task<IEnumerable<MovimentoManualDto>> ListarPorMesAnoAsync(int mes, int ano, CancellationToken ct = default)
    {
        var query = from m in _movRepo.Query()
                    join p in _produtoRepo.Query() on m.COD_PRODUTO equals p.COD_PRODUTO
                    join c in _cosifRepo.Query() on new { m.COD_PRODUTO, m.COD_COSIF } equals new { c.COD_PRODUTO, c.COD_COSIF }
                    where m.DAT_MES == mes && m.DAT_ANO == ano
                    orderby m.NUM_LANCAMENTO
                    select new MovimentoManualDto(
                        m.DAT_MES, m.DAT_ANO, m.NUM_LANCAMENTO, m.COD_PRODUTO, m.COD_COSIF,
                        m.VAL_VALOR, m.DES_DESCRICAO, m.DAT_MOVIMENTO, m.COD_USUARIO,
                        p.DES_PRODUTO, c.COD_CLASSIFICACAO
                    );
        return await query.ToListAsync(ct);
    }

    public async Task<MovimentoManualDto> IncluirAsync(IncluirMovimentoManualDto input, CancellationToken ct = default)
    {
        if (input is null) throw new ArgumentNullException(nameof(input));
        if (string.IsNullOrWhiteSpace(input.COD_PRODUTO)) throw new ArgumentException("COD_PRODUTO é obrigatório");
        if (string.IsNullOrWhiteSpace(input.COD_COSIF)) throw new ArgumentException("COD_COSIF é obrigatório");
        if (string.IsNullOrWhiteSpace(input.DES_DESCRICAO)) throw new ArgumentException("DES_DESCRICAO é obrigatório");
        if (input.VAL_VALOR <= 0) throw new ArgumentException("VAL_VALOR deve ser maior que zero");

        var produto = await _produtoRepo.Query().FirstOrDefaultAsync(p => p.COD_PRODUTO == input.COD_PRODUTO, ct)
                      ?? throw new InvalidOperationException("Produto inexistente");

        var cosif = await _cosifRepo.Query().FirstOrDefaultAsync(c => c.COD_PRODUTO == input.COD_PRODUTO && c.COD_COSIF == input.COD_COSIF, ct)
                    ?? throw new InvalidOperationException("Cosif não associado ao produto informado");

        var prox = await _movRepo.GetMaxLancamentoAsync(input.DAT_MES, input.DAT_ANO, ct) + 1;

        var entidade = new MovimentoManual
        {
            DAT_MES = input.DAT_MES,
            DAT_ANO = input.DAT_ANO,
            NUM_LANCAMENTO = prox,
            COD_PRODUTO = input.COD_PRODUTO,
            COD_COSIF = input.COD_COSIF,
            VAL_VALOR = input.VAL_VALOR,
            DES_DESCRICAO = input.DES_DESCRICAO,
            DAT_MOVIMENTO = DateTime.Now,
            COD_USUARIO = "TESTE"
        };

        await _movRepo.AddAsync(entidade);
        await _movRepo.SaveChangesAsync(ct);

        return new MovimentoManualDto(
            entidade.DAT_MES, entidade.DAT_ANO, entidade.NUM_LANCAMENTO, entidade.COD_PRODUTO,
            entidade.COD_COSIF, entidade.VAL_VALOR, entidade.DES_DESCRICAO, entidade.DAT_MOVIMENTO,
            entidade.COD_USUARIO, produto.DES_PRODUTO, cosif.COD_CLASSIFICACAO);
    }

    public async Task<IEnumerable<ProdutoDto>> ListarProdutosAsync(CancellationToken ct = default)
        => await _produtoRepo.Query().OrderBy(p => p.DES_PRODUTO)
            .Select(p => new ProdutoDto(p.COD_PRODUTO, p.DES_PRODUTO)).ToListAsync(ct);

    public async Task<IEnumerable<ProdutoCosifDto>> ListarCosifPorProdutoAsync(string codProduto, CancellationToken ct = default)
        => await _cosifRepo.Query().Where(c => c.COD_PRODUTO == codProduto)
            .OrderBy(c => c.COD_COSIF)
            .Select(c => new ProdutoCosifDto(c.COD_PRODUTO, c.COD_COSIF, c.COD_CLASSIFICACAO)).ToListAsync(ct);
}
