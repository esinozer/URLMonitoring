using Core.Utilities.Result;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IScheduleService
    {
        IDataResult<List<ScheduleSettings>> GetList();
        IDataResult<List<ScheduleSettings>> GetByUserId(string UserId);
        IDataResult<ScheduleSettings> GetById(int Id);
        IResult Add(ScheduleSettings ScheduleSettings);
        IResult Delete(ScheduleSettings ScheduleSettings);
        IResult Update(ScheduleSettings ScheduleSettings);
        IResult RunSchedule();

    }
}
