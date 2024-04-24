using IARecommendAPI.Data;
using IARecommendAPI.Modelos;
using IARecommendAPI.Repositorios.IRepositorios;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IARecommendAPI.Repositorios
{
    public class PeliculaRepositorio : IPeliculaRepositorio
    {
        private readonly ApplicationDbContext _bd;
        public PeliculaRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool CrearPelicula(Pelicula pelicula)
        {
            _bd.Pelicula.Add(pelicula);
            return Guardar();
        }

        public ICollection<Pelicula> GetPeliculas()
        {
            return _bd.Pelicula.OrderBy(c => c.Titulo_original).ToList();
        }
        public bool ExistePelicula(string nombre)
        {
            bool valor = _bd.Pelicula.Any(c => c.Titulo_original.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExistePelicula(int id)
        {
            bool valor = _bd.Pelicula.Any(c => c.Id_pelicula == id);
            return valor;
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
        public Pelicula GetPelicula(string nombreP)
        {
            return _bd.Pelicula.FirstOrDefault(c => c.Titulo_original.ToLower().Trim() == nombreP.ToLower().Trim());
        }
    }
}
