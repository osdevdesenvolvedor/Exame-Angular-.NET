using Exame.Domain.Entities;
using Exame.Domain.Interfaces;
using Exame.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Exame.Infrastructure.Repositories;

public class MovimentoManualRepository : Repository<MovimentoManual>, IMovimentoManualRepository
{
    public MovimentoManualRepository(ExameDbContext ctx) : base(ctx) { }

    public async Task<int> GetMaxLancamentoAsync(int mes, int ano, CancellationToken ct = default)
    {
        var q = _db.Where(m => m.DAT_MES == mes && m.DAT_ANO == ano);
        if (!await q.AnyAsync(ct)) return 0;
        return await q.MaxAsync(m => m.NUM_LANCAMENTO, ct);
    }
}
