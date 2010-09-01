using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class Delivery : FPObject
    {
        public string number { get; set; }
        public string non_order { get; set; }
        public string part_no { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string delivery_type { get; set; }
        public UserAC requested_by { get; set; }
        public UserAC handled_by { get; set; }
        public string notes { get; set; }
        public UserAC assigned_by { get; set; }
        public DateTime? deadline { get; set; }
        public string status { get; set; }
        public CustomerContact contact { get; set; }
        public string remarks { get; set; }
        public Customer customer { get; set; }
    }
}
