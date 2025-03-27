using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OGA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] // Protegemos con JWT
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        // Constructor que inyecta el servicio de vehículos
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Obtiene todos los vehículos.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve una lista de todos los vehículos almacenados en el sistema.
        /// </remarks>
        /// <returns>Lista de vehículos</returns>
        /// <response code="200">Devuelve la lista de vehículos</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetAll()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        /// <summary>
        /// Obtiene un vehículo por su ID.
        /// </summary>
        /// <param name="id">El ID del vehículo a buscar</param>
        /// <remarks>
        /// Este endpoint devuelve los detalles de un vehículo específico por su ID.
        /// </remarks>
        /// <returns>Detalles del vehículo</returns>
        /// <response code="200">Devuelve los detalles del vehículo</response>
        /// <response code="404">Si no se encuentra el vehículo con el ID proporcionado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDTO>> GetById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        /// <summary>
        /// Crea un nuevo vehículo.
        /// </summary>
        /// <param name="vehicleDto">Datos del nuevo vehículo a crear</param>
        /// <remarks>
        /// Este endpoint permite crear un nuevo vehículo proporcionando los datos necesarios.
        /// </remarks>
        /// <returns>El vehículo creado</returns>
        /// <response code="201">Vehículo creado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VehicleDTO vehicleDto)
        {
            await _vehicleService.AddVehicleAsync(vehicleDto);
            return CreatedAtAction(nameof(GetById), new { id = vehicleDto.VehicleId }, vehicleDto);
        }

        /// <summary>
        /// Actualiza un vehículo existente.
        /// </summary>
        /// <param name="id">El ID del vehículo a actualizar</param>
        /// <param name="vehicleDto">Los nuevos datos del vehículo</param>
        /// <remarks>
        /// Este endpoint permite actualizar los detalles de un vehículo existente. El ID del vehículo en la URL debe coincidir con el ID en los datos del vehículo.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Vehículo actualizado exitosamente</response>
        /// <response code="400">ID de vehículo no coincide con el proporcionado</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VehicleDTO vehicleDto)
        {
            if (id != vehicleDto.VehicleId)
                return BadRequest("ID mismatch");

            await _vehicleService.UpdateVehicleAsync(vehicleDto);
            return NoContent();
        }

        /// <summary>
        /// Elimina un vehículo por su ID.
        /// </summary>
        /// <param name="id">El ID del vehículo a eliminar</param>
        /// <remarks>
        /// Este endpoint elimina un vehículo del sistema utilizando su ID.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Vehículo eliminado exitosamente</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return NoContent();
        }
    }
}
