using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias {get;set;}

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    protected override void OnModelCreating(ModelBuilder Builder)
    {
        Builder.Entity<Produto>()
            .Property(p => p.Descricao).HasMaxLength(500).IsRequired(false);
        Builder.Entity<Produto>()
            .Property(p => p.Nome).HasMaxLength(120).IsRequired();
        Builder.Entity<Produto>()
            .Property(p => p.Codigo).HasMaxLength(50).IsRequired(false);
        Builder.Entity<Categoria>()
            .ToTable("Categorias");
    }  
}
