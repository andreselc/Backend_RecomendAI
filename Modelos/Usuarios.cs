using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IARecommendAPI.Modelos
{
    public class Usuarios : IdentityUser
    {
        public List<Like> Likes { get; } = new List<Like>();
    }

}

