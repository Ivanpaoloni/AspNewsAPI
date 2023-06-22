using System.ComponentModel.DataAnnotations;

namespace AspNewsAPI.DTOs
{
    public class AuthorCreationDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Name { get; set; }
    }
}
