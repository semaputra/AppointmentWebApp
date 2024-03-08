using AppointmentWebApp.BussinessModel.ViewModel;
using AppointmentWebApp.DataAccess.Models;
using AppointmentWebApp.Service.Interface;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(AppointmentWebAppDatabaseContext databaseContext, IMapper mapper) { 
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<Guid> Add(UserViewModel user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null.");
                }
                var userMap = _mapper.Map<Muser>(user);
                _databaseContext.Musers.Add(userMap);
                await _databaseContext.SaveChangesAsync();

                var role = _databaseContext.Mroles.Where(x => x.RoleId == user.UserRoleId).FirstOrDefault();
               
                var ur = new RuserRole { Role = role, User = userMap };

                _databaseContext.RuserRoles.Add(ur);
                _databaseContext.SaveChanges();

                return user.UserId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding user to the database.", ex);
            }
        }

        public async Task<UserViewModel> GetUser(Guid id)
        {
            try
            {
                var data = await _databaseContext.Musers
                    .Join(_databaseContext.RuserRoles, user => user.UserId, role => role.UserId,
                    (user, role) => new UserViewModel()
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email,
                        Fullname = user.Fullname,
                        Status = user.Status,
                        CreatedDate = user.CreatedDate,
                        CreatedBy = user.CreatedBy,
                        ModifiedDate = user.ModifiedDate,
                        ModifiedBy = user.ModifiedBy,
                        UserRoleId = role.RoleId
                    }).Where(x => x.UserId == id).FirstOrDefaultAsync();
                if (data == null) {
                    throw new Exception("Error occurred while retrieving users from the database.");
                }
                return _mapper.Map<UserViewModel>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving user from the database.", ex);
            }
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            try
            {
                var usersFromDb = await _databaseContext.Musers
                    .Join(_databaseContext.RuserRoles, user => user.UserId, role => role.UserId,
                    (user,role) => new UserViewModel() { 
                        UserId = user.UserId,
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email,
                        Fullname = user.Fullname,
                        Status = user.Status,
                        CreatedDate = user.CreatedDate,
                        CreatedBy = user.CreatedBy,
                        ModifiedDate = user.ModifiedDate,
                        ModifiedBy = user.ModifiedBy,
                        UserRoleId = role.RoleId
                    })
                    .ToListAsync();
                var usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(usersFromDb);
                return usersViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving users from the database.", ex);
            }
        }

        public async Task<UserViewModel> Update(UserViewModel user)
        {
            try
            {
                var userMap = _mapper.Map<Muser>(user);
                _databaseContext.Entry(userMap).State = EntityState.Modified;               
                await _databaseContext.SaveChangesAsync();

                var rolmap = _databaseContext.RuserRoles.Where(x => x.UserId == user.UserId).FirstOrDefault();
                if (rolmap != null && user.UserRoleId.HasValue)
                {
                    rolmap.RoleId = user.UserRoleId.Value;
                    _databaseContext.SaveChanges();
                }

                return _mapper.Map<UserViewModel>(userMap);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating user to the database.", ex);
            }
        }

        public async Task<UserViewModel> Delete(Guid id)
        {
            try
            {
                var data = await _databaseContext.Musers.FindAsync(id);
                if (data != null)
                {
                    data.Status = false;
                    _databaseContext.Entry(data).State = EntityState.Modified;
                    await _databaseContext.SaveChangesAsync();
                    return _mapper.Map<UserViewModel>(data);
                }
                throw new Exception("Error occurred while updating user to the database.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating user to the database.", ex);
            }
        }

        public async Task<UserViewModel> Login(string username, string password)
        {
            try
            {
                var data = await _databaseContext.Musers
                    .Join(_databaseContext.RuserRoles, user => user.UserId, role => role.UserId,
                    (user, role) => new UserViewModel()
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email,
                        Fullname = user.Fullname,
                        Status = user.Status,
                        CreatedDate = user.CreatedDate,
                        CreatedBy = user.CreatedBy,
                        ModifiedDate = user.ModifiedDate,
                        ModifiedBy = user.ModifiedBy,
                        UserRoleId = role.RoleId
                    }).SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
                if (data == null)
                {
                    throw new Exception("Error occurred while retrieving users from the database.");
                }
                return _mapper.Map<UserViewModel>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving users from the database.", ex);
            }
        }
    }
}
