using Exame.Application.DTOs;

namespace Exame.Application.Services;

public interface IMovimentoManualService
{
    Task<IEnumerable<MovimentoManualDto>> ListarPorMesAnoAsync(int mes, int ano, CancellationToken ct = default);
    Task<MovimentoManualDto> IncluirAsync(IncluirMovimentoManualDto input, CancellationToken ct = default);
    Task<IEnumerable<ProdutoDto>> ListarProdutosAsync(CancellationToken ct = default);
    Task<IEnumerable<ProdutoCosifDto>> ListarCosifPorProdutoAsync(string codProduto, CancellationToken ct = default);
}
