using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class UserAC : FPObject
    {
        public string chi_name { get; set; }
        public string eng_name { get; set; }
        public string user_name { get; set; }
        public Department dept { get; set; }
        public Section section { get; set; }
        public string user_password { get; set; }
        public List<FPRole> roles { get; set; }
    }
}
