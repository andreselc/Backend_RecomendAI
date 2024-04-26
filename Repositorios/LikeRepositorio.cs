using IARecommendAPI.Data;
using IARecommendAPI.Modelos;
using IARecommendAPI.Repositorios.IRepositorios;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IARecommendAPI.Repositorios
{
    public class LikeRepositorio : ILikeRepositorio
    {
        private readonly ApplicationDbContext _bd;
        public LikeRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool GiveLike(Like like)
        {
            _bd.Like.Add(like);
            return Guardar();
        }

        public ICollection<Like> GetLikes()
        {
            return _bd.Like.OrderBy(c => c.Id_Like).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
