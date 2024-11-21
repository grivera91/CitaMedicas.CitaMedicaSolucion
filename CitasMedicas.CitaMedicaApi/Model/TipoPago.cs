using System.ComponentModel.DataAnnotations;

namespace CitasMedicas.CitaMedicaApi.Model
{
    public class TipoPago
    {
        [Key]
        public int IdTipoPago { get; set; }
        public string DescripcionTipoPago { get; set; } = string.Empty;
    }
}
