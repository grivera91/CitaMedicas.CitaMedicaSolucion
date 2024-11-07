using CitaMedicas.CitaMedicaApi.Data;
using CitaMedicas.CitaMedicaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CitaMedicas.CitaMedicaApi.Services
{
    public class CitaMedicaService
    {
        private readonly ApplicationDbContext _context;

        public CitaMedicaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para validar la disponibilidad del horario
        public async Task<bool> ValidarDisponibilidadHorario(CitaMedica cita, int? idCitaExcluida = null)
        {
            return !await _context.CitasMedicas
                .Where(c => c.IdMedico == cita.IdMedico && c.FechaCita == cita.FechaCita)
                .Where(c => idCitaExcluida == null || c.IdCitaMedica != idCitaExcluida)
                .AnyAsync(c =>
                    (c.HoraInicio < cita.HoraFin && cita.HoraInicio < c.HoraFin) // Verifica solapamiento de horarios
                );
        }

        // Método para registrar una nueva cita médica
        public async Task RegistrarCitaMedica(CitaMedica cita)
        {
            await _context.CitasMedicas.AddAsync(cita);
            await _context.SaveChangesAsync();
        }

        // Método para editar una cita médica existente
        public async Task EditarCitaMedica(CitaMedica cita)
        {
            _context.CitasMedicas.Update(cita);
            await _context.SaveChangesAsync();
        }
        
    }
}
