using BattleBands.Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBands.Models
{
    public class RecentItems
    {
        public ApplicationPerformer Performer { get; set; }
        public string PerformerPath { get; set; }

        public ApplicationEvent Event { get; set; }
        public string EventPath { get; set; }

        public ApplicationVideo Video { get; set; }

        public ApplicationMusic Music { get; set; }
    }
}
