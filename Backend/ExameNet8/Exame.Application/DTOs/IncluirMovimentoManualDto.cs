namespace Exame.Application.DTOs;

public class IncluirMovimentoManualDto
{
    public int DAT_MES { get; set; }
    public int DAT_ANO { get; set; }
    public string COD_PRODUTO { get; set; } = string.Empty;
    public string COD_COSIF { get; set; } = string.Empty;
    public decimal VAL_VALOR { get; set; }
    public string DES_DESCRICAO { get; set; } = string.Empty;
}
