namespace Exame.Domain.Entities;

public class MovimentoManual
{
    public int DAT_MES { get; set; }
    public int DAT_ANO { get; set; }
    public int NUM_LANCAMENTO { get; set; }

    public string COD_PRODUTO { get; set; } = default!;
    public string COD_COSIF { get; set; } = default!;
    public decimal VAL_VALOR { get; set; }
    public string DES_DESCRICAO { get; set; } = default!;
    public DateTime DAT_MOVIMENTO { get; set; }
    public string COD_USUARIO { get; set; } = default!;

    public Produto? Produto { get; set; }
    public ProdutoCosif? ProdutoCosif { get; set; }
}
