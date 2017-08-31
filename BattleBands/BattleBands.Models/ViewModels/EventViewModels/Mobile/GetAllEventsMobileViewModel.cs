using System;
using System.Collections.Generic;
using System.Text;
using BattleBands.Models.ApplicationModels;
namespace BattleBands.Models.ViewModels.EventViewModels.Mobile
{
    public class GetAllEventsMobileViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTimeOffset Time { get; set; }
        public string LogoPath { get; set; }
    }
}
