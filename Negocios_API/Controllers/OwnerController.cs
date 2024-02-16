using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocios_API.Datos;
using Negocios_API.Models;
using Negocios_API.Models.Dto;
using Negocios_API.Repository.IRepository;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Negocios_API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly ILogger<OwnerController> _logger;
        private readonly IOwnerRepository _ownerRepo;
        protected APIResponse _response;

        public OwnerController(ILogger<OwnerController> logger, IOwnerRepository ownerRepo)
        {
            _logger = logger;
            _ownerRepo = ownerRepo;
            _response = new();
        }

        [HttpGet("GetOwners")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetOwners()
        {
            try
            {
                _logger.LogInformation("Get every owner");
                var owners = await _ownerRepo.GetAll();

                _response.Result = owners;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetOwner")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetOwner(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogError("Error al traer los datos del propietario con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var owner = await _ownerRepo.Get(o => o.Id == id);

                if (owner == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = owner;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("CreateOwner")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> CreateOwner([FromBody] OwnerCreateDto ownerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _ownerRepo.Get(b => b.Correo.ToLower() == ownerDto.Correo.ToLower()) != null)
                {
                    ModelState.AddModelError("CorreoExistente", "El correo electrónico ya está registrado!");
                    return BadRequest(ModelState);
                }
                if (ownerDto == null)
                {
                    return BadRequest(ownerDto);
                }

                Owner model = new()
                {
                    NombrePropietario = ownerDto.NombrePropietario,
                    Correo = ownerDto.Correo,
                    Clave = ownerDto.Clave
                };

                await _ownerRepo.Create(model);
                _response.Result = model;
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetOwner", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("DeleteOwner/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult<APIResponse>> DeleteOwner(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.statusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var owner = await _ownerRepo.Get(o => o.Id == id);
                if (owner == null)
                {
                    _response.IsSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _ownerRepo.Remove(owner);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpPut("UpdateOwner/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateOwner(int id, [FromBody] OwnerUpdateDto ownerDto) 
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

            _ownerRepo.Update(model);

            return NoContent();
        }
    }
}
