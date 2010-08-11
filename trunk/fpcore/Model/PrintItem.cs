using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class PrintItem : FPObject
    {
        public static readonly string STATUS_PENDING = "Pending";
        public static readonly string STATUS_FINISH = "Finished";
        public static readonly string STATUS_INPROGRESS = "In Progress";
        public static readonly string STATUS_NEWG = "New";

        public string jobid { get; set; }
        public string pid { get; set; }
        public string notes { get; set; }
        public bool mac { get; set; }
        public bool pc { get; set; }
        public bool newjob { get; set; }
        public bool em { get; set; }
        public bool ftp { get; set; }
        public bool cddvd { get; set; }
        public bool test_job { get; set; }
        public string register_date { get; set; }
        public string job_deadline { get; set; }
        public string item_detail { get; set; }
        public string Fpaper { get; set; }
        public string Fcolor { get; set; }
        public string Fdelivery_date { get; set; }
        public string Fdelivery_address { get; set; }
        public string job_status { get; set; }
        public string file_name { get; set; }
        public bool hold_job{ get; set; }
        public string Gpage { get; set; }
        public string Gcolor { get; set; }

        public List<AssetConsumption> print_job_detail { get; set; }
        public UserAC handled_by { get; set; }
        public PrintJobCategory job_type { get; set; }

        public List<PrintItemDetail> print_job_items { get; set; }
    }
}
