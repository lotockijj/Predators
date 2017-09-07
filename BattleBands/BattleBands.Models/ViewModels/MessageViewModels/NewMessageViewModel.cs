using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BattleBands.Models.ViewModels.MessageViewModels
{
    public class NewMessageViewModel
    {
        [Required]
        public string Content { get; set; }
    }
}
