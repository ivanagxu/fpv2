using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class Consumption : FPObject
    {
        public decimal? total { get; set; }
        public string totalunit { get; set; }
        public decimal? store { get; set; }
        public string storeunit { get; set; }
        public decimal? used { get; set; }
        public string usedunit { get; set; }
        public DateTime? asdate { get; set; }
        public Inventory inventory { get; set; }

        public decimal? subtotal { get; set; }
        public decimal? substore { get; set; }
        public decimal? subused { get; set; }
    }
}
