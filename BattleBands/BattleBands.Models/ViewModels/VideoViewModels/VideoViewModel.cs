using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Models.ViewModels.VideoViewModels
{
    public class VideoViewModel
    {
        public IEnumerable<ApplicationVideo> Video { get; set; }
    }
}
