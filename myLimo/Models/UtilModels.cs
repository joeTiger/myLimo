using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myLimo.Models
{
    public class LgSetting
    {
        public string Name { get; set; }
        public string Value{ get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Address{ get; set; }
        public string City { get; set; }
        public string Tel { get; set; }
        public string Mob { get; set; }
        public string Email{ get; set; }
        public string WWW { get; set; }
    }
}