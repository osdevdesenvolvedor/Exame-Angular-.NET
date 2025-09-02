using Exame.Application.DTOs;
using Exame.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exame.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentosController(IMovimentoManualService service) : ControllerBase
{
    private readonly IMovimentoManualService _service = service;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int mes, [FromQuery] int ano, CancellationToken ct)
    {
        if (mes < 1 || mes > 12) return BadRequest("Mês inválido");
        if (ano < 1900) return BadRequest("Ano inválido");
        var result = await _service.ListarPorMesAnoAsync(mes, ano, ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] IncluirMovimentoManualDto input, CancellationToken ct)
    {
        try
        {
            var result = await _service.IncluirAsync(input, ct);
            return Created($"/api/movimentos?mes={result.DAT_MES}&ano={result.DAT_ANO}", result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
