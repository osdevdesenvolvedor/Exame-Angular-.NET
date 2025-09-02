namespace Exame.Domain.Entities;

public class ProdutoCosif
{
    public string COD_PRODUTO { get; set; } = default!;
    public string COD_COSIF { get; set; } = default!;
    public string COD_CLASSIFICACAO { get; set; } = default!;
    public string STA_STATUS { get; set; } = default!;

    public Produto? Produto { get; set; }
}
