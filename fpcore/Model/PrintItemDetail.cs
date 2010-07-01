using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class PrintItemDetail : FPObject
    {
        public int jid { get; set; }
        public string code_desc { get; set; }
        public string category { get; set; }
        public string ordering { get; set; }
        public string category_name { get; set; }
    }
}
