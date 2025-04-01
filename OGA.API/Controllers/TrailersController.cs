using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OGA.API.Controllers
{
    /// <summary>
    /// Controlador para la gestión de remolques.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrailersController : ControllerBase
    {
        private readonly ITrailerService _trailerService;

        /// <summary>
        /// Constructor que inyecta el servicio de remolques.
        /// </summary>
        /// <param name="trailerService">Servicio de lógica de negocio para remolques.</param>
        public TrailersController(ITrailerService trailerService)
        {
            _trailerService = trailerService;
        }

        /// <summary>
        /// Obtiene la lista de todos los remolques disponibles.
        /// </summary>
        /// <returns>Lista de remolques.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrailerDTO>>> GetAll()
        {
            var trailers = await _trailerService.GetAllTrailersAsync();
            return Ok(trailers);
        }

        /// <summary>
        /// Obtiene un remolque específico por su ID.
        /// </summary>
        /// <param name="id">Identificador del remolque.</param>
        /// <returns>El remolque encontrado o un error 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TrailerDTO>> GetById(int id)
        {
            var trailer = await _trailerService.GetTrailerByIdAsync(id);
            if (trailer == null)
                return NotFound(new { message = "Remolque no encontrado." });

            return Ok(trailer);
        }

        /// <summary>
        /// Crea un nuevo remolque.
        /// </summary>
        /// <param name="trailerDto">Datos del remolque a crear.</param>
        /// <returns>Respuesta 201 Created con la ubicación del nuevo remolque.</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TrailerDTO trailerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _trailerService.AddTrailerAsync(trailerDto);
                return CreatedAtAction(nameof(GetById), new { id = trailerDto.TrailerId }, trailerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el remolque.", details = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un remolque existente.
        /// </summary>
        /// <param name="id">ID del remolque a actualizar.</param>
        /// <param name="trailerDto">Nuevos datos del remolque.</param>
        /// <returns>Código 204 No Content si la actualización es exitosa.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrailerDTO trailerDto)
        {
            if (id != trailerDto.TrailerId)
                return BadRequest("ID mismatch");

            await _trailerService.UpdateTrailerAsync(trailerDto);
            return NoContent();
        }

        /// <summary>
        /// Elimina un remolque por su ID.
        /// </summary>
        /// <param name="id">ID del remolque a eliminar.</param>
        /// <returns>Código 204 No Content si la eliminación fue exitosa.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTrailer = await _trailerService.GetTrailerByIdAsync(id);
            if (existingTrailer == null)
                return NotFound(new { message = "Remolque no encontrado." });

            try
            {
                await _trailerService.DeleteTrailerAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el remolque.", details = ex.Message });
            }
        }
    }
}
