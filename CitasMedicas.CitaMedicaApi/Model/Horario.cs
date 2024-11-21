using System.ComponentModel.DataAnnotations;

namespace CitasMedicas.CitaMedicaApi.Model
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }
        public string DescripcionHorario { get; set; } = string.Empty;
    }
}
