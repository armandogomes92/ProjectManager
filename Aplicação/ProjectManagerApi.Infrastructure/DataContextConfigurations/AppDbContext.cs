using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Models.DataModels;

namespace ProjectManagerApi.Infrastructure.DataContextConfigurations;

public class AppDbContext : DbContext
{
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Usuario> Users { get; set; }
    public DbSet<Domain.Models.DataModels.Tarefa> Tasks { get; set; }
    public DbSet<Comentario> Comments { get; set; }
    public DbSet<HistoricoDaTarefa> TaskHistories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Projeto>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Projeto>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projetos)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Domain.Models.DataModels.Tarefa>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Domain.Models.DataModels.Tarefa>()
            .HasOne(t => t.Projeto)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjetoId);

        modelBuilder.Entity<Comentario>()
            .HasKey(c => new { c.TarefaId, c.UsuarioId });

        modelBuilder.Entity<Comentario>()
            .HasOne(c => c.Task)
            .WithMany(t => t.comentarios)
            .HasForeignKey(c => c.TarefaId);

        modelBuilder.Entity<Comentario>()
            .HasOne(c => c.Usuario)
            .WithMany(u => u.Comentarios)
            .HasForeignKey(c => c.UsuarioId);

        modelBuilder.Entity<HistoricoDaTarefa>()
           .HasKey(t => t.Id);

        modelBuilder.Entity<HistoricoDaTarefa>()
            .HasOne(t => t.Task)
            .WithMany(p => p.HistoricoDaTarefa)
            .HasForeignKey(t => t.TaskId);
    }
}