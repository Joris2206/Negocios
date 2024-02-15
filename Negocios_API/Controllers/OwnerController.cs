using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocios_API.Datos;
using Negocios_API.Models;
using Negocios_API.Models.Dto;
using System.Reflection.Metadata.Ecma335;

namespace Negocios_API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly ILogger<OwnerController> _logger;
        private readonly ApplicationDbContext _db;

        public OwnerController(ILogger<OwnerController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("GetOwners")]
        public ActionResult<IEnumerable<OwnerDto>> GetOwners()
        {
            _logger.LogInformation("Get every owner");
            return Ok(_db.Owners.ToList());
        }

        [HttpGet("{id:int}", Name="GetOwner")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<OwnerDto> GetOwner(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Error al traer los datos del propietario con Id " + id);
                return BadRequest();
            }
            var owner = _db.Owners.FirstOrDefault(o => o.Id == id);

            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }

        [HttpPost("CreateOwner")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<OwnerDto> CreateOwner([FromBody] OwnerDto ownerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.Owners.FirstOrDefault(b => b.Correo.ToLower() == ownerDto.Correo.ToLower()) != null) 
            {
                ModelState.AddModelError("CorreoExistente", "El correo electrónico ya está registrado!");
                return BadRequest(ModelState);
            }
            if(ownerDto == null)
            {
                return BadRequest(ownerDto);
            }
            if (ownerDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Owner model = new()
            {
                NombrePropietario = ownerDto.NombrePropietario,
                Correo = ownerDto.Correo,
                Clave = ownerDto.Clave
            };

            _db.Owners.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetOwner", new {id= ownerDto.Id}, ownerDto);
        }

        [HttpDelete("DeleteOwner/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int id) 
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var owner = _db.Owners.FirstOrDefault(o=>o.Id == id);
            if(owner == null)
            {
                return NotFound();
            }
            _db.Owners.Remove(owner);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("UpdateOwner/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateOwner(int id, [FromBody] OwnerDto ownerDto) 
        {
            if (ownerDto==null || id!=ownerDto.Id)
            {
                return BadRequest();
            }

            Owner model = new()
            {
                Id = ownerDto.Id,
                NombrePropietario = ownerDto.NombrePropietario,
                Correo = ownerDto.Correo,
                Clave = ownerDto.Clave
            };

            _db.Owners.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
