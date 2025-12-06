using Microsoft.EntityFrameworkCore;
using PersonalAssistantApi.Domain.Entities;

namespace PersonalAssistantApi.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Conta> Contas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(b =>
        {
            b.HasKey(u => u.Id);
            b.Property(u => u.Nome).HasField("_nome");

            b.HasMany(u => u.Tarefas).WithOne(t => t.Usuario).HasForeignKey(t => t.UsuarioId);
            b.HasMany(u => u.Contas).WithOne(c => c.Usuario).HasForeignKey(c => c.UsuarioId);

            b.Navigation(u => u.Tarefas).UsePropertyAccessMode(PropertyAccessMode.Field);
            b.Navigation(u => u.Contas).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<Tarefa>(b =>
        {
            b.HasKey(t => t.Id);
            b.Property(t => t.Titulo).HasField("_titulo");
            b.Property(t => t.Data).HasField("_data");
            b.Property(t => t.Concluida).HasField("_concluida");
            b.Property(t => t.UsuarioId).HasField("_usuarioId");
        });

        modelBuilder.Entity<Conta>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Descricao).HasField("_descricao");
            b.Property(c => c.Valor).HasField("_valor").HasColumnType("decimal(18,2)");
            b.Property(c => c.DataVencimento).HasField("_dataVencimento");
            b.Property(c => c.UsuarioId).HasField("_usuarioId");
            b.Property(c => c.Pago).HasField("_pago");
        });
    }
}
