using AppointmentWebApp.BussinessModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentWebApp.Service.Interface
{
    public interface IDaySettingService
    {
        Task<IEnumerable<DaySettingViewModel>> GetDaySettings();
        Task<DaySettingViewModel> GetDaySetting(long id);
        Task<long> Add(DaySettingViewModel daySetting);
        Task<DaySettingViewModel> Update(DaySettingViewModel daySetting);
        Task<DaySettingViewModel> Delete(long id);
    }
}
