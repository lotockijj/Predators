using System;
using System.Collections.Generic;
using System.Text;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Models.ViewModels.CommentViewModels
{
    public class GetPerformerCommentsViewModel
    {
        public ApplicationComment Comment { get; set; }
        public string Name { get; set; }
    }
}
