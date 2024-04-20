using IARecommendAPI.Modelos;

namespace IARecommendAPI.Repositorios.IRepositorio
{
    public interface IPeliculaRepositorio
    {
        ICollection<Pelicula> GetPeliculas();
        Pelicula GetPelicula(string nombreP);

        bool ExistePelicula(string nombre);
        bool ExistePelicula(int id);
        bool CrearPelicula(Pelicula pelicula);
       
        bool Guardar();
    }
}
