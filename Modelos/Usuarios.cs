using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IARecommendAPI.Modelos
{
    public class Usuarios : IdentityUser
    {
        //Añadir campos personalizados
        [Required]
        public string UserName { get; set; }

    }

}

