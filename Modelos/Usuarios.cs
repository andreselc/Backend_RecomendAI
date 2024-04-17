using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSAP.Modelos
{
    public class Usuarios : IdentityUser
    {
        //Añadir campos personalizados
        [Required]
        public string UserName { get; set; }

    }

}

