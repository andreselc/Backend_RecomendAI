using AutoMapper;
using IARecommendAPI.Modelos;
using IARecommendAPI.Modelos.Dtos.Likes;
using IARecommendAPI.Modelos.Dtos.Peliculas;
using IARecommendAPI.Modelos.Dtos.Usuarios;

namespace IARecommendAPI.Mappers
{
    public class PaginaWebMapper : Profile
    {
        public PaginaWebMapper()
        {
            CreateMap<Usuarios, UsuarioDto>().ReverseMap();
            CreateMap<Usuarios, UsuarioDatosDto>().ReverseMap();
            CreateMap<Usuarios, UsuarioRegistroDto>().ReverseMap();
            CreateMap<Pelicula, CrearPeliculaDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
            CreateMap<Like, LikeDto>().ReverseMap();
            CreateMap<Like, CrearLikeDto>().ReverseMap();
        }
    }
}
