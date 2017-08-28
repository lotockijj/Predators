using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Interfaces
{
    public interface IFile
    {
         string Id { get; set; }
         string Name { get; set; }
         string Path { get; set; }
         string IdOwner { get; set; }
    }
}
