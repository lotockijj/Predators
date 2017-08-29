using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Models.ViewModels.PerformerViewModels
{
    public class PerformerProfileViewModel
    {
        public ApplicationPerformer Performer { get; set; }
        public ApplicationPhoto Picture { get; set; }
    }
}
