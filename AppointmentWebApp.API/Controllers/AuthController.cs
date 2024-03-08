using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.DataAccess.Models;
using AppointmentWebApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppointmentWebApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Auth(UserViewModel user)
        {
            IActionResult response = Unauthorized();
            if (user.Username == null || user.Password == null)
            {
                return BadRequest();
            }
            var data = await _userService.Login(user.Username, user.Password);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    var issuer = _configuration["Jwt:Issuer"];
                    var audience = _configuration["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha512Signature
                                        );

                    var subject = new ClaimsIdentity(new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Email, user.Username)
                });

                    var expires = DateTime.UtcNow.AddMinutes(10);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = subject,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = signingCredentials
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    return Ok(jwtToken);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserViewModel user)
        {
            if (user.Username == null || user.Password == null)
            {
                return BadRequest();
            }
            var data = await _userService.Login(user.Username, user.Password);
            if (data == null) { return NotFound(); }
            else
            {
                try
                {
                    return Ok(data);
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
        }
    }
}
