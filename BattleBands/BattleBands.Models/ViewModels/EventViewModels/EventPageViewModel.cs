using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Models.ViewModels.EventViewModels
{
    public class EventPageViewModel
    {
        public ApplicationEvent Event { get; set; }
        public ApplicationPhoto Logo { get; set; }
    }
}
