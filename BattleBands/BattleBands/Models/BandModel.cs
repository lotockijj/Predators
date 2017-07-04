using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class BandModel
    {
        public Guid IdBand;
        public Guid IdUser { get; set; }
        public string BandName { get; set; }
        public string BandDescription { get; set; }
        public PhotoModel BandPhotos { get; set; }
        public VideoModel BandVideos { get; set; }
        public MusicModel BandMusic { get; set; }

        public BandModel()
        {
            IdBand = new Guid();
        }
    }
}
