
using System.ComponentModel.DataAnnotations;


namespace IARecommendAPI.Modelos
{
    public class Pelicula
    {
        [Key]
        public int Id_pelicula { get; set; }
        [Required]
        public string Titulo_original{ get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha_estreno { get; set; }
        public string Cartel_path { get; set; }
    }
}
