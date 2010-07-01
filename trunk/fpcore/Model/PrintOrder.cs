using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpcore.Model
{
    public class PrintOrder : FPObject
    {

        public static readonly string STATUS_PENDING = "Pending";
        public static readonly string STATUS_FINISH = "Finish";
        public static readonly string STATUS_INPROGRESS = "In Progress";
        public static readonly string STATUS_NEWG = "New";

        public string pid { get; set; }
        public Nullable<DateTime> received_date { get; set; }
        public string order_deadline { get; set; }
        public string invoice_no { get; set; }
        public CustomerContact customer_contact { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }

        public List<PrintItem> print_job_list { get; set; }
        public UserAC received_by { get; set; }
        public UserAC sales_person { get; set; }
    }
}
