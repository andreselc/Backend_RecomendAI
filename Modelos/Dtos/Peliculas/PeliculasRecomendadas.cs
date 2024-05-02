using System.ComponentModel.DataAnnotations;

namespace IARecommendAPI.Modelos.Dtos.Peliculas
{
    public class PeliculasRecomendadasDto
    {
        public string PeliculaDeReferencia { get; set; }

        public ICollection<CrearPeliculaDto> Peliculas { get; set; }
    }
}
