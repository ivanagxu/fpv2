using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class Department :FPObject
    {
        public string name { get; set; }
        public string other { get; set; }
        public Department dept { get; set; }
    }
}
