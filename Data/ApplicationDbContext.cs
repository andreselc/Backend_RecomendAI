using IARecommendAPI.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IARecommendAPI.Data
{
 public class ApplicationDbContext : IdentityDbContext<Usuarios>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    protected override void OnModelCreating(ModelBuilder builder)
    {
         base.OnModelCreating(builder);
         builder.Entity<Like>().HasKey(e => new { e.Id_pelicula, e.Id_usuario});
         base.OnModelCreating(builder);

    }

        //Agregar TODOS los modelos aqui
        public DbSet<Pelicula> Pelicula{ get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Like> Like { get; set; }
    }
}
