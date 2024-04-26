using AutoMapper;
using IARecommendAPI.Modelos;
using IARecommendAPI.Modelos.Dtos.Likes;
using IARecommendAPI.Modelos.Dtos.Peliculas;
using IARecommendAPI.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Mvc;

namespace IARecommendAPI.Controllers
{
    [Route("api/Likes")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeRepositorio _likeRepo;
        private readonly IMapper _mapper;
        public LikesController(ILikeRepositorio likeRepo, IMapper mapper)
        {
            _likeRepo = likeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLikes()
        {
            var listLikes = _likeRepo.GetLikes();
            var listaLikesDto = new List<LikeDto>();
            foreach (var lista in listLikes)
            {
                listaLikesDto.Add(_mapper.Map<LikeDto>(lista));

            }
            return Ok(listaLikesDto);
        }

       /* [HttpPost]
        [ProducesResponseType(201, Type = typeof(LikeDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateLike([FromBody] CrearLikeDto crearLikeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearLikeDto == null)
            {
                return BadRequest(ModelState);
            }

            var pelicula = _mapper.Map<Pelicula>(crearPeliculaDto);
            if (!_pRepo.CrearPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro de {pelicula.Titulo_original}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPelicula", new { nombre = pelicula.Titulo_original }, pelicula);
        }*/

    }
}
