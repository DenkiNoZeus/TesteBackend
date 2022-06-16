using Microsoft.EntityFrameworkCore;

namespace TesteBackend.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Equipes> Equipes { get; set; } = null!;
        public DbSet<Funcionarios> Funcionarios { get; set; } = null!;
    }
}