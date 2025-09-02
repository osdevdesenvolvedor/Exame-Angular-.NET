using Exame.Application.DTOs;
using Exame.Application.Services;
using Exame.Domain.Entities;
using Exame.Domain.Interfaces;
using Moq;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace Exame.Tests;

public class MovimentoManualServiceTests
{
    [Fact]
    public async Task Incluir_DeveGerarNumeroLancamentoSequencial()
    {
        var movRepo = new Mock<IMovimentoManualRepository>();
        var prodRepo = new Mock<IRepository<Produto>>();
        var cosifRepo = new Mock<IRepository<ProdutoCosif>>();

        movRepo.Setup(m => m.GetMaxLancamentoAsync(8, 2025, default)).ReturnsAsync(3);
        prodRepo.Setup(p => p.Query()).Returns(new[] {
            new Produto { COD_PRODUTO = "PROD01", DES_PRODUTO = "Produto 1", STA_STATUS = "A" }
        }.AsQueryable());
        cosifRepo.Setup(c => c.Query()).Returns(new[] {
            new ProdutoCosif { COD_PRODUTO = "PROD01", COD_COSIF = "COS01", COD_CLASSIFICACAO = "0001", STA_STATUS = "A" }
        }.AsQueryable());

        var service = new MovimentoManualService(movRepo.Object, prodRepo.Object, cosifRepo.Object);

        var dto = await service.IncluirAsync(new IncluirMovimentoManualDto {
            DAT_MES = 8, DAT_ANO = 2025, COD_PRODUTO = "PROD01", COD_COSIF = "COS01", VAL_VALOR = 100m, DES_DESCRICAO = "Teste"
        });

        Assert.Equal(4, dto.NUM_LANCAMENTO);
        movRepo.Verify(m => m.AddAsync(It.IsAny<MovimentoManual>()), Times.Once);
        movRepo.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }
}
