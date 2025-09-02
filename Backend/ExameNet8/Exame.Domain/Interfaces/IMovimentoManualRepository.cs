namespace Exame.Domain.Interfaces;

public interface IMovimentoManualRepository : IRepository<Exame.Domain.Entities.MovimentoManual>
{
    Task<int> GetMaxLancamentoAsync(int mes, int ano, CancellationToken ct = default);
}
