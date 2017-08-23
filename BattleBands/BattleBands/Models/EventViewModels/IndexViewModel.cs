using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models.EventViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ApplicationEvent> Events { get; set; }
        public IEnumerable<ApplicationPhoto> Logos { get; set; }
    }
}
