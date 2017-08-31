using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBands.Interfaces
{
    public interface ILastItem<T>
    {
       T GetLast();
    }
}
