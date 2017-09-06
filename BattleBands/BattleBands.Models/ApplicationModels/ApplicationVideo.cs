using System;
using System.ComponentModel.DataAnnotations;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationVideo
    {
        [Key]
        public string VideoId { get; set; }

        [Required]
        public string VideoName { get; set; }

        [Required]
        public string VideoReference { get; set; }

        [Required]
        public string VideoDescription { get; set; }

        public string OwnerID { get; set; }

        public DateTimeOffset AddTime { get; set; }

    }
}
