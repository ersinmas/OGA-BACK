using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OGA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesTrailerController : ControllerBase
    {
        private readonly IVehicleTrailerService _vehicleTrailerService;

        // Constructor que inyecta el servicio de vehículos remolque
        public VehiclesTrailerController(IVehicleTrailerService vehicleTrailerService)
        {
            _vehicleTrailerService = vehicleTrailerService;
        }

        /// <summary>
        /// Obtiene todos los remolques de vehículos.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve una lista de todos los remolques de vehículos almacenados en el sistema.
        /// </remarks>
        /// <returns>Lista de remolques de vehículos</returns>
        /// <response code="200">Devuelve la lista de remolques de vehículos</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleTrailerDTO>>> GetAll()
        {
            var vehiclesTrailer = await _vehicleTrailerService.GetAllVehicleTrailersAsync();
            return Ok(vehiclesTrailer);
        }

        /// <summary>
        /// Obtiene un remolque de vehículo por su ID.
        /// </summary>
        /// <param name="id">El ID del remolque de vehículo a buscar</param>
        /// <remarks>
        /// Este endpoint devuelve los detalles de un remolque de vehículo específico por su ID.
        /// </remarks>
        /// <returns>Detalles del remolque de vehículo</returns>
        /// <response code="200">Devuelve los detalles del remolque de vehículo</response>
        /// <response code="404">Si no se encuentra el remolque con el ID proporcionado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleTrailerDTO>> GetById(int id)
        {
            var vehicleTrailer = await _vehicleTrailerService.GetVehicleTrailerByIdAsync(id);
            if (vehicleTrailer == null)
                return NotFound();

            return Ok(vehicleTrailer);
        }

        /// <summary>
        /// Crea un nuevo remolque de vehículo.
        /// </summary>
        /// <param name="vehicleTrailerDto">Datos del nuevo remolque de vehículo a crear</param>
        /// <remarks>
        /// Este endpoint permite crear un nuevo remolque de vehículo proporcionando los datos necesarios.
        /// </remarks>
        /// <returns>El remolque de vehículo creado</returns>
        /// <response code="201">Remolque de vehículo creado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VehicleTrailerDTO vehicleTrailerDto)
        {
            try
            {
                await _vehicleTrailerService.AddVehicleTrailerAsync(vehicleTrailerDto);
                return CreatedAtAction(nameof(GetById), new { id = vehicleTrailerDto.TrailerId }, vehicleTrailerDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un remolque de vehículo existente.
        /// </summary>
        /// <param name="vehicleTrailerDto">Los nuevos datos del remolque de vehículo</param>
        /// <remarks>
        /// Este endpoint permite actualizar los detalles de un remolque de vehículo existente.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Remolque de vehículo actualizado exitosamente</response>
        /// <response code="400">Datos incorrectos o errores al intentar actualizar el remolque</response>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VehicleTrailerDTO vehicleTrailerDto)
        {
            try
            {
                await _vehicleTrailerService.UpdateVehicleTrailerAsync(vehicleTrailerDto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un remolque de vehículo por su ID.
        /// </summary>
        /// <param name="id">El ID del remolque de vehículo a eliminar</param>
        /// <remarks>
        /// Este endpoint elimina un remolque de vehículo del sistema utilizando su ID.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Remolque de vehículo eliminado exitosamente</response>
        /// <response code="404">Si no se encuentra el remolque con el ID proporcionado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleTrailerService.DeleteVehicleTrailerAsync(id);
            return NoContent();
        }
    }
}
