using CitaMedicas.CitaMedicaApi.Data;
using CitaMedicas.CitaMedicaApi.DTO;
using CitaMedicas.CitaMedicaApi.Model;
using CitaMedicas.CitaMedicaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitaMedicas.CitaMedicaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaMedicaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CorrelativoService _correlativoService;        

        public CitaMedicaController(ApplicationDbContext context, CorrelativoService correlativoService)
        {
            _context = context;
            _correlativoService = correlativoService;            
        }

        //[Authorize]
        [HttpPost()]
        public async Task<IActionResult> RegistrarCitaMedica([FromBody] CitaMedicaRequestDto citaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Generar un nuevo código de cita médica
                    string codigoCitaMedica = await _correlativoService.ObtenerNuevoCorrelativoAsync("CT");

                    // Crear la nueva cita
                    CitaMedica citaMedica = new CitaMedica
                    {
                        IdPaciente = citaRequest.IdPaciente,
                        IdMedico = citaRequest.IdMedico,
                        CodigoCitaMedica = codigoCitaMedica,
                        FechaCita = citaRequest.FechaCita,
                        HoraInicio = citaRequest.HoraInicio,
                        HoraFin = citaRequest.HoraFin,
                        MotivoConsulta = citaRequest.MotivoConsulta,
                        EstadoCita = "Pendiente",
                        UsuarioCreacion = citaRequest.UsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };

                    _context.CitasMedicas.Add(citaMedica);
                    await _context.SaveChangesAsync();

                    // Validación de disponibilidad de horario
                    //bool horarioDisponible = await _citaMedicaService.ValidarDisponibilidadHorario(nuevaCita);
                    //if (!horarioDisponible)
                    //{
                    //    return Conflict("El horario seleccionado no está disponible.");
                    //}

                    // Registrar la cita médica
                    //await _citaMedicaService.RegistrarCitaMedica(citaMedica);

                    // Confirmar la transacción
                    await transaction.CommitAsync();

                    // Mapear al DTO de respuesta
                    var citaResponse = new CitaMedicaResponseDto
                    {
                        IdCitaMedica = citaMedica.IdCitaMedica,
                        IdPaciente = citaMedica.IdPaciente,
                        IdMedico = citaMedica.IdMedico,
                        CodigoCitaMedica = citaMedica.CodigoCitaMedica,
                        FechaCita = citaMedica.FechaCita,
                        HoraInicio = citaMedica.HoraInicio,
                        HoraFin = citaMedica.HoraFin,
                        MotivoConsulta = citaMedica.MotivoConsulta
                    };

                    return Ok(citaResponse);
                }
                catch (Exception ex)
                {
                    // Revertir la transacción en caso de error
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"Error al registrar la cita: {ex.Message}");
                }
            }
        }


        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaMedicaResponseDto>>> ObtenerRecepcionistas()
        {
            var citasMedicas = await _context.CitasMedicas
                .Select(m => new CitaMedicaResponseDto
                {
                    IdCitaMedica = m.IdCitaMedica,
                    IdPaciente = m.IdPaciente,
                    IdMedico = m.IdMedico,
                    CodigoCitaMedica = m.CodigoCitaMedica,
                    FechaCita = m.FechaCita,
                    HoraInicio = m.HoraInicio,
                    HoraFin = m.HoraFin,
                    MotivoConsulta = m.MotivoConsulta
                })
                .ToListAsync();

            return Ok(citasMedicas);
        }
    }
}
