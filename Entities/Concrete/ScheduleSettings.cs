using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ScheduleSettings : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public DateTime LastCheckTime {get;set;}


    }
}
