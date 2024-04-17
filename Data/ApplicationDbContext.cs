using BackendSAP.Modelos;
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
   
    }

    //Agregar los modelos aquí
    public DbSet<Usuarios> Usuarios { get; set; }
    }
}
