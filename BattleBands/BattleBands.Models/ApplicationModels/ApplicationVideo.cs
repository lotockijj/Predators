using System;
using System.ComponentModel.DataAnnotations;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationVideo
    {
        [Key]
        public string VideoId { get; set; }
        public string VideoName { get; set; }
        public string VideoReference { get; set; }
        public string VideoDescription { get; set; }
        public string OwnerID { get; set; }
        public DateTimeOffset AddTime { get; set; }

    }
}
