using AutoMapper;
using IARecommendAPI.Modelos;
using IARecommendAPI.Modelos.Dtos.Likes;
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


        
    }
}
