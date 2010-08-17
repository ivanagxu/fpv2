using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class AssetConsumption : FPObject
    {
        public string jobid { get; set; }
        public string qty { get; set; }
        public string size { get; set; }
        public string unit { get; set; }
        public int purpose { get; set; }
        public string cost { get; set; }
        public Inventory product { get; set; }
    }
}
