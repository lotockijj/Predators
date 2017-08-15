using BattleBands.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class ApplicationPhoto : IFile
    {
        [Key]
        public int Id { get; set; }
        public string Name { get ; set; }
        public string Path { get ; set; }
    }
}
