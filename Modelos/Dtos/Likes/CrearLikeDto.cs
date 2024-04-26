using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Likes
{
    public class CrearLikeDto
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        public string Id_usuario { get; set; }
        [Required(ErrorMessage = "El ID de la película es obligatorio")]
        public int Id_Pelicula { get; set; }
    }
}
