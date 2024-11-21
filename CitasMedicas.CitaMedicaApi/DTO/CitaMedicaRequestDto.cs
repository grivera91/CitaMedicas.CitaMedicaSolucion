using System.ComponentModel.DataAnnotations;

namespace CitaMedicas.CitaMedicaApi.DTO
{
    public class CitaMedicaRequestDto
    {
        [Required]
        public int IdEspecialidad { get; set; }

        [Required]
        public int IdPaciente { get; set; }

        [Required]
        public int IdMedico { get; set; }

        [Required]
        public DateTime FechaCita { get; set; }

        [Required]
        public int IdHorario { get; set; }

        [Required]
        [MaxLength(255)]
        public string MotivoConsulta { get; set; }

        [Required]
        public int IdTipoPago { get; set; }

        [Required]
        public decimal ImporteTotal { get; set; }        

        [Required]
        [MaxLength(20)]
        public string UsuarioCreacion { get; set; }

        [MaxLength(20)]
        public string? UsuarioModificacion { get; set; }
    }
}
