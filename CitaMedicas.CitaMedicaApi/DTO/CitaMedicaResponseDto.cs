using System.ComponentModel.DataAnnotations;

namespace CitaMedicas.CitaMedicaApi.DTO
{
    public class CitaMedicaResponseDto
    {        
        public int IdCitaMedica { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string CodigoCitaMedica { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string MotivoConsulta { get; set; }
    }
}