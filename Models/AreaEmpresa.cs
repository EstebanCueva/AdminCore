using System.ComponentModel.DataAnnotations;

namespace AdminCore.Models
{
    public class AreaEmpresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del área es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar una empresa.")]
        public int EmpresaId { get; set; }

        public Empresa? Empresa { get; set; }

        public ICollection<PresupuestoArea> PresupuestosArea { get; set; } = new List<PresupuestoArea>();
    }
}
