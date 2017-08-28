using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models.ViewModels.MusicViewModels
{
    public class AddMusicViewModel
    {
        public string ID { get; set; }
        public IFormFile Music { get; set; }
    }
}
