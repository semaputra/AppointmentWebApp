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
    public class DaySettingService : IDaySettingService
    {
        private readonly AppointmentWebAppDatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public DaySettingService(AppointmentWebAppDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<long> Add(DaySettingViewModel daySetting)
        {
            try
            {
                if (daySetting == null)
                {
                    throw new ArgumentNullException(nameof(daySetting), "App cannot be null.");
                }
                var appMap = _mapper.Map<DaySetting>(daySetting);
                _databaseContext.DaySettings.Add(appMap);
                await _databaseContext.SaveChangesAsync();

                return daySetting.DaySettingId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding daySetting to the database.", ex);
            }
        }

        public Task<DaySettingViewModel> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<DaySettingViewModel> GetDaySetting(Guid id)
        {
            try
            {
                var data = await _databaseContext.DaySettings.FindAsync(id);
                if (data == null)
                {
                    throw new Exception("Error occurred while retrieving daySetting from the database.");
                }
                return _mapper.Map<DaySettingViewModel>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving daySetting from the database.", ex);
            }
        }

        public Task<DaySettingViewModel> GetDaySetting(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DaySettingViewModel>> GetDaySettings()
        {
            try
            {
                var daySettingsFromDb = await _databaseContext.DaySettings
                    .ToListAsync();
                var daySettingsViewModel = _mapper.Map<IEnumerable<DaySettingViewModel>>(daySettingsFromDb);
                return daySettingsViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving daySettings from the database.", ex);
            }
        }

        public async Task<DaySettingViewModel> Update(DaySettingViewModel daySetting)
        {
            try
            {
                var daySettingMap = _mapper.Map<DaySetting>(daySetting);
                _databaseContext.Entry(daySettingMap).State = EntityState.Modified;
                await _databaseContext.SaveChangesAsync();
                return _mapper.Map<DaySettingViewModel>(daySettingMap);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating daySetting to the database.", ex);
            }
        }
    }
}
