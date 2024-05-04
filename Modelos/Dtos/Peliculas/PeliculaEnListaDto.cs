using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Peliculas
{
    public class PeliculaEnListaDto
    {

        public int Id_Pelicula { get; set; }
        [Required(ErrorMessage = "El Cartel_path (Imagen) de la pelicula es obligatorio")]
        [MaxLength(600, ErrorMessage = "El número máximo de caracteres es 600!")]
        public string Cartel_path { get; set; }

        [Required(ErrorMessage = "La descripcion de la pelicula es obligatorio")]
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres es 1000!")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de estreno de la pelicula es obligatorio")]
        public string Fecha_estreno { get; set; }

        [Required(ErrorMessage = "EL Genero_Pelicula es obligatorio")]
        [MaxLength(600, ErrorMessage = "El número máximo de caracteres es 600!")]
        public string Genero_Pelicula { get; set; }

        [Required(ErrorMessage = "El titulo_original de la pelicula es obligatorio")]
        [MaxLength(300, ErrorMessage = "El número máximo de caracteres es 300!")]
        public string Titulo_original { get; set; }

    }
}
