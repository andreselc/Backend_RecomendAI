using AutoMapper;
using IARecommendAPI.Modelos;
using IARecommendAPI.Modelos.Dtos;

namespace IARecommendAPI.Mappers
{
    public class PeliculasMappers : Profile
    {
        public PeliculasMappers()
        {
            //crear pelicula
            CreateMap<Pelicula, CrearPeliculaDto>().ReverseMap();

            //Para consultas (no contiene el ID)
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
        }
    }
}
