using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminCore.Models
{
    public class PresupuestoArea
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una empresa.")]
        public int EmpresaId { get; set; }

        public Empresa? Empresa { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un área.")]
        public int AreaEmpresaId { get; set; }

        public AreaEmpresa? AreaEmpresa { get; set; }

        [Required(ErrorMessage = "El mes es obligatorio.")]
        [Range(1, 12, ErrorMessage = "El mes debe estar entre 1 y 12.")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "El año es obligatorio.")]
        [Range(2024, 2100, ErrorMessage = "Ingrese un año válido.")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El monto asignado es obligatorio.")]
        [Range(0.01, 999999999, ErrorMessage = "El presupuesto debe ser mayor a 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoAsignado { get; set; }

        public string? Descripcion { get; set; }
    }
}
