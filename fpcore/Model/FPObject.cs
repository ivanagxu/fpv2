using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class FPObject
    {
        public int objectId { get; set; }
        public Nullable<DateTime> createDate { get; set; }
        public Nullable<DateTime> updateDate { get; set; }
        public String updateBy { get; set; }
        public bool isDeleted {get;set;}
    }
}
