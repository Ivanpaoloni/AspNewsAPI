using System.ComponentModel.DataAnnotations;

namespace AspNewsAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Name { get; set; }
        public List<News> News { get; set; }
    }
}
