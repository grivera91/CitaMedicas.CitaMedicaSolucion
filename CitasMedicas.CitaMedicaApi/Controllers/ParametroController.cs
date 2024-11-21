using CitaMedicas.CitaMedicaApi.Data;
using CitaMedicas.CitaMedicaApi.DTO;
using CitaMedicas.CitaMedicaApi.Services;
using CitasMedicas.CitaMedicaApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicas.CitaMedicaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;        

        public ParametroController(ApplicationDbContext context)
        {
            _context = context;            
        }

        //[Authorize]
        [HttpGet("especialidades")]
        public async Task<ActionResult<IEnumerable<Especialidad>>> ObtenerEspecialidades()
        {
            try
            {
                List<Especialidad> especialidades = await _context.Especialidades.ToListAsync();
                return Ok(especialidades);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //[Authorize]
        [HttpGet("horarios")]
        public async Task<ActionResult<IEnumerable<Horario>>> ObtenerHorarios()
        {
            try
            {
                List<Horario> horarios = await _context.Horarios.ToListAsync();
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        //[Authorize]
        [HttpGet("tipoPago")]
        public async Task<ActionResult<IEnumerable<TipoPago>>> ObtenerTipoPagos()
        {
            try
            {
                List<TipoPago> tipoPagos = await _context.TipoPagos.ToListAsync();
                return Ok(tipoPagos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
