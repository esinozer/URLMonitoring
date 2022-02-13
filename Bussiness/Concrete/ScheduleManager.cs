using Business.Abstract;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Business.Concrete
{
    public class ScheduleManager : IScheduleService
    {
        private IScheduleSettingsDal _IScheduleSettingsDal;
        
        private ISendingService _ISendingService;

        public ScheduleManager(IScheduleSettingsDal scheduleSettingsDal,IIdentityService identityService,ISendingService sendingService)
        {
            _IScheduleSettingsDal = scheduleSettingsDal;
            
            _ISendingService = sendingService;
        }
        public IResult Add(ScheduleSettings ScheduleSettings)
        {

            _IScheduleSettingsDal.Add(ScheduleSettings);
            return new SuccessResult("Settings Eklendi");
        }
        public IResult Delete(ScheduleSettings ScheduleSettings)
        {
            _IScheduleSettingsDal.Delete(ScheduleSettings);

            return new SuccessResult("Silindi");
        }

        public IDataResult<ScheduleSettings> GetById(int Id)
        {

            return new SuccessDataResult<ScheduleSettings>(_IScheduleSettingsDal.Get(x => x.Id == Id));
        }

        public IDataResult<List<ScheduleSettings>> GetByUserId(string UserId)
        {
            return new SuccessDataResult<List<ScheduleSettings>>(_IScheduleSettingsDal.Getlist(x => x.UserId == UserId).ToList());
        }
        public IDataResult<List<ScheduleSettings>> GetList()
        {
            return new SuccessDataResult<List<ScheduleSettings>>(_IScheduleSettingsDal.Getlist().ToList());
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult RunSchedule()
        {
            var DateNow = DateTime.Now;

            var Schedules = GetList().Data.Where(x => x.LastCheckTime.AddMinutes(x.Interval) < DateNow).ToList();

            foreach (var item in Schedules)
            {
                var result = UrlCheck(item.Url);
                if(!result)
                {
                    EmailInfo emailInfo = new EmailInfo();
                    emailInfo.Body = $"Alert!{item.Name} app is down.Target Url :{item.Url} ";
                    emailInfo.Subject = item.Name;
                    emailInfo.To = item.UserEmail;

                    var resulsend = _ISendingService.SendEmail(emailInfo);

                }
                 item.LastCheckTime = DateNow;
                Update(item);               
            }
            return new SuccessResult();
        }
        public bool UrlCheck(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public IResult Update(ScheduleSettings ScheduleSettings)
        {

            _IScheduleSettingsDal.Update(ScheduleSettings);

            return new SuccessResult("Kayıt Güncellendi.");
        }
    }
}
