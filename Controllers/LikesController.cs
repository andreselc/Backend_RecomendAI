using AutoMapper;
using IARecommendAPI.Modelos;
using IARecommendAPI.Modelos.Dtos.Likes;
using IARecommendAPI.Modelos.Dtos.Peliculas;
using IARecommendAPI.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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

        [Authorize(Roles = "admin,usuario")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CrearLikeDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateLike([FromBody] CrearLikeDto crearLikeDto)
        {
            Like like = new Like();
            List<LikeDto> crearLike = new List<LikeDto>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearLikeDto == null)
            {
                return BadRequest(ModelState);
            }

            foreach (var likeDto in crearLikeDto.Likes)
            {

                if (_likeRepo.ExisteLikeDuplicado(likeDto.Id_usuario, likeDto.Id_Pelicula))
                {
                    ModelState.AddModelError("", "No puedes registrar dos likes a la misma película");
                    return StatusCode(400, ModelState);
                }
            }

            foreach (var likeDto in crearLikeDto.Likes)
            {
                like = _mapper.Map<Like>(likeDto);
                if (!_likeRepo.GiveLike(like))
                {
                    ModelState.AddModelError("", $"Algo salio mal guardando el registro de la película {like.Id_pelicula} , hecha por {like.Id_usuario}");
                    return StatusCode(500, ModelState);
                }

                crearLike.Add(likeDto);
            }
            
            return Ok(crearLike);
        }

    }
}
