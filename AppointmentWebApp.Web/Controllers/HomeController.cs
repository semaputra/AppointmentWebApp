using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.Web.Helper;
using AppointmentWebApp.Web.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.Json;

namespace AppointmentWebApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected readonly ApiCallHelper<UserViewModel> _apiCallHelper;

        public HomeController(ILogger<HomeController> logger, ApiCallHelper<UserViewModel> apiCallHelper)
        {
            _logger = logger;
            _apiCallHelper = apiCallHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login() { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IFormCollection collection)
        {
            try
            {
                var user = new UserViewModel() { Username = collection["Username"], Password = collection["Password"] };
                var jsonData = _apiCallHelper.CallPostApiWithJwt("", user, "Auth", "Login");
                var data = JsonSerializer.Deserialize<UserViewModel>(jsonData.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (data != null)
                {
                    if (data.Username != null && data.Password != null)
                    {
                        HttpContext.Session.SetObject("UserData", data);
                        HttpContext.Session.SetString("UserId", data.UserId.ToString());
                        HttpContext.Session.SetString("UserName", data.Username);
                        HttpContext.Session.SetString("Password", data.Password);
                        HttpContext.Session.SetInt32("Role", data.UserRoleId.HasValue ? data.UserRoleId.Value : 2);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
