using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IARecommendAPI.Modelos
{
    public class Like
    {
        [Key]
        public int Id_pelicula { get; set; }
        [Key]
        public string Id_usuario { get; set; }
        [ForeignKey("Id_pelicula")]
        public Pelicula PeliculasConLike { get; set; }
        [ForeignKey("Id_usuario")]
        public Usuarios usuarios { get; set; }
    }
}
