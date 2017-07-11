using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class ApplicationEvent
    {
        [Key]
        public string EventId{get;set;}
        public string EventName{get;set;}
        public string EventDescription{get;set;}
        public string EventPlace {get;set;}
        public DateTimeOffset EventTime { get; set; }
    }
}
