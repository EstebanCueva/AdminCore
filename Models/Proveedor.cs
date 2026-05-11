using System.ComponentModel.DataAnnotations;

namespace AdminCore.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RUC del proveedor es obligatorio.")]
        [StringLength(13)]
        public string Ruc { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public string? TipoProveedor { get; set; }

        public bool Activo { get; set; } = true;

        [Required(ErrorMessage = "Debe seleccionar una empresa.")]
        public int EmpresaId { get; set; }

        public Empresa? Empresa { get; set; }
    }
}
