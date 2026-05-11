using System.ComponentModel.DataAnnotations;

namespace AdminCore.Models
{
    public class SubCategoriaGasto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la subcategoría es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int CategoriaGastoId { get; set; }

        public CategoriaGasto? CategoriaGasto { get; set; }
    }
}
