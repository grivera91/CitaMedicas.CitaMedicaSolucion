using System.ComponentModel.DataAnnotations;

namespace CitasMedicas.CitaMedicaApi.Model
{
    public class Especialidad
    {
        [Key]
        public int IdEspecialidad { get; set; }
        public string DescripcionEspecialidad { get; set; } = string.Empty;
        public decimal PrecioEspecialidad { get; set; } 
    }
}
