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
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentWebAppDatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public AppointmentService(AppointmentWebAppDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<Guid> Add(AppointmentViewModel appointment)
        {
            try
            {
                if (appointment == null)
                {
                    throw new ArgumentNullException(nameof(appointment), "App cannot be null.");
                }
                var appMap = _mapper.Map<Tappointment>(appointment);
                _databaseContext.Tappointments.Add(appMap);
                await _databaseContext.SaveChangesAsync();

                return appointment.AppointmentId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding appointment to the database.", ex);
            }
        }

        public async Task<AppointmentViewModel> Delete(Guid id)
        {
            try
            {
                var data = await _databaseContext.Tappointments.FindAsync(id);
                if (data != null)
                {
                    data.Status = "Canceled";
                    _databaseContext.Entry(data).State = EntityState.Modified;
                    await _databaseContext.SaveChangesAsync();
                    return _mapper.Map<AppointmentViewModel>(data);
                }
                throw new Exception("Error occurred while updating appointment to the database.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating appointment to the database.", ex);
            }
        }

        public async Task<AppointmentViewModel> GetAppointment(Guid id)
        {
            try
            {
                var data = await _databaseContext.Tappointments.FindAsync(id);
                if (data == null)
                {
                    throw new Exception("Error occurred while retrieving appointment from the database.");
                }
                return _mapper.Map<AppointmentViewModel>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving appointment from the database.", ex);
            }
        }

        public async Task<IEnumerable<AppointmentViewModel>> GetAppointments()
        {
            try
            {
                var appointmentsFromDb = await _databaseContext.Tappointments
                    .Join(_databaseContext.Mlocations,
                        appointment => appointment.LocationId,
                        location => location.LocationId,
                        (appointment, location) => new { Appointment = appointment, Location = location })
                    .Join(_databaseContext.Musers,
                        combined => combined.Appointment.UserId,
                        user => user.UserId,
                        (combined, user) => new AppointmentViewModel
                        {
                            AppointmentId = combined.Appointment.AppointmentId,
                            UserId = combined.Appointment.UserId,
                            AppointmentTitle = combined.Appointment.AppointmentTitle,
                            AppointmentPurpose = combined.Appointment.AppointmentPurpose,
                            LocationId = combined.Appointment.LocationId,
                            LocationName = combined.Location.LocationName,
                            Username = user.Username,
                            AppointmentDate = combined.Appointment.AppointmentDate,
                            Status = combined.Appointment.Status,
                            CreatedDate = combined.Appointment.CreatedDate,
                            CreatedBy = combined.Appointment.CreatedBy,
                            LocationDescription = combined.Appointment.LocationDescription,
                            AppointmentDateStr = combined.Appointment.AppointmentDate.HasValue ? combined.Appointment.AppointmentDate.Value.ToString("MM-dd-yyyy") : ""
                        })
                    .ToListAsync();
                var appointmentsViewModel = _mapper.Map<IEnumerable<AppointmentViewModel>>(appointmentsFromDb);
                return appointmentsViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving appointments from the database.", ex);
            }
        }

        public async Task<AppointmentViewModel> Update(AppointmentViewModel appointment)
        {
            try
            {
                var appointmentMap = _mapper.Map<Tappointment>(appointment);
                _databaseContext.Entry(appointmentMap).State = EntityState.Modified;
                await _databaseContext.SaveChangesAsync();
                return _mapper.Map<AppointmentViewModel>(appointmentMap);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating appointment to the database.", ex);
            }
        }
    }
}
