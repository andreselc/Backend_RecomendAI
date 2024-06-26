﻿using IARecommendAPI.Modelos;

namespace IARecommendAPI.Repositorios.IRepositorios
{
    public interface IPeliculaRepositorio
    {
        ICollection<Pelicula> GetPeliculas();
        Pelicula GetPelicula(string nombreP);
        public Pelicula GetPeliculaById(int idPelicula);

        bool ExistePelicula(string nombre);
        bool ExistePelicula(int id);
        bool CrearPelicula(Pelicula pelicula);
        ICollection<Pelicula> GetPeliculas_Genero(String genero);

        bool Guardar();
    }
}
