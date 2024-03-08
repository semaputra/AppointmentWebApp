using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace AppointmentWebApp.Web.Controllers
{
    public class AppointmentController : Controller
    {
        protected readonly ApiCallHelper<AppointmentViewModel> _apiCallHelperApp;
        protected readonly ApiCallHelper<UserViewModel> _apiCallHelperUser;
        protected UserViewModel _userData = new UserViewModel();

        public AppointmentController(ApiCallHelper<AppointmentViewModel> apiCallHelperApp, ApiCallHelper<UserViewModel> apiCallHelperUser)
        {
            _apiCallHelperApp = apiCallHelperApp;
            _apiCallHelperUser = apiCallHelperUser;
        }

        // GET: AppointmentController
        public ActionResult Index()
        {
            CheckSessionDataAndSet();
            var token = _apiCallHelperUser.CallPostApiWithJwt("", _userData, "Auth", "Auth");
            var jsonData = _apiCallHelperApp.CallGetApiWithJwt(token.Result, "Appointment", "GetAppointments");
            var data = JsonSerializer.Deserialize<IEnumerable<AppointmentViewModel>>(jsonData.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (data != null && _userData.UserRoleId == 2)
            {
                data = data.Where(x => x.UserId == _userData.UserId);
            }
            return View(data);
        }

        // GET: AppointmentController/Details/5
        public ActionResult Details(string id)
        {
            CheckSessionDataAndSet();
            var token = _apiCallHelperUser.CallPostApiWithJwt("", _userData, "Auth", "Auth");
            var jsonData = _apiCallHelperApp.CallGetApiWithJwt(token.Result, "Appointment", "GetAppointment", id);
            var data = JsonSerializer.Deserialize<AppointmentViewModel>(jsonData.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(data);
        }

        // GET: AppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                CheckSessionDataAndSet();
                var token = _apiCallHelperUser.CallPostApiWithJwt("", _userData, "Auth", "Auth");

                DateTime.TryParse(collection["AppointmentDate"], out DateTime appointmentDate);
                var newAppointment = new AppointmentViewModel()
                {
                    UserId = _userData.UserId,
                    AppointmentTitle = collection["AppointmentTitle"],
                    AppointmentPurpose = collection["AppointmentPurpose"],
                    AppointmentDate = appointmentDate,
                    LocationId = int.Parse(collection["LocationId"]),
                    Status = "Confirmed",
                    LocationDescription = collection["LocationDescription"],
                    CreatedDate = DateTime.Now,
                    CreatedBy = _userData.Username
                };

                var jsonData = _apiCallHelperApp.CallPostApiWithJwt(token.Result, newAppointment, "Appointment", "AddAppointment");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentController/Edit/5
        public ActionResult Edit(string id)
        {
            CheckSessionDataAndSet();
            var token = _apiCallHelperUser.CallPostApiWithJwt("", _userData, "Auth", "Auth");
            var jsonData = _apiCallHelperApp.CallGetApiWithJwt(token.Result, "Appointment", "GetAppointment", id);
            var data = JsonSerializer.Deserialize<AppointmentViewModel>(jsonData.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(data);
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                CheckSessionDataAndSet();
                var token = _apiCallHelperUser.CallPostApiWithJwt("", _userData, "Auth", "Auth");
                var jsonDataOldAppointment = _apiCallHelperApp.CallGetApiWithJwt(token.Result, "Appointment", "GetAppointment", id);
                var oldAppointment = JsonSerializer.Deserialize<AppointmentViewModel>(jsonDataOldAppointment.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                DateTime.TryParse(collection["AppointmentDate"], out DateTime appointmentDate);
                if (oldAppointment != null)
                {
                    oldAppointment.UserId = _userData.UserId;
                    oldAppointment.AppointmentTitle = collection["AppointmentTitle"];
                    oldAppointment.AppointmentPurpose = collection["AppointmentPurpose"];
                    oldAppointment.AppointmentDate = appointmentDate;
                    oldAppointment.LocationId = int.Parse(collection["LocationId"]);
                    oldAppointment.Status = collection["Status"];
                    oldAppointment.LocationDescription = collection["LocationDescription"];

                    var jsonData = _apiCallHelperApp.CallPostApiWithJwt(token.Result, oldAppointment, "Appointment", "UpdateAppointment");
                    return RedirectToAction(nameof(Index));
                }
                throw new Exception("Data not exist");
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentController/Delete/5
        public ActionResult Delete(string id)
        {
            CheckSessionDataAndSet();
            var token = _apiCallHelperUser.CallPostApiWithJwt("", _userData, "Auth", "Auth");
            var jsonData = _apiCallHelperApp.CallGetApiWithJwt(token.Result, "Appointment", "GetAppointment", id);
            var data = JsonSerializer.Deserialize<AppointmentViewModel>(jsonData.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(data);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                CheckSessionDataAndSet();
                var token = _apiCallHelperUser.CallPostApiWithJwt("", _userData, "Auth", "Auth");
                var jsonDataOldAppointment = _apiCallHelperApp.CallGetApiWithJwt(token.Result, "Appointment", "GetAppointment", id);
                var oldAppointment = JsonSerializer.Deserialize<AppointmentViewModel>(jsonDataOldAppointment.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (oldAppointment != null)
                {
                    var jsonData = _apiCallHelperApp.CallPostApiWithJwt(token.Result, oldAppointment, "Appointment", "DeleteAppointment");
                    return RedirectToAction(nameof(Index));
                }
                throw new Exception("Data not exist");
            }
            catch
            {
                return View();
            }
        }

        public void CheckSessionDataAndSet()
        {
            UserViewModel? data = new UserViewModel();
            var sessionData = HttpContext.Session.Get("UserData");
            if (sessionData != null)
            {
                var sessionValue = Encoding.UTF8.GetString(sessionData);
                if (sessionValue != null)
                {
                    data = JsonSerializer.Deserialize<UserViewModel>(sessionValue, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }

            if (data != null)
            {
                _userData.Username = data.Username;
                _userData.Password = data.Password;
                _userData.UserId = data.UserId;
            }
            else
            {
                RedirectToAction(nameof(Index));
            }
        }
    }
}
