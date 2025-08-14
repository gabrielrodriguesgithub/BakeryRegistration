using Microsoft.AspNetCore.Mvc;
using BakeryRegistration.Models;
using BakeryRegistration.Services;
using System.Data;
using System.Diagnostics;
using BakeryRegistration.Data.DTOs;

namespace BakeryRegistration.Controllers
{

    [Route("[Controller]")]
    public class UserController : Controller
    {
        private UserService _usuarioService;
        public UserController(UserService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View("Users/Register");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUserDto(CreateUserDto dto)
        {
            try
            {
                await _usuarioService.CreateUserDto(dto);
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Users/Register", dto);
            }
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View("Users/Login");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login( LoginUserDto dto)
        {
            var token = await _usuarioService.Login(dto);

            var user = _usuarioService.GetLoggedInUser();

            if (token is null || user is null) return BadRequest("Falha na autenticação");
            return Json(new { token, redirectUrl = "Bakeries/List" });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _usuarioService.Logout();
            return View("Users/Login");
        }
    }
}