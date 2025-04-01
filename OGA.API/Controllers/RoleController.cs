using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OGA.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar roles en el sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        /// <summary>
        /// Constructor del controlador, recibe el servicio de roles.
        /// </summary>
        /// <param name="roleService">Servicio para manejar roles.</param>
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Obtiene todos los roles disponibles.
        /// </summary>
        /// <returns>Lista de roles.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAll()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Obtiene un rol específico por su ID.
        /// </summary>
        /// <param name="id">Identificador del rol.</param>
        /// <returns>El rol encontrado o un error 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound(new { message = "Rol no encontrado." });

            return Ok(role);
        }

        /// <summary>
        /// Crea un nuevo rol en el sistema.
        /// </summary>
        /// <param name="roleDto">Datos del rol a crear.</param>
        /// <returns>Respuesta 201 Created con la ubicación del nuevo rol.</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RoleDTO roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _roleService.AddRoleAsync(roleDto);
                return CreatedAtAction(nameof(GetById), new { id = roleDto.RoleId }, roleDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el rol.", details = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un rol existente.
        /// </summary>
        /// <param name="id">ID del rol a actualizar.</param>
        /// <param name="roleDto">Nuevos datos del rol.</param>
        /// <returns>Código 204 No Content si la actualización es exitosa.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDTO roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRole = await _roleService.GetRoleByIdAsync(id);
            if (existingRole == null)
                return NotFound(new { message = "Rol no encontrado." });

            try
            {
                await _roleService.UpdateRoleAsync(roleDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el rol.", details = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un rol por su ID.
        /// </summary>
        /// <param name="id">ID del rol a eliminar.</param>
        /// <returns>Código 204 No Content si la eliminación fue exitosa.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRole = await _roleService.GetRoleByIdAsync(id);
            if (existingRole == null)
                return NotFound(new { message = "Rol no encontrado." });

            try
            {
                await _roleService.DeleteRoleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el rol.", details = ex.Message });
            }
        }
    }
}
