using Exame.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exame.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController(IMovimentoManualService service) : ControllerBase
{
    private readonly IMovimentoManualService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetProdutos(CancellationToken ct)
        => Ok(await _service.ListarProdutosAsync(ct));

    [HttpGet("{codProduto}/cosifs")]
    public async Task<IActionResult> GetCosifs(string codProduto, CancellationToken ct)
        => Ok(await _service.ListarCosifPorProdutoAsync(codProduto, ct));
}
