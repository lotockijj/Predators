using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class ApplicationVideo
    {
        [Key]
        public string VideoId { get; set; }
        public string VideoName { get; set; }
        public string VideoReference { get; set; }
        public string VideoDescription { get; set; }
        public string OwnerID { get; set; }
    }
}
