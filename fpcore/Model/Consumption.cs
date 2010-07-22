using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class Consumption : FPObject
    {
        public string total { get; set; }
        public string totalunit { get; set; }
        public string store { get; set; }
        public string storeunit { get; set; }
        public string  used { get; set; }
        public string usedunit { get; set; }
        public DateTime? asdate { get; set; }
        public Inventory inventory { get; set; }

        public decimal? subtotal { get; set; }
        public decimal? substore { get; set; }
        public decimal? subused { get; set; }
    }
}
