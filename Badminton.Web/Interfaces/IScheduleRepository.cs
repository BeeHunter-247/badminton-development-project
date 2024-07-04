using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IScheduleRepository
    {
        Task<Schedule> Create(Schedule scheduleModel);
        Task<List<Schedule>> GetAll();
        Task<Schedule?> GetById(int id);
        Task<Schedule?> Update(int id, UpdateScheduleDTO scheduleDTO);
        Task<Schedule?> Delete(int id);
        Task<bool> ScheduleExistsAsync(Schedule scheduleCheck);
    }
}
