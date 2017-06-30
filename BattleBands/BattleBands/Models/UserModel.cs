using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battle_Bands.Models
{
    public class UserModel
    {
        public Guid IdUser;
        public string Username { get; set; }
        public string Password { get; set; }
        public BandModel UserBands { get; set; }
        public UserModel(string username, string password)
        {
            IdUser = new Guid();
            Username = username;
            Password = password;
        }
    }
}
