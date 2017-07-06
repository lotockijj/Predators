using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.ViewModels
{
    public class ProfileChangePasswordViewModel : ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
    }
}
