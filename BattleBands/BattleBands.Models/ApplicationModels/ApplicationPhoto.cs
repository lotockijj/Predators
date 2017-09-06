using BattleBands.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models.ApplicationModels
{
    public class ApplicationPhoto : IFile
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get ; set; }

        [Required]
        public string Path { get ; set; }

        public DateTimeOffset UploadTime { get; set; }

        public string IdOwner { get; set; }
    }
}
