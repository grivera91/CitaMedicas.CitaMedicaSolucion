using System;
using System.ComponentModel.DataAnnotations;

namespace CitaMedicas.CitaMedicaApi.DTO
{
    public class CitaMedicaResponseDto
    {        
        public int IdCitaMedica { get; set; }
        public int IdEspecialidad { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public string CodigoCitaMedica { get; set; }
        public DateTime FechaCita { get; set; }
        public int IdHorario { get; set; }
        public string MotivoConsulta { get; set; }
        public string EstadoCita { get; set; }

        public int IdTipoPago { get; set; }
        public decimal ImporteNeto { get; set; }
        public decimal Impuesto { get; set; }
        public decimal ImporteTotal { get; set; }
        public DateTime FechaPago { get; set; }
        public string Moneda { get; set; }
        public string CodigoTransaccion { get; set; }
    }
}