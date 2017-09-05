using System;
using System.Collections.Generic;
using System.Text;
using BattleBands.Models.ApplicationModels;


namespace BattleBands.Models.ViewModels.CommentViewModels
{
    public class GetCommentsViewModel
    {
        public IEnumerable<GetPerformerCommentsViewModel> CommentsList { get; set; }
        public ApplicationComment Comment { get; set; }
    }
}
