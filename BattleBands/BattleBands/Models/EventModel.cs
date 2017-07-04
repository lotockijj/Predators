using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class EventModel
    {
        public Guid IdEvent;
        public string EventName { get; set; }
        public string EventDescription { get; set; }
    }
}
