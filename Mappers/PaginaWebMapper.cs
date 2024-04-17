using AutoMapper;
using IARecommendAPI.Modelos;
using IARecommendAPI.Modelos.Dtos.Usuarios;

namespace IARecommendAPI.Mappers
{
    public class PaginaWebMapper : Profile
    {
        public PaginaWebMapper()
        {
            CreateMap<Usuarios, UsuarioDto>().ReverseMap();
        }
    }
}
