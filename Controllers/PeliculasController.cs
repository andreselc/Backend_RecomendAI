using AutoMapper;
using IARecommendAPI.Modelos;
using IARecommendAPI.Modelos.Dtos;
using IARecommendAPI.Repositorios.IRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IARecommendAPI.Controllers
{
    [Route("api/Pelicula")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepositorio _pRepo;
        private readonly IMapper _mapper;
        public PeliculasController(IPeliculaRepositorio pRepo, IMapper mapper)
        {
            _pRepo = pRepo;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = _pRepo.GetPeliculas();
            var listaPeliculasDto = new List<PeliculaDto>();
            foreach (var lista in listaPeliculas)
            {
                listaPeliculasDto.Add(_mapper.Map<PeliculaDto>(lista));

            }
            return Ok(listaPeliculasDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PeliculaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearPelicula([FromBody] CrearPeliculaDto crearPeliculaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearPeliculaDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_pRepo.ExistePelicula(crearPeliculaDto.Titulo_original))
            {
                ModelState.AddModelError("", "La pelicula ya existe");
                return StatusCode(404, ModelState);
            }

            var pelicula = _mapper.Map<Pelicula>(crearPeliculaDto);
            if (!_pRepo.CrearPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro de {pelicula.Titulo_original}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPelicula", new { nombre= pelicula.Titulo_original }, pelicula);
        }

        [HttpGet("buscar", Name = "GetPelicula")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPelicula(string nombre)
        {
            var itemPeli = _pRepo.GetPelicula(nombre);
            if (itemPeli == null)
            {
                return NotFound();
            }
            var itemPeliDto = _mapper.Map<PeliculaDto>(itemPeli);
            return Ok(itemPeliDto);
        }
    }
}
