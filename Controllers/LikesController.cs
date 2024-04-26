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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearLikeDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_likeRepo.ExisteLikeDuplicado(crearLikeDto.Id_usuario, crearLikeDto.Id_Pelicula))
            {
                ModelState.AddModelError("", "No puedes registrar dos likes a la misma película");
                return StatusCode(400, ModelState);
            }

            var like = _mapper.Map<Like>(crearLikeDto);
            if (!_likeRepo.GiveLike(like))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro de {like.Id_Like}");
                return StatusCode(500, ModelState);
            }
            return Ok(like);
        }

    }
}
