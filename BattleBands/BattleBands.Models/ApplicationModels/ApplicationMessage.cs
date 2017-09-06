using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationMessage
    {
        [Key]
        public string MessageID { get; set; }

        public string Content { get; set; }

        [ForeignKeyAttribute("ApplicationUser")]
        [Required]
        public string UserID { get; set; }

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
    }
}
