using System;
using System.ComponentModel.DataAnnotations;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationPerformer
    {
        [Key]
        public string PerformerId { get; set; }

        [Required]
        public string PerformerName { get; set; }

        [Required]
        public string PerformerDescription { get; set; }

        public bool PerformerIsBand { get; set; }

        [Required]
        public string PerformerGenre { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string PerformerEmail { get; set; }

        [Required]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Not a valid Phone number")]
        public string PerformerPhone { get; set; }

        [Required]
        public string PerformerCountry { get; set; }

        public string UserId { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}
