using BakeryRegistration.Data;
using BakeryRegistration.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using BakeryRegistration.Services;
using BakeryRegistration.Data.DTOs;

namespace BakeryRegistration.Controllers
{
    [Route("[Controller]")]
    public class BakeriesController : Controller
    {
        private BakeriesService _bakeriesService;

        public BakeriesController(BakeriesService bakeriesService)
        {
            _bakeriesService = bakeriesService;
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View("Bakery/CreateBakery");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateBakery(CreateBakeryDto dto)
        {
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
                return View(dto);

            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Bakery/CreateBakery", dto); 
            }
        }

    }
}
