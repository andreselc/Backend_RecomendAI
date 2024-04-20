using IARecommendAPI.Modelos;
using Microsoft.EntityFrameworkCore;

namespace IARecommendAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Agregar TODOS los modelos aqui
        public DbSet<Pelicula> Pelicula{ get; set; }
    }
}
