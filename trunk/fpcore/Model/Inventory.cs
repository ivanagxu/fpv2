using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class Inventory:FPObject 
    {
        public string orderno { get; set; }
        public string assetno { get; set; }
        public string category { get; set; }
        public DateTime? receiveddate { get; set; }
        public UserAC receivedby { get; set; }
        public DateTime? orderdeadline { get; set; }
        public string remark { get; set; }
        public string contactperson { get; set; }
        public string Tel { get; set; }
        public string productno { get; set; }
        public string productnameen{ get; set; }
        public string productnamecn { get; set; }
        public string description { get; set; }
        public string dimension { get; set; }
        public string unit { get; set; }
        public string unitcost { get; set; }
        public string quantity { get; set; }
        public string stored { get; set; }
    }
}
