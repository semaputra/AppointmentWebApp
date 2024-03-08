using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.Web.Helper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace AppointmentWebApp.Web.Controllers
{
    public class UserController : Controller
    {
        protected readonly ApiCallHelper<UserViewModel> _apiCallHelper;
        protected UserViewModel _userData = new UserViewModel();

        public UserController(ApiCallHelper<UserViewModel> apiCallHelper) { 
            _apiCallHelper = apiCallHelper;
        }

        // GET: UserController
        public ActionResult Index()
        {
            CheckSessionDataAndSet();
            var token = _apiCallHelper.CallPostApiWithJwt("", _userData, "Auth", "Auth");
            var jsonData = _apiCallHelper.CallGetApiWithJwt(token.Result, "User", "GetUsers");
            var data = JsonSerializer.Deserialize<IEnumerable<UserViewModel>>(jsonData.Result, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
            return View(data);
        }

        // GET: UserController/Details/5
        public ActionResult Details(string id)
        {
            CheckSessionDataAndSet();
            var token = _apiCallHelper.CallPostApiWithJwt("", _userData, "Auth", "Auth");
            var jsonData = _apiCallHelper.CallGetApiWithJwt(token.Result, "User", "GetUser", id);
            var data = JsonSerializer.Deserialize<UserViewModel>(jsonData.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(data);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View(new UserViewModel());
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                CheckSessionDataAndSet();
                var token = _apiCallHelper.CallPostApiWithJwt("", _userData, "Auth", "Auth");
                var newUser = new UserViewModel()
                {
                    Username = collection["Username"],
                    Password = collection["Password"],
                    Fullname = collection["Fullname"],
                    Email = collection["Email"],
                    UserRoleId = int.Parse(collection["UserRoleId"]),
                    Status = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = _userData.Username
                };
                var jsonData = _apiCallHelper.CallPostApiWithJwt(token.Result, newUser, "User", "AddUser");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(string id)
        {
            CheckSessionDataAndSet();
            var token = _apiCallHelper.CallPostApiWithJwt("", _userData, "Auth", "Auth");
            var jsonData = _apiCallHelper.CallGetApiWithJwt(token.Result, "User", "GetUser", id);
            var data = JsonSerializer.Deserialize<UserViewModel>(jsonData.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(data);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                CheckSessionDataAndSet();
                var token = _apiCallHelper.CallPostApiWithJwt("", _userData, "Auth", "Auth");
                var jsonDataOldUser = _apiCallHelper.CallGetApiWithJwt(token.Result, "User", "GetUser", id);
                var oldUser = JsonSerializer.Deserialize<UserViewModel>(jsonDataOldUser.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (oldUser != null)
                {
                    oldUser.Username = collection["Username"];
                    oldUser.Password = collection["Password"];
                    oldUser.Fullname = collection["Fullname"];
                    oldUser.Email = collection["Email"];
                    oldUser.UserRoleId = int.Parse(collection["UserRoleId"]);
                    oldUser.Status = true;
                    oldUser.ModifiedBy = _userData.Username;
                    oldUser.ModifiedDate = DateTime.Now;

                    var jsonData = _apiCallHelper.CallPostApiWithJwt(token.Result, oldUser, "User", "UpdateUser");
                    return RedirectToAction(nameof(Index)); 
                }
                throw new Exception("Data not exist");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(string id)
        {
            CheckSessionDataAndSet();
            var token = _apiCallHelper.CallPostApiWithJwt("", _userData, "Auth", "Auth");
            var jsonData = _apiCallHelper.CallGetApiWithJwt(token.Result, "User", "GetUser", id);
            var data = JsonSerializer.Deserialize<UserViewModel>(jsonData.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(data);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                CheckSessionDataAndSet();
                var token = _apiCallHelper.CallPostApiWithJwt("", _userData, "Auth", "Auth");
                var jsonDataOldUser = _apiCallHelper.CallGetApiWithJwt(token.Result, "User", "GetUser", id);
                var oldUser = JsonSerializer.Deserialize<UserViewModel>(jsonDataOldUser.Result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (oldUser != null)
                {
                    var jsonData = _apiCallHelper.CallPostApiWithJwt(token.Result, oldUser, "User", "DeleteUser");
                    return RedirectToAction(nameof(Index));
                }
                throw new Exception("Data not exist");
            }
            catch
            {
                return View();
            }
        }

        public void CheckSessionDataAndSet() {
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
            }
            else
            {
                RedirectToAction(nameof(Index));
            }
        }
    }
}
