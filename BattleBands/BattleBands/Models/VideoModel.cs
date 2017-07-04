using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class VideoModel
    {
        public Guid IdVideo;
        public Guid IdBand;
        public string VideoName;
        public List<string> VideoReferences { get; set; }
    }
}
