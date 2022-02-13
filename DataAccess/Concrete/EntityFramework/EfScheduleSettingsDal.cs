using Core.Entities.Concrete;
using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfScheduleSettingsDal : EfEntityRepositoryBase<ScheduleSettings, MainDbContext>, IScheduleSettingsDal
    {
    }
}
