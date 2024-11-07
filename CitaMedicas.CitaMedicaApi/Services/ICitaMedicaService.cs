using CitaMedicas.CitaMedicaApi.Model;

namespace CitaMedicas.CitaMedicaApi.Services
{
    public interface ICitaMedicaService
    {
        Task<bool> ValidarDisponibilidadHorario(CitaMedica cita, int? idCitaExcluida = null);
        Task RegistrarCitaMedica(CitaMedica cita);
        Task EditarCitaMedica(CitaMedica cita);
        Task<CitaMedica> ObtenerCitaMedicaPorId(int idCitaMedica);
    }
}
