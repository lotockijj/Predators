using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Models.ViewModels.VideoViewModels
{
    public class PerformerGetVideosViewModel
    {
        public string ID { get; set; }
        public IEnumerable<ApplicationVideo> Video { get; set; }
    }
}
