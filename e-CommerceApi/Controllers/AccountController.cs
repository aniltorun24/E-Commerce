using e_CommerceApi.Dto;
using e_CommerceApi.Models.Entity;
using e_CommerceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_CommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return BadRequest(new { message = "username hatalı" });
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (result)
            {
                return Ok(new
                {
                    token = await _tokenService.CreateTokenAsync(user)
                });
            }
            return Unauthorized();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new AppUser
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return StatusCode(201);
            }
            return BadRequest(result.Errors);
        }
    }
}
