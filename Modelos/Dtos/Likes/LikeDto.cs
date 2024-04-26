using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Likes
{
    public class LikeDto
    {
        public string Id_Like { get; set; }
        public string Id_usuario { get; set; }
        public int Id_Pelicula { get; set; }

    }
}
