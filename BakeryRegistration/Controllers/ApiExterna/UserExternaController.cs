using Microsoft.AspNetCore.Mvc;
using BakeryRegistration.Models;
using BakeryRegistration.Services;
using System.Data;
using System.Diagnostics;
using BakeryRegistration.Data.DTOs;

namespace BakeryRegistration.Controllers
{

    [Route("api/[Controller]")]
    public class UserExternaController : Controller
    {
        private UserService _usuarioService;
        public UserExternaController(UserService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUserDto(CreateUserDto dto)
        {
            try
            {
                await _usuarioService.CreateUserDto(dto);
                return Created();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno. Por favor, tente novamente mais tarde.");
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var token = await _usuarioService.Login(dto);

            if (token == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            return Ok(new { token });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _usuarioService.Logout();
            return NoContent();
        }
    }
}