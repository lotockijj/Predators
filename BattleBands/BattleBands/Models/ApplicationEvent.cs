using System;
using System.ComponentModel.DataAnnotations;

namespace BattleBands.Models
{
    public class ApplicationEvent
    {
        [Key]
        public string EventId {get; set;}
        public string EventName {get; set;}
        public string EventDescription {get; set;}
        public string EventPlace {get; set;}
        public DateTimeOffset EventTime {get; set; }
        public string E_UserId { get; set; }
    }
}
