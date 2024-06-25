using Badminton.Web.DTO;
using Badminton.Web.Helpers;
using Badminton.Web.Models;

namespace Badminton.Web.Interfaces
{
    public interface IScheduleRepository
    {
        Task<Schedule> Create(Schedule scheduleModel);
        //Task<ScheduleDTO> Update(int id, ScheduleDTO scheduleDto);
        Task<bool> Delete(int id);
        Task<List<ScheduleDTO>> GetAll();
        Task<ScheduleDTO> GetById(int id);
        Task<bool> ScheduleExistsAsync(Schedule scheduleCheck);


    }
}
