using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Peliculas
{
    public class PeliculasRecomendadasDto
    {
        public string PeliculaDeReferencia { get; set; }

        public PeliculaDto Pelicula { get; set; }
    }
}
