using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Usuarios
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "El Correo Electrónico es Obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El UserName es Obligatorio")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El Password es Obligatorio")]
        public string Password { get; set; }

        public string Role { get; set; }

    }
}
