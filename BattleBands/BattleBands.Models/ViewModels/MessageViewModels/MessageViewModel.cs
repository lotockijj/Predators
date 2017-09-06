using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBands.Models.ViewModels.MessageViewModels
{
    public class MessageViewModel
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
