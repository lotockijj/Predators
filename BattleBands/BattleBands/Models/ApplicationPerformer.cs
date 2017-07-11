using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class ApplicationPerformer
    {
        [Key]
        public string PerformerId { get; set; }
        public string PerformerName { get; set; }
        public string PerformerDescription { get; set; }
        public bool PerformerIsBand { get; set; }
        public string PerformerGenre { get; set; }
        public string PerformerEmail { get; set; }
        public string PerformerPhone { get; set; }
        public string PerformerCountry { get; set; }
        public string UserId { get; set; }
    }
}
