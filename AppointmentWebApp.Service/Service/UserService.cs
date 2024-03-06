using AppointmentWebApp.DataAccess.Models;
using AppointmentWebApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentWebApp.Service.Service
{
    public class UserService : IUserService
    {
        private readonly AppointmentWebAppDatabaseContext _databaseContext;

        public UserService(AppointmentWebAppDatabaseContext databaseContext) { 
            _databaseContext = databaseContext;
        }

        public async Task<Guid> Add(Muser user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null.");
                }

                _databaseContext.Musers.Add(user);
                await _databaseContext.SaveChangesAsync();

                return user.UserId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding user to the database.", ex);
            }
        }

        public async Task<Muser> GetUser(Guid id)
        {
            try
            {
                var data = await _databaseContext.Musers.FindAsync(id);
                if (data == null) {
                    throw new Exception("Error occurred while retrieving users from the database.");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving user from the database.", ex);
            }
        }

        public async Task<IEnumerable<Muser>> GetUsers()
        {
            try
            {
                return await _databaseContext.Musers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving users from the database.", ex); ;
            }
        }

        public async Task<Muser> Update(Muser user)
        {
            try
            {
                _databaseContext.Entry(user).State = EntityState.Modified;
                await _databaseContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating user to the database.", ex);
            }
        }
    }
}
