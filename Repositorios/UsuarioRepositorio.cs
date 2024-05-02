using AutoMapper;
using IARecommendAPI.Data;
using IARecommendAPI.Modelos.Dtos.Usuarios;
using IARecommendAPI.Modelos;
using IARecommendAPI.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IARecommendAPI.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd;
        private string claveSecreta;
        private readonly UserManager<Usuarios> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioRepositorio(ApplicationDbContext bd, IConfiguration config,
            UserManager<Usuarios> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _bd = bd;
            //Accedes al AppSetings para obtener la clave secreta de tus tokens
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        //Obtener usuario que está logeado
        public Usuarios GetCurrentUser()
        {
            var usuarioActual = _httpContextAccessor.HttpContext.User;

            if (!usuarioActual.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Usuario no autenticado.");
            }

            var userEmail = usuarioActual.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (userEmail == null)
            {
                throw new InvalidOperationException("No se pudo encontrar el identificador del usuario en los claims.");
            }

            var usuario = _bd.Usuarios.FirstOrDefault(u => u.Email == userEmail);

            if (usuario == null)
            {
                throw new InvalidOperationException("El usuario autenticado no existe en la base de datos.");
            }

            return usuario;
        }

        //Obtener Usuario
        public Usuarios GetUsuario(string usuarioId)
        {
            return _bd.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        }

        //Obtener usuarios
        public ICollection<Usuarios> GetUsuarios()
        {
            return _bd.Usuarios.OrderBy(u => u.Email).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuariobd = _bd.Usuarios.FirstOrDefault(u => u.Email == usuario);
            if (usuariobd == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //LOGIN
        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            //var passwordEncrptado = obtenermd5(usuarioLoginDto.Password);
            var usuario = _bd.Usuarios.FirstOrDefault(
               u => u.Email.ToLower() == usuarioLoginDto.Email.ToLower());

            bool isValida = await _userManager.CheckPasswordAsync(usuario, usuarioLoginDto.Password);
            //Validamos si el usuario no existe con la combinación de usuario y contraseña correcta
            if (isValida == false || usuario == null)
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            //Aquí sí existe el usuario, entnces se puede procesar el login
            var roles = await _userManager.GetRolesAsync(usuario);
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = _mapper.Map<UsuarioDatosDto>(usuario)
            };

            return usuarioLoginRespuestaDto;
        }

        //REGISTRO
        public async Task<UsuarioLoginRespuestaDto> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            Usuarios usuario = new Usuarios()
            {
                UserName = usuarioRegistroDto.UserName,
                Email = usuarioRegistroDto.Email,
                NormalizedEmail = usuarioRegistroDto.Email.ToUpper(),
            };

            var result = await _userManager.CreateAsync(usuario, usuarioRegistroDto.Password);
            if (result.Succeeded)
            {
                //Solo la primera vez y es para crear los roles
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("usuario"));
                }

                await _userManager.AddToRoleAsync(usuario, usuarioRegistroDto.Role);
                var usuarioRetornado = _bd.Usuarios.FirstOrDefault(u => u.UserName == usuarioRegistroDto.UserName);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email.ToString()),
                    new Claim(ClaimTypes.Role, usuarioRegistroDto.Role)
                }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = manejadorToken.CreateToken(tokenDescriptor);

                return new UsuarioLoginRespuestaDto()
                {
                    Token = manejadorToken.WriteToken(token),
                    Usuario = _mapper.Map<UsuarioDatosDto>(usuarioRetornado)
                };

            }
            return new UsuarioLoginRespuestaDto();
        }

       //Buscar usuario por UserName
        public ICollection<Usuarios> BuscarUsuarioPorNombre(string userName)
        {
            IQueryable<Usuarios> query = _bd.Usuarios.Include(u => u.UserName);
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(u => u.UserName.Contains(userName));
            }
            return query.ToList();
        }
    }
}
