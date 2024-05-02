using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using IARecommendAPI.Repositorios.IRepositorios;
using IARecommendAPI.Modelos.Dtos.Likes;
using IARecommendAPI.Modelos.Dtos.Peliculas;

namespace IARecommendAPI.Controllers
{
    [Route("api/RecomendIA")]
    [ApiController]
    public class RecomendController : ControllerBase
    {
        private readonly ILikeRepositorio _likeRepo;
        private readonly IUsuarioRepositorio _userRepo;
        private readonly IPeliculaRepositorio _peliRepo;
        private readonly IMapper _mapper;
        public RecomendController(ILikeRepositorio likeRepo, 
            IUsuarioRepositorio userRepo,
            IPeliculaRepositorio peliRepo,
            IMapper mapper)
        {
            _likeRepo = likeRepo;
            _userRepo = userRepo;
            _peliRepo = peliRepo;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin,usuario")]
        [HttpGet("Recomiendame", Name = "GetDatosApi")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDatosApi(string NombrePelicula)
        {
            var usuarioLogeado = _userRepo.GetCurrentUser();
            var listaPeliculasRandomEnLikes = _likeRepo.GetThreeRandomLikesForUser(usuarioLogeado.Id,3);
            var peliculasRecomendadas = new List<PeliculasRecomendadasDto>();

            foreach (var lista in listaPeliculasRandomEnLikes)
            {
                var pelicula = _peliRepo.GetPeliculaById(lista.Id_pelicula);

                string apiUrl = "http://localhost:5000/recomendar_pelicula/" + pelicula.Titulo_original;

                HttpClient client = new HttpClient();

                // Enviar solicitud GET
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Obtener el contenido de la respuesta
                    string jsonString = await response.Content.ReadAsStringAsync();
                    peliculasRecomendadas.Add(_mapper.Map<PeliculasRecomendadasDto>(jsonString));
                    return Ok(jsonString);
                }

                else
                {
                    return BadRequest(ModelState);
                }
            }

        return Ok(peliculasRecomendadas);
        }
    }
}
