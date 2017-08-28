using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Models.ViewModels.MusicViewModels
{
    public class GetMusicViewModel
    {
        public IEnumerable<ApplicationMusic> Music { get; set; }
        public string ID { get; set; }
    }
}
