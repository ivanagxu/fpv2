using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class CustomerContact : FPObject
    {
        public Customer customer { get; set; }
        public string contact_person { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
    }
}
