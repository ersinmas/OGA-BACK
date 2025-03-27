using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OGA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // Protegemos con JWT
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor que inyecta el servicio de usuario
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        /// <remarks>
        /// Este endpoint devuelve una lista de todos los usuarios almacenados en el sistema.
        /// </remarks>
        /// <returns>Lista de usuarios</returns>
        /// <response code="200">Devuelve la lista de usuarios</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a buscar</param>
        /// <remarks>
        /// Este endpoint devuelve los detalles de un usuario específico por su ID.
        /// </remarks>
        /// <returns>Detalles del usuario</returns>
        /// <response code="200">Devuelve los detalles del usuario</response>
        /// <response code="404">Si no se encuentra el usuario con el ID proporcionado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="userDto">Datos del nuevo usuario a crear</param>
        /// <remarks>
        /// Este endpoint permite crear un nuevo usuario proporcionando los datos necesarios.
        /// La contraseña se recibe sin encriptar y es hasheada antes de ser almacenada.
        /// </remarks>
        /// <returns>El usuario creado</returns>
        /// <response code="201">Usuario creado exitosamente</response>
        /// <response code="400">Datos incorrectos en la solicitud</response>
        [HttpPost("createUser")]
        public async Task<ActionResult> Create([FromBody] UserDTO userDto)
        {
            var passHash = new PasswordHasher<object>();
            string hashedPassword = passHash.HashPassword(null, userDto.PasswordHash);
            userDto.PasswordHash = hashedPassword;
            await _userService.AddUserAsync(userDto);
            return CreatedAtAction(nameof(GetById), new { id = userDto.UserId }, userDto);
        }

        /// <summary>
        /// Inicia sesión de un usuario.
        /// </summary>
        /// <param name="userDto">Credenciales del usuario para el login</param>
        /// <remarks>
        /// Este endpoint valida las credenciales del usuario y genera un token JWT si el login es exitoso.
        /// </remarks>
        /// <returns>Un token JWT en caso de éxito</returns>
        /// <response code="200">Login exitoso, retorna un token JWT</response>
        /// <response code="400">Credenciales incorrectas</response>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO userDto)
        {
            var user = await _userService.SearchUser(userDto);

            var passHash = new PasswordHasher<string>();
            var verificationResult = passHash.VerifyHashedPassword(user.Email, user.PasswordHash, userDto.PasswordHash);

            if (user == null)
            {
                throw new InvalidOperationException("Usuario incorrecto");
            }
            if (verificationResult == PasswordVerificationResult.Success)
            {
                var token = await _userService.GenerateJwtToken("dd");
                return Ok(new { token });
            }
            else
            {
                throw new InvalidOperationException("Contraseña incorrecta");
            }
        }

        /// <summary>
        /// Actualiza los datos de un usuario.
        /// </summary>
        /// <param name="id">El ID del usuario a actualizar</param>
        /// <param name="userDto">Los nuevos datos del usuario</param>
        /// <remarks>
        /// Este endpoint permite actualizar los detalles de un usuario. El ID del usuario en la URL debe coincidir con el ID en los datos del usuario.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Usuario actualizado exitosamente</response>
        /// <response code="400">ID de usuario no coincide con el proporcionado</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.UserId)
                return BadRequest("ID mismatch");

            await _userService.UpdateUserAsync(userDto);
            return NoContent();
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a eliminar</param>
        /// <remarks>
        /// Este endpoint elimina un usuario del sistema utilizando su ID.
        /// </remarks>
        /// <returns>Resultado de la operación</returns>
        /// <response code="204">Usuario eliminado exitosamente</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
