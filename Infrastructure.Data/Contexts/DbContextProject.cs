using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts
{
    public class DbContextProject : DbContext
    {
        public DbContextProject(DbContextOptions<DbContextProject> options) : base(options)
        {
        }

        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<StatusTarefa> StatusTarefa { get; set; }
        public DbSet<Prioridade> Prioridade { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }

        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<HistoricoAtualizacao> HistoricoAtualizacao { get; set; }
    }
}
