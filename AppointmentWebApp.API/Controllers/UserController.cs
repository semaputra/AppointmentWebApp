﻿using Microsoft.AspNetCore.Mvc;
using AppointmentWebApp.DataAccess.Models;
using AppointmentWebApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using AppointmentWebApp.BussinessModel.ViewModel;

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
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
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
        public async Task<ActionResult<UserViewModel>> GetUser(Guid id)
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
        public async Task<ActionResult> AddUser(UserViewModel user)
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
        public async Task<ActionResult> UpdateUser(UserViewModel user)
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
        public async Task<ActionResult> DeleteUser(UserViewModel user)
        {
            var oldUser = await _userService.GetUser(user.UserId);
            if (oldUser == null) { return NotFound(); }
            else
            {
                try
                {
                    var result = await _userService.Delete(user.UserId);
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
