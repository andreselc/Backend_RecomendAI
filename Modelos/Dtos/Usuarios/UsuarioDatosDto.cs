using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Usuarios
{
    public class UsuarioDatosDto
    {
        [Key]
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

    }
}
