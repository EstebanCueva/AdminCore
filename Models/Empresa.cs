using AdminCore.Models;
using System.ComponentModel.DataAnnotations;

namespace AdminCore.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RUC es obligatorio.")]
        [StringLength(13)]
        public string Ruc { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public bool Activo { get; set; } = true;

        public ICollection<AreaEmpresa> Areas { get; set; } = new List<AreaEmpresa>();
        public ICollection<Proveedor> Proveedores { get; set; } = new List<Proveedor>();
        public ICollection<PresupuestoArea> PresupuestosArea { get; set; } = new List<PresupuestoArea>();
    }

}

