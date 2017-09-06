﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationMessage
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }
    }
}
