namespace Exame.Domain.Entities;

public class Produto
{
    public string COD_PRODUTO { get; set; } = default!;
    public string DES_PRODUTO { get; set; } = default!;
    public string STA_STATUS { get; set; } = default!;
    public ICollection<ProdutoCosif> ProdutoCosifs { get; set; } = new List<ProdutoCosif>();
}
