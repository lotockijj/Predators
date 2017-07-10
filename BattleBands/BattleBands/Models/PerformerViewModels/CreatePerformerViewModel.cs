using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models.PerformerViewModels
{
    public class CreatePerformerViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string PerformerEmail { get; set; }
        [Required]
        [Display(Name = "Ім'я виконавця")]
        public string PerformerName { get; set; }

    }
}
