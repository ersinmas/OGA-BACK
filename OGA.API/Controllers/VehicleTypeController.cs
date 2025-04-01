using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OGA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclesTypeController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        // Constructor que inyecta el servicio de tipos de vehículos
        public VehiclesTypeController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        /// <summary>
        /// Obtiene todos los tipos de vehículos.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve una lista de todos los tipos de vehículos almacenados en el sistema.
        /// </remarks>
        /// <returns>Lista de tipos de vehículos</returns>
        /// <response code="200">Devuelve la lista de tipos de vehículos</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleTypeDTO>>> GetAll()
        {
            var vehiclesType = await _vehicleTypeService.GetAllVehicleTypeAsync();
            return Ok(vehiclesType);
        }

        /// <summary>
        /// Obtiene un tipo de vehículo por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo de vehículo a buscar</param>
        /// <remarks>
        /// Este endpoint devuelve los detalles de un tipo de vehículo específico por su ID.
        /// </remarks>
        /// <returns>Detalles del tipo de vehículo</returns>
        /// <response code="200">Devuelve los detalles del tipo de vehículo</response>
        /// <response code="404">Si no se encuentra el tipo de vehículo con el ID proporcionado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleTypeDTO>> GetById(int id)
        {
            var vehicleType = await _vehicleTypeService.GetVehicleTypeByIdAsync(id);
            if (vehicleType == null)
                return NotFound();

            return Ok(vehicleType);
        }

        /// <summary>
        /// Crea un nuevo tipo de vehículo.
        /// </summary>
        /// <param name="vehicleTypeDto">Datos del nuevo tipo de vehículo a crear</param>
        /// <remarks>
        /// Este endpoint permite crear un nuevo tipo de vehículo proporcionando los datos necesarios.
        /// </remarks>
        /// <returns>El tipo de vehículo creado</returns>
        /// <response code="201">Tipo de vehículo creado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VehicleTypeDTO vehicleTypeDto)
        {
            await _vehicleTypeService.AddVehicleTypeAsync(vehicleTypeDto);
            return CreatedAtAction(nameof(GetById), new { id = vehicleTypeDto.VehicleTypeId }, vehicleTypeDto);
        }

        /// <summary>
        /// Actualiza un tipo de vehículo existente.
        /// </summary>
        /// <param name="id">El ID del tipo de vehículo a actualizar</param>
        /// <param name="vehicleTypeDto">Los nuevos datos del tipo de vehículo</param>
        /// <remarks>
        /// Este endpoint permite actualizar los detalles de un tipo de vehículo existente. El ID del tipo de vehículo en la URL debe coincidir con el ID en los datos del tipo de vehículo.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Tipo de vehículo actualizado exitosamente</response>
        /// <response code="400">ID de tipo de vehículo no coincide con el proporcionado</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VehicleTypeDTO vehicleTypeDto)
        {
            if (id != vehicleTypeDto.VehicleTypeId)
                return BadRequest("ID mismatch");

            await _vehicleTypeService.UpdateVehicleTypeAsync(vehicleTypeDto);
            return NoContent();
        }

        /// <summary>
        /// Elimina un tipo de vehículo por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo de vehículo a eliminar</param>
        /// <remarks>
        /// Este endpoint elimina un tipo de vehículo del sistema utilizando su ID.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Tipo de vehículo eliminado exitosamente</response>
        /// <response code="404">Si no se encuentra el tipo de vehículo con el ID proporcionado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleTypeService.DeleteVehicleTypeAsync(id);
            return NoContent();
        }
    }
}
