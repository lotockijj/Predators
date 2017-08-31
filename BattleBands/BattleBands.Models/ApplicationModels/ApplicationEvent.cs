using System;
using System.ComponentModel.DataAnnotations;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationEvent
    {
        [Key]
        public string EventId {get; set;}
        public string EventName {get; set;}
        public string EventDescription {get; set;}
        public string EventPlace {get; set;}
        [Range(1,100)]
        [DataType(DataType.DateTime)]
        public DateTimeOffset EventTime {get; set; }
        public string E_UserId { get; set; }
        public DateTimeOffset CreateTime { get; set; }

    }
}
