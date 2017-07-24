using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models.PerformerViewModels
{
    public class GetVideoAndInfoViewModel
    {
        public ApplicationVideo video { get; set; }
        public Uri reference { get; set; }
    }
}
