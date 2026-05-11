using System.ComponentModel.DataAnnotations;

namespace AdminCore.Models
{
    public class CategoriaGasto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<SubCategoriaGasto> SubCategorias { get; set; } = new List<SubCategoriaGasto>();
    }
}
