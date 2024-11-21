using CitasMedicas.CitaMedicaApi.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitaMedicas.CitaMedicaApi.Model
{
    public class CitaMedica
    {
        [Key]
        public int IdCitaMedica { get; set; }

        [ForeignKey("Especialidad")]
        public int IdEspecialidad { get; set; }

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }

        [ForeignKey("Horario")]
        public int IdHorario { get; set; }

        public string CodigoCitaMedica { get; set; }

        public DateTime FechaCita { get; set; }

        public string MotivoConsulta { get; set; }

        public string EstadoCita { get; set; } = "Pendiente";

        public int IdTipoPago { get; set; }
        public decimal ImporteNeto { get; set; }
        public decimal Impuesto { get; set; }
        public decimal ImporteTotal { get; set; }
        public DateTime FechaPago { get; set; }
        public string Moneda { get; set; }
        public string CodigoTransaccion { get; set; }

        public string UsuarioCreacion { get; set; } = "Sistema";

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public string? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        // Propiedades de navegación
        public virtual Paciente Paciente { get; set; }
        public virtual Medico Medico { get; set; }
        public virtual Horario Horario { get; set; }
        public virtual Especialidad Especialidad { get; set; }
    }
}
