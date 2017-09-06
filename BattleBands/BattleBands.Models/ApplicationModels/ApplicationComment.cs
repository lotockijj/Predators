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

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public string DestinationId { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTimeOffset Time { get; set; }

        public DateTimeOffset EditTime { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsEdited { get; set; }
    }
}
