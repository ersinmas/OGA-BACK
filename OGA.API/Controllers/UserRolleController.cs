using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OGA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        // Constructor que inyecta el servicio de roles de usuario
        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// Obtiene todos los roles de usuario.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve una lista con todos los roles de usuario almacenados en el sistema.
        /// </remarks>
        /// <returns>Lista de roles de usuario</returns>
        /// <response code="200">Devuelve la lista de roles de usuario</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleDTO>>> GetAll()
        {
            var userRoles = await _userRoleService.GetAllUserRolesAsync();
            return Ok(userRoles);
        }

        /// <summary>
        /// Crea un nuevo rol de usuario.
        /// </summary>
        /// <param name="userRoleDto">Datos del nuevo rol de usuario a crear</param>
        /// <remarks>
        /// Este endpoint permite crear un nuevo rol de usuario proporcionando los datos necesarios.
        /// </remarks>
        /// <returns>El rol de usuario creado</returns>
        /// <response code="200">Rol de usuario creado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserRoleDTO userRoleDto)
        {
            await _userRoleService.AddUserRoleAsync(userRoleDto);
            return Ok(userRoleDto);
        }

        /// <summary>
        /// Elimina un rol de usuario.
        /// </summary>
        /// <param name="userRoleDto">Datos del rol de usuario a eliminar</param>
        /// <remarks>
        /// Este endpoint elimina un rol de usuario proporcionando los detalles del rol que se desea eliminar.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Rol de usuario eliminado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserRoleDTO userRoleDto)
        {
            await _userRoleService.DeleteUserRoleAsync(userRoleDto);
            return NoContent();
        }
    }
}
