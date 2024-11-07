using System.ComponentModel.DataAnnotations;

namespace CitaMedicas.CitaMedicaApi.Model
{
    public class CitaMedica
    {
        [Key]
        public int IdCitaMedica { get; set; }
        public int IdPaciente { get; set; } // Relación con el paciente
        public int IdMedico { get; set; } // Relación con el médico
        public string CodigoCitaMedica { get; set; }
        public DateTime FechaCita { get; set; } // Fecha de la cita
        public TimeSpan HoraInicio { get; set; } // Hora de inicio de la cita
        public TimeSpan HoraFin { get; set; } // Hora de fin de la cita
        public string MotivoConsulta { get; set; } // Descripción breve del motivo de la cita
        public string EstadoCita { get; set; } = "Pendiente"; // Estado inicial de la cita
        public string UsuarioCreacion { get; set; } = "Sistema";
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
