using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentWebApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DaySettingController : ControllerBase
    {
        protected readonly IAppointmentService _appointmentService;
        protected readonly IUserService _userService;
        protected readonly IDaySettingService _daySettingService;

        public DaySettingController(IAppointmentService appointmentService, IUserService userService, IDaySettingService daySettingService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _daySettingService = daySettingService;
        }
        // GET: api/<AppointmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DaySettingViewModel>>> GetDaySettings()
        {
            try
            {
                var data = await _daySettingService.GetDaySettings();
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

        // GET api/<DaySettingController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DaySettingViewModel>> GetDaySetting(long id)
        {
            try
            {
                var data = await _daySettingService.GetDaySetting(id);
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

        // POST api/<DaySettingController>
        [HttpPost]
        public async Task<ActionResult> AddDaySetting(DaySettingViewModel daySetting)
        {
            try
            {
                var result = await _daySettingService.Add(daySetting);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDaySetting(DaySettingViewModel daySetting)
        {
            try
            {
                var result = await _daySettingService.Update(daySetting);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDaySetting(DaySettingViewModel daySetting)
        {
            var oldDaySetting = await _daySettingService.GetDaySetting(daySetting.DaySettingId);
            if (oldDaySetting == null) { return NotFound(); }
            else
            {
                try
                {
                    var result = await _daySettingService.Delete(daySetting.DaySettingId);
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
