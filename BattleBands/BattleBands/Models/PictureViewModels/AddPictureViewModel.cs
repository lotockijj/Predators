using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models.PictureViewModels
{
    public class AddPictureViewModel
    {
        public string ID { get; set; }
        public IFormFile Photo { get; set; }
    }
}
