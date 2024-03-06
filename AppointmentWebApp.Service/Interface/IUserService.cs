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
        Task<IEnumerable<Muser>> GetUsers();
        Task<Muser> GetUser(Guid id);
        Task<Guid> Add(Muser user);
        Task<Muser> Update(Muser user);
    }
}
