using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class CustomerContact : FPObject
    {
        public string company_name { get; set; }
        public Customer customer { get; set; }
        public string contact_person { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
        public string ctype { get; set; }
        public string cid { get; set; }
        public string cname { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string street3 { get; set; }
        public string city { get; set; }
        public string remarks { get; set; }
        public string mobile { get; set; }
        public string district { get; set; }
        public int deliveryid { get; set; }
    }
}
