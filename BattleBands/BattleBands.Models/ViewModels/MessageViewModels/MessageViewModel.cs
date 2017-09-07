using BattleBands.Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BattleBands.Models.ViewModels.MessageViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel() { }
        public MessageViewModel(ApplicationMessage message)
        {
            Content = message.Content;
            Author = message.User.UserName;
            Timestamp = message.DateCreated;
        }
        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
    }
}
