using IARecommendAPI.Modelos;

namespace IARecommendAPI.Repositorios.IRepositorios
{
    public interface ILikeRepositorio
    {
        ICollection<Like> GetLikes();
        bool GiveLike(Like like);
        bool Guardar();
        public bool ExisteLikeDuplicado(string idUsuario, int idPelicula)
    }
}
