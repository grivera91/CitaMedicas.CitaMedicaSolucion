using CitaMedicas.CitaMedicaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CitaMedicas.CitaMedicaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CitaMedica> CitasMedicas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<HorarioAtencion> HorariosAtencion { get; set; }
        public DbSet<Correlativo> Correlativos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CitaMedica>().ToTable("CitaMedica");
            modelBuilder.Entity<Paciente>().ToTable("Paciente");
            modelBuilder.Entity<Medico>().ToTable("Medico");
            modelBuilder.Entity<HorarioAtencion>().ToTable("HorarioAtencion");
            modelBuilder.Entity<Correlativo>().ToTable("Correlativo");
            base.OnModelCreating(modelBuilder);
        }
    }
}
