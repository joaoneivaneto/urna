using Microsoft.EntityFrameworkCore;

namespace CandidatosApi.Models
{
    public class CandidatosDBContext: DbContext
    {
        public CandidatosDBContext(DbContextOptions<CandidatosDBContext> options) : base(options)
        {

        }

        public DbSet<RegistroCandidato> RegistroCandidatos { get; set; }

        public DbSet<Voto> votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<RegistroCandidato>().HasKey(x => x.Id);
            modelBuilder.Entity<Voto>().HasKey(pk => pk.id);

            modelBuilder.Entity<Voto>()
           .HasOne(p => p.RegistroCandidato)
           .WithMany(b => b.votos)
           .HasForeignKey(p => p.IdCandidato);
        }


    }
}
