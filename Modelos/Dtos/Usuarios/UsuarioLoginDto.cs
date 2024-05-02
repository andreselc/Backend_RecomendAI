using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Usuarios
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El Correo del usuario es Obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Password es Obligatorio")]
        public string Password { get; set; }

    }
}
