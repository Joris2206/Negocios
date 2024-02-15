using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocios_API.Datos;
using Negocios_API.Models;
using Negocios_API.Models.Dto;

namespace Negocios_API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly ILogger<BusinessController> _logger;
        private readonly ApplicationDbContext _db;

        public BusinessController(ILogger<BusinessController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("GetBusinesses")]
        public ActionResult<IEnumerable<BusinessDto>> GetBusinesses()
        {
            _logger.LogInformation("Get every business");
            return Ok(_db.Businesses.ToList());
        }

        [HttpGet("{id:int}", Name = "GetBusiness")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<BusinessDto> GetBusiness(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Error al traer los datos del negocio con Id " + id);
                return BadRequest();
            }
            var business = _db.Businesses.FirstOrDefault(b => b.Id == id);

            if (business == null)
            {
                return NotFound();
            }

            return Ok(business);
        }

        [HttpPost("CreateBusiness")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<BusinessDto> CreateOwner([FromBody] BusinessDto businessDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.Businesses.FirstOrDefault(b => b.RUC == businessDto.RUC) != null)
            {
                ModelState.AddModelError("RUCExistente", "El RUC ya está registrado!");
                return BadRequest(ModelState);
            }
            if (businessDto == null)
            {
                return BadRequest(businessDto);
            }
            if (businessDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Business model = new()
            {
                OwnerId = businessDto.OwnerId,
                NombreNegocio = businessDto.NombreNegocio,
                Direccion = businessDto.Direccion,
                RUC = businessDto.RUC,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            _db.Businesses.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetBusiness", new { id = businessDto.Id }, businessDto);
        }

        [HttpDelete("DeleteBusiness/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBusiness(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var business = _db.Businesses.FirstOrDefault(b => b.Id == id);
            if (business == null)
            {
                return NotFound();
            }
            
            _db.Businesses.Remove(business);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("UpdateBusiness/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateBusiness(int id, [FromBody] BusinessDto businessDto)
        {
            if (businessDto == null || id != businessDto.Id)
            {
                return BadRequest();
            }

            Business model = new()
            {
                Id = businessDto.Id,
                OwnerId = businessDto.OwnerId,
                NombreNegocio = businessDto.NombreNegocio,
                Direccion = businessDto.Direccion,
                RUC = businessDto.RUC
            };

            _db.Businesses.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
