using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Peliculas
{
    public class PeliculaDto
    {
        public int Id_pelicula { get; set; }
        [Required(ErrorMessage = "El titulo_original de la pelicula es obligatorio")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es 100!")]
        public string Titulo_original { get; set; }

        [Required(ErrorMessage = "La descripcion de la pelicula es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de estreno de la pelicula es obligatorio")]
        public DateTime Fecha_estreno { get; set; }

        [Required(ErrorMessage = "El Cartel_path (Imagen) de la pelicula es obligatorio")]
        public string Cartel_path { get; set; }
    }
}
