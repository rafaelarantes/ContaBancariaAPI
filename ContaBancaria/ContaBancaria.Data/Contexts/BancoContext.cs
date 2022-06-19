using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContaBancaria.Data.Contexts
{
    public class BancoContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<ExtratoConta> ExtratoContas { get; set; }
        public DbSet<TaxaBancaria> TaxasBancarias { get; set; }

        public BancoContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapearBanco(modelBuilder);
            MapearTaxaBancaria(modelBuilder);
            MapearConta(modelBuilder);
            MapearExtatoConta(modelBuilder);
        }

        private void MapearBanco(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banco>()
                        .ToTable("BANCO")
                        .HasKey(b => b.Guid);


            modelBuilder.Entity<Banco>()
                        .Property(p => p.Guid)
                        .HasColumnName("GUID")
                        .IsRequired();
            
            modelBuilder.Entity<Banco>()
                        .Property(p => p.Nome)
                        .HasColumnName("NOME")
                        .HasColumnType("varchar(100)")
                        .IsRequired();

            modelBuilder.Entity<Banco>()
                        .Property(p => p.Numero)
                        .HasColumnName("NUMERO")
                        .HasColumnType("int")
                        .IsRequired();

            modelBuilder.Entity<Banco>()
                    .Property(p => p.Agencia)
                    .HasColumnName("AGENCIA")
                    .HasColumnType("int")
                    .IsRequired();

            modelBuilder.Entity<Banco>()
                        .HasMany(b => b.TaxasBancarias)
                        .WithOne(t => t.Banco)
                        .HasForeignKey(b => b.GuidBanco)
                        .IsRequired();

            modelBuilder.Entity<Banco>()
                        .HasMany(b => b.Contas)
                        .WithOne(t => t.Banco)
                        .HasForeignKey(b => b.GuidBanco)
                        .IsRequired();
        }

        private void MapearTaxaBancaria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxaBancaria>()
                        .ToTable("TAXA_BANCARIA")
                        .HasKey(t => t.Guid);

            modelBuilder.Entity<TaxaBancaria>()
                        .Property(p => p.Guid)
                        .HasColumnName("GUID")
                        .IsRequired();

            modelBuilder.Entity<TaxaBancaria>()
                        .Property(p => p.Descricao)
                        .HasColumnName("DESCRICAO")
                        .HasColumnType("varchar(100)");

            modelBuilder.Entity<TaxaBancaria>()
                        .Property(p => p.Tipo)
                        .HasColumnName("TIPO")
                        .HasConversion(x => (byte)x, x => (TipoTaxaBancaria)x);

            modelBuilder.Entity<TaxaBancaria>()
                        .Property(p => p.TipoValor)
                        .HasColumnName("TIPO_VALOR")
                        .HasConversion(x => (byte)x, x => (TipoValorTaxaBancaria)x)
                        .IsRequired();

            modelBuilder.Entity<TaxaBancaria>()
                        .Property(p => p.Valor)
                        .HasColumnName("VALOR")
                        .HasColumnType("numeric")
                        .IsRequired();

            modelBuilder.Entity<TaxaBancaria>()
                        .Property(p => p.GuidBanco)
                        .HasColumnName("GUID_BANCO");
        }

        private void MapearConta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>()
                        .ToTable("CONTA")
                        .HasKey(c => c.Guid);
                        
            modelBuilder.Entity<Conta>()
                        .Property(p => p.Guid)
                        .HasColumnName("GUID")
                        .IsRequired();

            modelBuilder.Entity<Conta>()
                       .Property(p => p.Numero)
                       .HasColumnName("NUMERO")
                       .HasColumnType("bigint")
                       .IsRequired();

            modelBuilder.Entity<Conta>()
                        .HasMany(c => c.Extrato)
                        .WithOne(e => e.Conta)
                        .HasForeignKey(e => e.GuidConta);

            modelBuilder.Entity<Conta>()
                        .Property(c => c.GuidBanco)
                        .HasColumnName("GUID_BANCO");
        }

        private void MapearExtatoConta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExtratoConta>()
                        .ToTable("EXTRATO_CONTA")
                        .HasKey(e => e.Guid);

            modelBuilder.Entity<ExtratoConta>()
                        .Property(p => p.Guid)
                        .HasColumnName("GUID")
                        .IsRequired();

            modelBuilder.Entity<ExtratoConta>()
                       .Property(p => p.DataOperacao)
                       .HasColumnName("DATA_OPERACAO")
                       .HasColumnType("datetime")
                       .IsRequired();

            modelBuilder.Entity<ExtratoConta>()
                       .Property(p => p.TipoOperacao)
                       .HasColumnName("TIPO_OPERACAO")
                       .HasConversion(x => (byte)x, x => (TipoOperacaoConta)x)
                       .IsRequired();

            modelBuilder.Entity<ExtratoConta>()
                       .Property(p => p.TipoTaxaBancaria)
                       .HasColumnName("TIPO_TAXA_BANCARIA")
                       .HasConversion(x => (byte)x, x => (TipoTaxaBancaria)x);

            modelBuilder.Entity<ExtratoConta>()
                       .Property(p => p.Valor)
                       .HasColumnName("VALOR")
                       .HasColumnType("money")
                       .IsRequired();

            modelBuilder.Entity<ExtratoConta>()
                        .Property(c => c.GuidConta)
                        .HasColumnName("GUID_CONTA");

            modelBuilder.Entity<ExtratoConta>()
                        .Property(c => c.GuidContaOrigem)
                        .HasColumnName("GUID_CONTA_ORIGEM");
        }
    }
}
