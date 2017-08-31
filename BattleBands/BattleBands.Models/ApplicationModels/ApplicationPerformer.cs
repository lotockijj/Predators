using System;
using System.ComponentModel.DataAnnotations;

namespace BattleBands.Models.ApplicationModels
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
        public DateTimeOffset CreateTime { get; set; }


        public bool isValid()
        {
            if (PerformerName != null &&
                PerformerGenre != null &&
                PerformerPhone != null &&
                PerformerEmail != null &&
                PerformerCountry != null)
                return true;
            else return false;
        }
    }
}
