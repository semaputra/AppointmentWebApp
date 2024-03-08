using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentWebApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        protected readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        // GET: api/<AppointmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentViewModel>>> GetAppointments()
        {
            try
            {
                var data = await _appointmentService.GetAppointments();
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

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentViewModel>> GetAppointment(Guid id)
        {
            try
            {
                var data = await _appointmentService.GetAppointment(id);
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

        // POST api/<AppointmentController>
        [HttpPost]
        public async Task<ActionResult> AddAppointment(AppointmentViewModel appointment)
        {
            if (appointment.AppointmentId == Guid.Empty)
            {
                try
                {
                    appointment.AppointmentId = Guid.NewGuid();
                    var result = await _appointmentService.Add(appointment);
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
        public async Task<ActionResult> UpdateAppointment(AppointmentViewModel appointment)
        {
            if (appointment.AppointmentId != Guid.Empty)
            {
                try
                {
                    var result = await _appointmentService.Update(appointment);
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
        public async Task<ActionResult> DeleteAppointment(AppointmentViewModel appointment)
        {
            var oldAppointment = await _appointmentService.GetAppointment(appointment.AppointmentId);
            if (oldAppointment == null) { return NotFound(); }
            else
            {
                try
                {
                    var result = await _appointmentService.Delete(appointment.AppointmentId);
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
