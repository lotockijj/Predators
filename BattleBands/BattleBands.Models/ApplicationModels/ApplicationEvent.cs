using System;
using System.ComponentModel.DataAnnotations;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationEvent
    {
        [Key]
        public string EventId {get; set;}

        [Required]
        public string EventName {get; set;}

        [Required]
        public string EventDescription {get; set;}

        [Required]
        public string EventPlace {get; set;}

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset EventTime {get; set; }

        public string E_UserId { get; set; }

        public DateTimeOffset CreateTime { get; set; }

    }
}
