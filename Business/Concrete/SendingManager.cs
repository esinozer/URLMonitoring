using Business.Abstract;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace Business.Concrete
{
    public class SendingManager : ISendingService
    {
        private readonly IConfiguration Configuration;
        public SendingManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult SendEmail(EmailInfo emailInfo)
        {

            using (MailMessage mail = new MailMessage())
            {

                mail.From = new MailAddress(Configuration["EmailAccount:Email"], Configuration["EmailAccount:DisplayName"]);

                mail.To.Add(emailInfo.To);
                mail.Subject = emailInfo.Subject;
                mail.IsBodyHtml = true;
                mail.Body = emailInfo.Body;

                using (SmtpClient sc = new SmtpClient())
                {
                    sc.Port = Convert.ToInt32(Configuration["EmailAccount:Port"]);
                    sc.Host = Configuration["EmailAccount:Host"];
                   // sc.UseDefaultCredentials = Convert.ToBoolean(Configuration["EmailAccount:UseDefaultCredentials"]);
                    sc.EnableSsl = Convert.ToBoolean(Configuration["EmailAccount:EnableSsl"]);
                    sc.Credentials = new NetworkCredential(Configuration["EmailAccount:Username"], Configuration["EmailAccount:Password"]);
                    sc.Send(mail);
                }
            }

            return new SuccessResult();
        }
    }
}
