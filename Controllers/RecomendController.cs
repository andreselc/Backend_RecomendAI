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
            var listaPeliculasRandom = _likeRepo.GetThreeRandomLikesForUser(usuarioLogeado.Id,3);
            var peliculasRecomendadas = new List<PeliculasRecomendadasDto>();

            foreach (var lista in listaPeliculasRandom)
            {
                //await 
                peliculasRecomendadas.Add(_mapper.Map<PeliculasRecomendadasDto>(lista));
            }

            // URL de la API Flask
            string apiUrl = "http://localhost:5000/recomendar_pelicula/" + NombrePelicula;

            // Crear cliente HTTP
            HttpClient client = new HttpClient();

            // Enviar solicitud GET
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            // Comprobar si la solicitud fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Obtener el contenido de la respuesta
                string jsonString = await response.Content.ReadAsStringAsync();

                // Deserializar JSON a objeto
                // ...

                // Devolver la respuesta
                return Ok(jsonString);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
