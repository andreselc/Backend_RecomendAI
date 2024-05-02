using IARecommendAPI.Data;
using IARecommendAPI.Modelos;
using IARecommendAPI.Repositorios.IRepositorios;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IARecommendAPI.Repositorios
{
    public class LikeRepositorio : ILikeRepositorio
    {
        private readonly ApplicationDbContext _bd;
        private readonly Random _random;

        public LikeRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
            _random = new Random();
        }

        public bool GiveLike(Like like)
        {
            _bd.Like.Add(like);
            return Guardar();
        }

        public ICollection<Like> GetThreeRandomLikesForUser(string userId, int numberOfLikes)
        {
            // Obtener los likes del usuario
            var userLikes = _bd.Like.Where(l => l.Id_usuario == userId).ToList();

            // Si el usuario no tiene suficientes likes, devolver una lista vacía
            if (userLikes.Count < numberOfLikes)
            {
                return new List<Like>();
            }

            // Obtener tres índices aleatorios sin repetición
            var randomIndices = Enumerable.Range(0, userLikes.Count).OrderBy(x => _random.Next()).Take(numberOfLikes);

            // Obtener los likes correspondientes a los índices aleatorios
            var randomLikes = randomIndices.Select(i => userLikes[i]).ToList();

            return randomLikes;
        }

        public ICollection<Like> GetLikes()
        {
            return _bd.Like.OrderBy(c => c.Id_pelicula).ToList();
        }

        public bool ExisteLikeDuplicado(string idUsuario, int idPelicula)
        {
            return _bd.Like.Any(l => l.Id_usuario == idUsuario && l.Id_pelicula == idPelicula);
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
