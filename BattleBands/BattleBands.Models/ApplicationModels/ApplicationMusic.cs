using BattleBands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationMusic : IFile
    {
        public string Id { get; set; }
        public string Name { get ; set ; }
        public string Path { get ; set ; }
        public string IdOwner { get; set; }
        public DateTimeOffset UploadTime { get; set; }
    }
}
