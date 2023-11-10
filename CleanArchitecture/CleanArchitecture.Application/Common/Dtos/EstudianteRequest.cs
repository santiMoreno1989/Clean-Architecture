using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.Common.Dtos
{
    public class EstudianteRequest
    {
        [Required]
        [MaxLength(200,ErrorMessage ="Ha excedido el maximo de 200 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(250, ErrorMessage = "Ha excedido el maximo de 250 caracteres.")]
        public string Apellido { get; set; } = string.Empty;
        public bool Activo { get; set; }

    }
}
