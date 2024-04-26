
using System.ComponentModel.DataAnnotations;


namespace IARecommendAPI.Modelos
{
    public class Pelicula
    {
        [Key]
        public int Id_pelicula { get; set; }
        [MaxLength(300, ErrorMessage = "El número máximo de caracteres es de 300")]
        public string Titulo_original{ get; set; }
        [MaxLength(1000, ErrorMessage = "El número máximo de caracteres es de 1000")]
        public string Descripcion { get; set; }
        public DateTime? Fecha_estreno { get; set; }
        [MaxLength(600, ErrorMessage = "El número máximo de caracteres es de 600")]
        public string Cartel_path { get; set; }
        [MaxLength(600, ErrorMessage = "El número máximo de caracteres es de 600")]
        public string Genero_Pelicula { get; set; }
        public List<Like> Likes { get; } = new List<Like>();
    }
}
