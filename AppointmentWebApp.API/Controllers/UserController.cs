using Microsoft.AspNetCore.Mvc;
using AppointmentWebApp.DataAccess.Models;
using AppointmentWebApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentWebApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Muser>>> GetUsers()
        {
            try
            {
                var data = await _userService.GetUsers();
                if (data == null)
                {
                    return NotFound();
                }
                return data.ToList();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Muser>> GetUser(Guid id)
        {
            try
            {
                var data = await _userService.GetUser(id);
                if (data == null)
                {
                    return NotFound();
                }
                return data;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> AddUser(Muser user)
        {
            if (user.UserId == Guid.Empty)
            {
                try
                {
                    user.UserId = Guid.NewGuid();
                    var result = await _userService.Add(user);
                    return Ok(result);
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(Muser user)
        {
            if (user.UserId != Guid.Empty)
            {
                try
                {
                    var result = await _userService.Update(user);
                    return Ok(result);
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetUser(id);
            if (user == null) { return NotFound(); }
            else
            {
                try
                {
                    user.Status = false;
                    var result = await _userService.Update(user);
                    return Ok(result);
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
        }
    }
}
