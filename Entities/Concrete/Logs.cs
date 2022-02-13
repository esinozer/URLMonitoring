using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class Logs : IEntity
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public string Audit { get; set; }
    }
}
