using AutoMapper;
using BackendSAP.Modelos;
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
