﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battle_Bands.Models
{
    public class PhotoModel
    {
        public Guid IdPhoto;
        public Guid IdBand;
        public string PhotoName;
        public List<string> PhotoReferences { get; set; }
    }
}
