using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentWebApp.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUser(Guid id);
        Task<Guid> Add(UserViewModel user);
        Task<UserViewModel> Update(UserViewModel user);
        Task<UserViewModel> Delete(Guid id);
        Task<UserViewModel> Login(string username, string password);
    }
}
