using IARecommendAPI.Modelos;
using IARecommendAPI.Modelos.Dtos.Usuarios;

namespace IARecommendAPI.Repositorios.IRepositorios
{
    public interface IUsuarioRepositorio
    {
        ICollection<Usuarios> GetUsuarios();
        Usuarios GetUsuario(string usuarioId);
        bool IsUniqueUser(string usuario);
        Usuarios GetCurrentUser();
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<UsuarioLoginRespuestaDto> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}
