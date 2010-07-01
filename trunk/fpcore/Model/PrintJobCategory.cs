using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class PrintJobCategory : FPObject
    {
        public string id { get; set; }
        public string category_name { get; set; }
        public string category_code { get; set; }
    }
}
