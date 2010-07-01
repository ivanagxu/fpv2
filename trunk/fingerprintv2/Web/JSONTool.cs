using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using fpcore.Model;

namespace fingerprintv2.Web
{
    public class JSONTool
    {
        public static StringBuilder customerJson = null;

        public static String getJobJson(PrintItem job)
        {
            StringBuilder jobJson = new StringBuilder();
            jobJson.Append("{").Append("jobid:'").Append(job.jobid).Append("',")
                .Append("job_type:'").Append(job.job_type == null ? "" : job.job_type.category_name).Append("',")
                .Append("handled_by:'").Append(job.handled_by == null ? "" : job.handled_by.eng_name).Append("',")
                .Append("customer_name:'").Append("").Append("',")
                .Append("job_deadline:'").Append(job.job_deadline).Append("',")
                .Append("notes:'").Append("").Append("',")
                .Append("job_status:'").Append(job.job_status).Append("'}");
            return jobJson.ToString();
        }
        public static String getJobOfOrderJson(PrintOrder order , PrintItem job)
        {
            StringBuilder jobJson = new StringBuilder();
            jobJson.Append("{").Append("pid:'").Append(job.pid).Append("',")
                .Append("received_date:'").Append(order.received_date == null ? "" : order.received_date.Value.ToString("yyyy-MM-dd")).Append("',")
                .Append("received_by:'").Append(order.received_by == null ? "" : order.received_by.eng_name).Append("',")
                .Append("order_deadline:'").Append(order.order_deadline).Append("',")
                .Append("customer_name:'").Append(order.customer_contact.customer.company_name).Append("',")
                .Append("customer_tel:'").Append(order.customer_contact.tel).Append("',")

                .Append("customer_contact_person:'").Append(order.customer_contact.contact_person).Append("',")
                .Append("remarks:'").Append(filter(order.remarks)).Append("',")
                .Append("section:'").Append(job.job_type == null ? "" : job.job_type.category_name).Append("',")
                .Append("jobid:'").Append(job.jobid).Append("',")
                .Append("filename:'").Append(job.file_name).Append("',")
                .Append("request:'");

            bool addRequest = false;
            if(job.newjob)
            {
                addRequest = true;
                jobJson.Append("New Job");
            }
            if(job.em)
            {
                if(addRequest)
                    jobJson.Append(",EM");
                else
                    jobJson.Append("EM");

                addRequest = true;
            }
            if(job.ftp)
            {
                if(addRequest)
                    jobJson.Append(",FTP");
                else
                    jobJson.Append("FTP");

                addRequest = true;
            }
            if(job.cddvd)
            {
                if(addRequest)
                    jobJson.Append(",CD/DVD");
                else
                    jobJson.Append("CD/DVD");

                addRequest = true;
            }
            if(job.mac)
            {
                if(addRequest)
                    jobJson.Append(",MAC");
                else
                    jobJson.Append("MAC");

                addRequest = true;
            }
            if(job.pc)
            {
                if(addRequest)
                    jobJson.Append(",PC");
                else
                    jobJson.Append("PC");

                addRequest = true;
            }
            if(job.test_job)
            {
                if(addRequest)
                    jobJson.Append(",Test");
                else
                    jobJson.Append("Test");

                addRequest = true;
            }
            jobJson.Append("',item_details:'");
            String itemType = "";
            for (int i = 0; i < job.print_job_items.Count; i++)
            {
                if(itemType != job.print_job_items[i].category_name)
                {
                    jobJson.Append("\\n").Append(job.print_job_items[i].category_name).Append(" : ");
                    itemType = job.print_job_items[i].category_name;
                }
                jobJson.Append(job.print_job_items[i].code_desc).Append(" ");
            }

            jobJson.Append("',").Append("notes:'").Append(job.notes).Append("',")
                .Append("handledby:'").Append(job.handled_by == null ? "" : (job.handled_by.objectId == 0 ? "" : job.handled_by.objectId.ToString())).Append("',")
                .Append("job_details:[").Append(""/*TODO*/).Append("],")
                .Append("job_status:'").Append(job.job_status).Append("',")
                .Append("purpose:'").Append(""/*TODO*/).Append("',")
                .Append("size:'").Append(""/**/).Append("'}");
            return jobJson.ToString();
        }
        public static String getJobDetailJson(PrintItem job)
        {
            StringBuilder jobJson = new StringBuilder();
            jobJson.Append("{").Append("jobid:'").Append(job.jobid).Append("',")
                .Append("job_type:'").Append(job.job_type == null ? "" : job.job_type.id).Append("',")
                .Append("job_deadline:'").Append(job.job_deadline).Append("',")
                .Append("notes:'").Append(filter(job.notes)).Append("',")
                .Append("file_name:'").Append(job.file_name).Append("',")
                .Append("mac:").Append(job.mac ? "true":"false").Append(",")
                .Append("pc:").Append(job.pc ? "true" : "false").Append(",")
                .Append("newjob:").Append(job.newjob ? "true" : "false").Append(",")
                .Append("em:").Append(job.em ? "true" : "false").Append(",")
                .Append("ftp:").Append(job.ftp ? "true" : "false").Append(",")
                .Append("cddvd:").Append(job.cddvd ? "true" : "false").Append(",")
                .Append("handled_by:'").Append(job.handled_by == null ? "" : job.handled_by.eng_name).Append("',")
                .Append("testjob:").Append(job.test_job ? "true" : "false").Append(",")
                .Append("register_date:'").Append(job.register_date).Append("',")
                .Append("print_job:'").Append(job.item_detail).Append("',")
                .Append("Fpaper:'").Append(job.Fpaper).Append("',")
                .Append("Fcolor:'").Append(job.Fcolor).Append("',")
                .Append("Fdelivery_date:'").Append(job.Fdelivery_date).Append("',")
                .Append("Fdelivery_address:'").Append(job.Fdelivery_address).Append("',")
                .Append("hold_job:").Append(job.hold_job ? "true" : "false").Append(",")
                .Append("Gpage:'").Append(job.Gpage).Append("',")
                .Append("Gcolor:'").Append(job.Gcolor).Append("',")
                .Append("job_status:'").Append(job.job_status).Append("'}");
            return jobJson.ToString();
        }
        public static String getOrderJson(PrintOrder order)
        {
            StringBuilder orderJson = new StringBuilder();
            orderJson.Append("{").Append("pid:'").Append(order.pid).Append("',")
                .Append("received_date:'").Append(order.received_date == null ? "" : order.received_date.Value.ToString("yyyy-MM-dd")).Append("',")
                .Append("update_date:'").Append(order.updateDate == null ? "" : order.updateDate.Value.ToString("yyyy-MM-dd")).Append("',")
                .Append("order_deadline:'").Append(("" + order.order_deadline).Replace("'", "`")).Append("',")
                .Append("invoice_no:'").Append(order.invoice_no).Append("',");

            if (order.customer_contact != null)
            {
                if (order.customer_contact.customer != null)
                {
                    orderJson.Append("cid:'").Append(order.customer_contact.customer.company_code).Append("',");
                }
                else
                {
                    orderJson.Append("cid:'").Append("").Append("',");
                }
            }
            else
            {
                orderJson.Append("cid:'").Append("").Append("',");
            }

            orderJson.Append("received_by:'").Append(order.received_by == null ? "" : order.received_by.eng_name).Append("',")
                .Append("sales_person:'").Append(order.sales_person == null ? "" : order.sales_person.eng_name).Append("',")
                .Append("update_by:'").Append(order.updateBy == null ? "" : order.updateBy).Append("',")
                .Append("remarks:'").Append(filter(order.remarks)).Append("',");
            if (order.customer_contact != null)
            {
                if (order.customer_contact.customer != null)
                {
                    orderJson.Append("customer_name:'").Append(order.customer_contact.customer.company_name).Append("',");
                }
                else
                {
                    orderJson.Append("customer_name:'").Append("").Append("',");
                }
            }
            else
            {
                orderJson.Append("customer_name:'").Append("").Append("',");
            }

            orderJson.Append("status:'").Append(order.status).Append("',")
                .Append("customer_tel:'").Append((order.customer_contact == null ? "" : order.customer_contact.tel).Replace("'", "`")).Append("',");

            if (order.customer_contact != null)
            {
                if (order.customer_contact.customer != null)
                {
                    orderJson.Append("customer:'").Append(order.customer_contact.customer.company_code).Append("',");
                }
                else
                {
                    orderJson.Append("customer:'").Append("").Append("',");
                }
            }
            else
            {
                orderJson.Append("customer:'").Append("").Append("',");
            }

            orderJson.Append("customer_contact_person:'").Append(order.customer_contact == null ? "": order.customer_contact.contact_person.Replace("'", "`")).Append("'}");
            return orderJson.ToString();
            
        }

        public static String getJobLookupItemsJson(PrintItemDetail lookup)
        {

            StringBuilder JobLookupItemJson = new StringBuilder();
            JobLookupItemJson.Append("{xtype:'checkbox',boxLabel:'").Append(lookup.code_desc).Append("',name:'item").Append(lookup.jid).Append("'}");
            return JobLookupItemJson.ToString();
        }

        public static String getJobTypeJson(PrintJobCategory category)
        {
            StringBuilder jobTypeJson = new StringBuilder();
            jobTypeJson.Append("{id:'").Append(category.id).Append("',name:'").Append(category.category_name).Append("'}");
            return jobTypeJson.ToString();
        }


        private static String filter(string str)
        {
            if(str == null)
                return str;

            return str.Replace("'","''").Replace("\n","\\n").Replace("\r","\\r");
        }
        
    }
}
