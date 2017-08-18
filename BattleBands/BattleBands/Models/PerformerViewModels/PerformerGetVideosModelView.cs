using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models.PerformerViewModels
{
    public class PerformerGetVideosModelView
    {
        public string ID { get; set; }
        public IEnumerable<ApplicationVideo> Video { get; set; }
    }
}
