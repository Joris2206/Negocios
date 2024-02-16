using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocios_API.Datos;
using Negocios_API.Models;
using Negocios_API.Models.Dto;
using Negocios_API.Repository.IRepository;

namespace Negocios_API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly ILogger<BusinessController> _logger;
        private readonly IBusinessRepository _businessRepo;

        public BusinessController(ILogger<BusinessController> logger, IBusinessRepository businessRepo)
        {
            _logger = logger;
            _businessRepo = businessRepo;
        }

        [HttpGet("GetBusinesses")]
        public async Task<ActionResult<IEnumerable<BusinessDto>>> GetBusinesses()
        {
            _logger.LogInformation("Get every business");
            return Ok(await _businessRepo.GetAll());
        }

        [HttpGet("{id:int}", Name = "GetBusiness")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BusinessDto>> GetBusiness(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Error al traer los datos del negocio con Id " + id);
                return BadRequest();
            }
            var business = await _businessRepo.Get(b => b.Id == id);

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
        public async Task<ActionResult<BusinessDto>> CreateOwner([FromBody] BusinessCreateDto businessDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _businessRepo.Get(b => b.RUC == businessDto.RUC) != null)
            {
                ModelState.AddModelError("RUCExistente", "El RUC ya está registrado!");
                return BadRequest(ModelState);
            }
            if (businessDto == null)
            {
                return BadRequest(businessDto);
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

            await _businessRepo.Create(model);

            return CreatedAtRoute("GetBusiness", new { id = model.Id }, model);
        }

        [HttpDelete("DeleteBusiness/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var business = await _businessRepo.Get(b => b.Id == id);
            if (business == null)
            {
                return NotFound();
            }
            
            _businessRepo.Remove(business);

            return NoContent();
        }

        [HttpPut("UpdateBusiness/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateBusiness(int id, [FromBody] BusinessUpdateDto businessDto)
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

            _businessRepo.Update(model);

            return NoContent();
        }
    }
}
