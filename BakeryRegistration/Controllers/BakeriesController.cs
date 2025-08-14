using BakeryRegistration.Data;
using BakeryRegistration.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using BakeryRegistration.Services;
using BakeryRegistration.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Humanizer;

namespace BakeryRegistration.Controllers
{
    [Route("[Controller]")]
    public class BakeriesController : Controller
    {
        private BakeriesService _bakeriesService;
        private UserService _userService;

        public BakeriesController(BakeriesService bakeriesService, UserService userService)
        {
            _bakeriesService = bakeriesService;
            _userService = userService;
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View("Bakery/CreateBakery");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateBakery(CreateBakeryDto dto)
        {
            var usuario = _userService.GetLoggedInUser();
            if (usuario == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (dto.Photo != null && dto.Photo.Length > 0)
            {

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var filePath = Path.Combine(uploadsFolder, Path.GetFileName(dto.Photo.FileName));


                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Photo.CopyToAsync(stream);
                }

                dto.PhotoPath = $"/uploads/{Path.GetFileName(dto.Photo.FileName)}";
            }
            try
            {
                await _bakeriesService.CreateBakery(dto);
                return RedirectToAction("List", "Bakeries");

            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Bakery/CreateBakery", dto); 
            }
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetBakeriesAsync()
        {
            var usuario = _userService.GetLoggedInUser();
            if (usuario == null)
            {
                return RedirectToAction("Login", "User");
            }
            var bakeries = await _bakeriesService.GetBakeriesAsync();
            return View("Bakery/ListBakeries", bakeries);
        }

        [HttpPut("Put")]
        public IActionResult UpdateBakery([FromBody] BakeryModel bakery)
        {
            try
            {
                _bakeriesService.UpdateBakery(bakery);
                return Ok(bakery);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Bakery/ListBakeries", bakery);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteBakery(string id)
        {
            _bakeriesService.DeleteBakery(id);

            return NoContent();
        }

    }
}
