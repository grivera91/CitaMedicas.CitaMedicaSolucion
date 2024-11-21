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
                    // Validar duplicidad
                    var citaExistente = await _context.CitasMedicas
                        .Where(c => c.IdHorario == citaRequest.IdHorario &&
                                    c.IdMedico == citaRequest.IdMedico &&
                                    c.IdPaciente == citaRequest.IdPaciente &&
                                    c.FechaCita.Date == citaRequest.FechaCita.Date)
                        .FirstOrDefaultAsync();

                    if (citaExistente != null)
                    {
                        // Buscar el nombre del paciente
                        var horario = await _context.Horarios
                            .Where(p => p.IdHorario == citaRequest.IdHorario)
                            .Select(p => new { Nombre = p.DescripcionHorario }) // Cambia ContactoEmergencia por el campo correcto de nombre
                            .FirstOrDefaultAsync();

                        string descripcionHorario = horario?.Nombre ?? "desconocido";

                        return Conflict($"Ya existe una cita registrada para el paciente y el médico seleccionados, en el horario de {descripcionHorario} del día {citaRequest.FechaCita:dd-MM-yyyy}.");
                    }

                    // Generar un nuevo código de cita médica
                    string codigoCitaMedica = await _correlativoService.ObtenerNuevoCorrelativoAsync("CT");

                    // Calcular el impuesto y el importe neto
                    decimal importeTotal = citaRequest.ImporteTotal;
                    decimal impuesto = Math.Round(importeTotal * 0.18m, 2); // 18% de IGV (Impuesto General a las Ventas)
                    decimal importeNeto = Math.Round(importeTotal - impuesto, 2);

                    string codigoTransaccion = Guid.NewGuid().ToString();

                    // Crear la nueva cita
                    CitaMedica citaMedica = new CitaMedica
                    {
                        IdEspecialidad = citaRequest.IdEspecialidad,
                        IdPaciente = citaRequest.IdPaciente,
                        IdMedico = citaRequest.IdMedico,
                        CodigoCitaMedica = codigoCitaMedica,
                        FechaCita = citaRequest.FechaCita,
                        IdHorario = citaRequest.IdHorario,
                        MotivoConsulta = citaRequest.MotivoConsulta,
                        EstadoCita = "Pendiente",

                        IdTipoPago = citaRequest.IdTipoPago,
                        ImporteNeto = importeNeto,
                        Impuesto = impuesto,
                        ImporteTotal = citaRequest.ImporteTotal,
                        FechaPago = DateTime.Now,
                        Moneda = "PEN",
                        CodigoTransaccion = codigoTransaccion,

                        UsuarioCreacion = citaRequest.UsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };

                    _context.CitasMedicas.Add(citaMedica);
                    await _context.SaveChangesAsync();

                    // Confirmar la transacción
                    await transaction.CommitAsync();

                    // Mapear al DTO de respuesta
                    CitaMedicaResponseDto citaResponse = new CitaMedicaResponseDto
                    {
                        IdCitaMedica = citaMedica.IdCitaMedica,
                        IdEspecialidad = citaMedica.IdEspecialidad,
                        IdPaciente = citaMedica.IdPaciente,
                        IdMedico = citaMedica.IdMedico,
                        CodigoCitaMedica = citaMedica.CodigoCitaMedica,
                        FechaCita = citaMedica.FechaCita,
                        IdHorario = citaMedica.IdHorario,
                        MotivoConsulta = citaMedica.MotivoConsulta,
                        EstadoCita = citaMedica.EstadoCita,

                        IdTipoPago = citaMedica.IdTipoPago,
                        ImporteNeto = citaMedica.ImporteNeto,
                        Impuesto = citaMedica.ImporteNeto,
                        ImporteTotal = citaMedica.ImporteNeto,
                        FechaPago = citaMedica.FechaPago,
                        Moneda = citaMedica.Moneda,
                        CodigoTransaccion = citaMedica.CodigoTransaccion,
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
        public async Task<ActionResult<IEnumerable<CitaMedicaResponseDto>>> ObteneCitasMedicas()
        {
            var citasMedicas = await _context.CitasMedicas
                .Select(m => new CitaMedicaResponseDto
                {
                    IdCitaMedica = m.IdCitaMedica,
                    IdEspecialidad = m.IdEspecialidad,
                    IdPaciente = m.IdPaciente,
                    IdMedico = m.IdMedico,
                    CodigoCitaMedica = m.CodigoCitaMedica,
                    FechaCita = m.FechaCita,
                    IdHorario = m.IdHorario,
                    MotivoConsulta = m.MotivoConsulta,
                    EstadoCita = m.EstadoCita
                })
                .ToListAsync();

            return Ok(citasMedicas);
        }
    }
}
