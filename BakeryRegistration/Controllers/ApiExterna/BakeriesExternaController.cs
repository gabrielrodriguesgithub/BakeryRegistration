using BakeryRegistration.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryRegistration.Controllers.ApiExterna
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BakeriesExternaController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly BakeriesService _bakeriesService;

        public BakeriesExternaController(UserService userService, BakeriesService bakeriesService)
        {
            _userService = userService;
            _bakeriesService = bakeriesService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetBakeries()
        {
            var usuario = _userService.GetLoggedInUser();
            if (usuario == null)
                return Unauthorized();

            var bakeries = await _bakeriesService.GetBakeriesAsync();
            return Ok(bakeries);
        }
    }
}
