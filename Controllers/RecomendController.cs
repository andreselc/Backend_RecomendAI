using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using IARecommendAPI.Repositorios.IRepositorios;

namespace IARecommendAPI.Controllers
{
    [Route("api/RecomendIA")]
    [ApiController]
    public class RecomendController : ControllerBase
    {
        private readonly ILikeRepositorio _likeRepo;
        private readonly IMapper _mapper;
        public RecomendController(ILikeRepositorio likeRepo, IMapper mapper)
        {
            _likeRepo = likeRepo;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin,usuario")]
        [HttpGet("Recomiendame", Name = "GetDatosApi")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDatosApi(string NombrePelicula)
        {
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
