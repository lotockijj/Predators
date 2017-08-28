using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Models.ViewModels.EventViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ApplicationEvent> Events { get; set; }
        public IEnumerable<ApplicationPhoto> Logos { get; set; }
    }
}
