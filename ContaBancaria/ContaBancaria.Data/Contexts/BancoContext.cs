using ContaBancaria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ContaBancaria.Data.Contexts
{
    public class BancoContext : DbContext
    {
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<ExtratoConta> ExtratoContas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
