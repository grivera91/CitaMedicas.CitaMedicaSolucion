using CitaMedicas.CitaMedicaApi.Model;
using CitasMedicas.CitaMedicaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CitaMedicas.CitaMedicaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CitaMedica> CitasMedicas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }        
        public DbSet<Correlativo> Correlativos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }                
        public DbSet<TipoPago> TipoPagos { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CitaMedica>().ToTable("CitaMedica");
            modelBuilder.Entity<Paciente>().ToTable("Paciente");
            modelBuilder.Entity<Medico>().ToTable("Medico");            
            modelBuilder.Entity<Correlativo>().ToTable("Correlativo");
            modelBuilder.Entity<Horario>().ToTable("Horario");
            modelBuilder.Entity<Especialidad>().ToTable("Especialidad");            
            modelBuilder.Entity<TipoPago>().ToTable("TipoPago");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
