using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class MusicModel
    {
        public Guid IdMusic;
        public Guid IdBand;
        public string MusicName;
        public List<string> MusicReferences { get; set; }
    }
}
