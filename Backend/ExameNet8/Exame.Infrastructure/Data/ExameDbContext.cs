using Exame.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Exame.Infrastructure.Data;

public class ExameDbContext(DbContextOptions<ExameDbContext> options) : DbContext(options)
{
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<ProdutoCosif> ProdutosCosif => Set<ProdutoCosif>();
    public DbSet<MovimentoManual> Movimentos => Set<MovimentoManual>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(b =>
        {
            b.ToTable("PRODUTO");
            b.HasKey(p => p.COD_PRODUTO);
            b.Property(p => p.COD_PRODUTO).HasMaxLength(11).IsRequired();
            b.Property(p => p.DES_PRODUTO).HasMaxLength(50).IsRequired();
            b.Property(p => p.STA_STATUS).HasMaxLength(1).IsRequired();
        });

        modelBuilder.Entity<ProdutoCosif>(b =>
        {
            b.ToTable("PRODUTO_COSIF");
            b.HasKey(pc => new { pc.COD_PRODUTO, pc.COD_COSIF });
            b.Property(pc => pc.COD_PRODUTO).HasMaxLength(11).IsRequired();
            b.Property(pc => pc.COD_COSIF).HasMaxLength(11).IsRequired();
            b.Property(pc => pc.COD_CLASSIFICACAO).HasMaxLength(6).IsRequired();
            b.Property(pc => pc.STA_STATUS).HasMaxLength(1).IsRequired();

            b.HasOne(pc => pc.Produto)
             .WithMany(p => p.ProdutoCosifs)
             .HasForeignKey(pc => pc.COD_PRODUTO);
        });

        modelBuilder.Entity<MovimentoManual>(b =>
        {
            b.ToTable("MOVIMENTO_MANUAL");
            b.HasKey(m => new { m.DAT_MES, m.DAT_ANO, m.NUM_LANCAMENTO });
            b.Property(m => m.COD_PRODUTO).HasMaxLength(11).IsRequired();
            b.Property(m => m.COD_COSIF).HasMaxLength(11).IsRequired();
            b.Property(m => m.DES_DESCRICAO).HasMaxLength(50).IsRequired();
            b.Property(m => m.COD_USUARIO).HasMaxLength(15).IsRequired();
            b.Property(m => m.VAL_VALOR).HasPrecision(18, 2).IsRequired();

            b.HasOne(m => m.Produto).WithMany()
             .HasForeignKey(m => m.COD_PRODUTO);

            b.HasOne(m => m.ProdutoCosif).WithMany()
             .HasForeignKey(m => new { m.COD_PRODUTO, m.COD_COSIF });
        });
    }
}
