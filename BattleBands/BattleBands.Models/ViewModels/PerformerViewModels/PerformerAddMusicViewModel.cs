using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Models.ViewModels.PerformerViewModels
{
    public class PerformerAddMusicViewModel
    {
        public ApplicationMusic Music { get; set; }
        public string ID { get; set; }
    }
}
