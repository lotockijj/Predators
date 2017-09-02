using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationComment
    {
        [Key]
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string DestinationId { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Time { get; set; }
        public DateTimeOffset EditTime { get; set; }
    }
}
