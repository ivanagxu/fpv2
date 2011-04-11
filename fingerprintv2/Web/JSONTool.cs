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

        public static String getJobJson(PrintItem job, PrintOrder order)
        {
            String customername = "";
            if (order != null)
            {
                if (order.customer_contact != null)
                {
                    if (order.customer_contact.customer != null)
                    {
                        customername = order.customer_contact.customer.company_name;
                    }
                }
            }
            StringBuilder jobJson = new StringBuilder();
            jobJson.Append("{").Append("jobid:'").Append(job.jobid).Append("',")
                .Append("job_type:'").Append(job.job_type == null ? "" : job.job_type.category_name).Append("',")
                .Append("handled_by:'").Append(job.handled_by == null ? "" : job.handled_by.eng_name).Append("',")
                .Append("customer_name:'").Append(filter(customername)).Append("',")
                .Append("job_deadline:'").Append(job.job_deadline).Append("',")
                .Append("notes:'").Append(filter(job.notes)).Append("',")
                .Append("job_status:'").Append(job.job_status).Append("'}");
            return jobJson.ToString();
        }

        public static string getCustomerJson(Customer customer, CustomerContact cc)
        {
            if (customer == null)
                customer = new Customer();
            if (cc == null)
                cc = new CustomerContact();

            StringBuilder customerJson = new StringBuilder();
            customerJson.Append("{").Append("objectid:'").Append(customer.objectId.ToString()).Append("',")
                .Append("company_code:'").Append(customer.company_code == null ? string.Empty : customer.company_code.ToString()).Append("',")
                .Append("company_name:'").Append(customer.company_name == null ? string.Empty : filter(customer.company_name.ToString())).Append("',")
                 .Append("contact_objectid:'").Append(cc.objectId.ToString()).Append("',")
                 .Append("contact_person:'").Append(cc.contact_person == null ? string.Empty : cc.contact_person.ToString()).Append("',")
                .Append("contact_tel:'").Append(cc.tel == null ? string.Empty : cc.tel.ToString()).Append("',")
                .Append("email:'").Append(cc.email == null ? string.Empty : cc.email.ToString()).Append("',")
                .Append("deliveryid:'").Append(cc.deliveryid.ToString ()).Append("',")
                 .Append("street1:'").Append(cc.street1 == null ? string.Empty : cc.street1.ToString().Replace("'", "\\\'")).Append("',")
                 .Append("street2:'").Append(cc.street2 == null ? string.Empty : cc.street2.ToString().Replace("'", "\\\'")).Append("',")
                 .Append("street3:'").Append(cc.street3 == null ? string.Empty : cc.street3.ToString().Replace("'", "\\\'").Replace ("\n","").Replace ("\r","")).Append("',")
                 .Append("district:'").Append(cc.district == null ? string.Empty : cc.district.ToString()).Append("',")
                 .Append("city:'").Append(cc.city == null ? string.Empty : cc.city.ToString()).Append("',")
                 .Append("district:'").Append(cc.district == null ? string.Empty : cc.district.ToString()).Append("',")
                  .Append("fax:'").Append(cc.fax == null ? string.Empty : cc.fax.ToString()).Append("',")
                   .Append("remark:'").Append(cc.remarks == null ? string.Empty : cc.remarks.ToString()).Append("',")
                 .Append("mobile:'").Append(cc.mobile == null ? string.Empty : cc.mobile.ToString()).Append("',")
                .Append("contact_address:'").Append(cc.address == null ? string.Empty : cc.address.ToString()).Append("'}");

            return customerJson.ToString();
        }

        public static string getGroupJson(FPRole role, List<UserAC> users)
        {
            if (role == null)
                role = new FPRole();
            if (users == null)
                users = new List<UserAC>();

            string str = string.Empty;
            string ids = string.Empty;

            foreach (var u in users)
            {
                str = str + u.eng_name + ",";
                ids = ids + u.objectId + ",";
            }

            if (str != string.Empty)
                str = str.Substring(0, str.Length - 1);
            if (ids != string.Empty)
                ids = ids.Substring(0, ids.Length - 1);

            StringBuilder groupJson = new StringBuilder();

            groupJson.Append("{").Append("objectid:'").Append(role.objectId.ToString()).Append("',")
                .Append("name:'").Append(role.name == null ? string.Empty : role.name.ToString()).Append("',")
                .Append("user_ids:'").Append(ids.Replace("'", "\\\'")).Append("',")
                 .Append("user_names:'").Append(str.Replace("'", "\\\'")).Append("'}");

            return groupJson.ToString();
        }

        public static string getAdminJson(UserAC admin)
        {
            string name = string.Empty;
            if (admin.roles != null)
            {
                var roles = admin.roles;
                foreach (var r in roles)
                {
                    name = name + r.name + ",";
                }
                if (roles.Count > 0)
                    name = name.Substring(0, name.Length - 1);
            }

            StringBuilder adminJson = new StringBuilder();
            adminJson.Append("{").Append("objectid:'").Append(admin.objectId).Append("',")
                .Append("user_name:'").Append(admin.user_name).Append("',")
                .Append("eng_name:'").Append(admin.eng_name).Append("',")
                 .Append("chi_name:'").Append(admin.chi_name).Append("',")
                  .Append("pwd:'").Append(admin.user_password).Append("',")
                .Append("post:'").Append(admin.post).Append("',")
                .Append("email:'").Append(admin.email).Append("',")
                .Append("group:'").Append(name).Append("',")
                .Append("remark:'").Append(admin.remark).Append("',")
                .Append("status:'").Append(admin.status).Append("'}");
            return adminJson.ToString();

        }
        public static String getJobOfOrderJson(PrintOrder order, PrintItem job)
        {
            StringBuilder jobJson = new StringBuilder();
            jobJson.Append("{").Append("pid:'").Append(job.pid).Append("',")
                .Append("received_date:'").Append(order.received_date == null ? "" : order.received_date.Value.ToString("yyyy-MM-dd")).Append("',")
                .Append("received_by:'").Append(order.received_by == null ? "" : order.received_by.eng_name).Append("',")
                .Append("order_deadline:'").Append(order.order_deadline).Append("',")
                .Append("customer_name:'").Append(filter(order.customer_contact.customer.company_name)).Append("',")
                .Append("customer_tel:'").Append(order.customer_contact.tel).Append("',")

                .Append("customer_contact_person:'").Append(order.customer_contact.contact_person).Append("',")
                .Append("remarks:'").Append(filter(order.remarks)).Append("',")
                .Append("section:'").Append(job.job_type == null ? "" : job.job_type.category_name).Append("',")
                .Append("jobid:'").Append(job.jobid).Append("',")
                .Append("filename:'").Append(job.file_name).Append("',")
                .Append("request:'");

            bool addRequest = false;
            if (job.newjob)
            {
                addRequest = true;
                jobJson.Append("New Job");
            }
            if (job.em)
            {
                if (addRequest)
                    jobJson.Append(",EM");
                else
                    jobJson.Append("EM");

                addRequest = true;
            }
            if (job.ftp)
            {
                if (addRequest)
                    jobJson.Append(",FTP");
                else
                    jobJson.Append("FTP");

                addRequest = true;
            }
            if (job.cddvd)
            {
                if (addRequest)
                    jobJson.Append(",CD/DVD");
                else
                    jobJson.Append("CD/DVD");

                addRequest = true;
            }
            if (job.mac)
            {
                if (addRequest)
                    jobJson.Append(",MAC");
                else
                    jobJson.Append("MAC");

                addRequest = true;
            }
            if (job.pc)
            {
                if (addRequest)
                    jobJson.Append(",PC");
                else
                    jobJson.Append("PC");

                addRequest = true;
            }
            if (job.test_job)
            {
                if (addRequest)
                    jobJson.Append(",Test");
                else
                    jobJson.Append("Test");

                addRequest = true;
            }
            jobJson.Append("',item_details:'");
            String itemType = "";
            for (int i = 0; i < job.print_job_items.Count; i++)
            {
                if (itemType != job.print_job_items[i].category_name)
                {
                    jobJson.Append("\\n").Append(job.print_job_items[i].category_name).Append(" : ");
                    itemType = job.print_job_items[i].category_name;
                }
                jobJson.Append(job.print_job_items[i].code_desc).Append(" ");
            }
            if((job.qty + "").Trim() != "")
                jobJson.Append("\\n").Append("Quantity : ").Append((job.qty + "").Replace("Q:", ""));
            if ((job.size + "").Trim() != "")
                jobJson.Append("\\n").Append("Size : ").Append((job.size + "").Replace("Size:", ""));
            if ((job.unit + "").Trim() != "")
                jobJson.Append("\\n").Append("Unit : ").Append((job.unit + "").Replace("Unit:", ""));


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
                .Append("mac:").Append(job.mac ? "true" : "false").Append(",")
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
                .Append("qty:'").Append((job.qty + "").Replace("Q:", "")).Append("',")
                .Append("size:'").Append((job.size + "").Replace("Size:", "")).Append("',")
                .Append("unit:'").Append((job.unit + "").Replace("Unit:", "")).Append("',");
                jobJson.Append("item_details:'");
                String itemType = "";
                for (int i = 0; i < job.print_job_items.Count; i++)
                {
                    if (itemType != job.print_job_items[i].category_name)
                    {
                        jobJson.Append("\\n").Append(job.print_job_items[i].category_name).Append(" : ");
                        itemType = job.print_job_items[i].category_name;
                    }
                    jobJson.Append(job.print_job_items[i].code_desc).Append(" ");
                }
                if((job.qty + "").Trim() != "")
                    jobJson.Append("\\n").Append("Quantity : ").Append((job.qty + "").Replace("Q:", ""));
                if ((job.size + "").Trim() != "")
                    jobJson.Append("\\n").Append("Size : ").Append((job.size + "").Replace("Size:", ""));
                if ((job.unit + "").Trim() != "")
                    jobJson.Append("\\n").Append("Unit : ").Append((job.unit + "").Replace("Unit:", ""));

                jobJson.Append("',job_status:'").Append(job.job_status).Append("'}");
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
                    orderJson.Append("customer_name:'").Append(filter(order.customer_contact.customer.company_name)).Append("',");
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
                .Append("customer_tel:'").Append((order.customer_contact == null ? "" : filter(order.customer_contact.tel))).Append("',");

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

            orderJson.Append("customer_contact_person:'").Append(order.customer_contact == null ? "" : filter(order.customer_contact.contact_person)).Append("'}");
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
            if (str == null)
                return str;

            return str.Replace("\\", "\\\\").Replace("\r", "\\r").Replace("\n", "\\n").Replace("'", "\\'").Replace("\"", "\\\"");
        }


        internal static string getDeliveryJson(Delivery delivery)
        {

            if (delivery == null)
                delivery = new Delivery();

            if (delivery.customer == null)
                delivery.customer = new Customer();

            if (delivery.handled_by == null)
                delivery.handled_by = new UserAC();
            if (delivery.requested_by == null)
                delivery.requested_by = new UserAC();



            StringBuilder deliveryJson = new StringBuilder();

            deliveryJson.Append("{").Append("objectid:'").Append(delivery.objectId.ToString()).Append("',")
                .Append("customerid:'").Append(delivery.customer.objectId.ToString ()).Append("',")
                 .Append("company_code:'").Append(delivery.customer.company_code == null ? string.Empty : delivery.customer.company_code.ToString().Replace("'", "\\\'")).Append("',")
                .Append("company_name:'").Append(delivery.customer.company_name == null ? string.Empty : filter(delivery.customer.company_name.ToString())).Append("',")
                 .Append("number:'").Append(delivery.number.ToString()).Append("',")
                 .Append("district:'").Append(delivery.contact.district == null ? string.Empty : delivery.contact.district.ToString()).Append("',")
                .Append("delivery_type:'").Append(delivery.delivery_type == null ? string.Empty : delivery.delivery_type.ToString()).Append("',")
                .Append("date:'").Append(delivery.deadline == null ? string.Empty : delivery.deadline.Value.ToShortDateString()).Append("',")
                .Append("time:'").Append(delivery.deadline == null ? string.Empty : delivery.deadline.Value.ToShortTimeString()).Append("',")
                .Append("handledby:'").Append(delivery.handled_by.eng_name == null ? string.Empty : delivery.handled_by.eng_name.ToString()).Append("',")
                .Append("partno:'").Append(delivery.part_no == null ? string.Empty : delivery.part_no.ToString()).Append("',")
                 .Append("nonorder:'").Append(delivery.non_order == null ? string.Empty : delivery.non_order.ToString()).Append("',")
                  .Append("length:'").Append(delivery.length == null ? string.Empty : delivery.length.ToString()).Append("',")
                   .Append("width:'").Append(delivery.width == null ? string.Empty : delivery.width.ToString()).Append("',")
                   .Append("goods_type:'").Append(delivery.goods_type == null ? string.Empty : delivery.goods_type.ToString()).Append("',")
                    .Append("height:'").Append(delivery.height == null ? string.Empty : delivery.height.ToString()).Append("',")
                     .Append("weight:'").Append(delivery.weight == null ? string.Empty : delivery.weight.ToString()).Append("',")
                     .Append("street1:'").Append(delivery.contact.street1 == null ? string.Empty : delivery.contact.street1.ToString().Replace("'", "\\\'").Replace ("\n","").Replace ("\r","")).Append("',")
                       .Append("street2:'").Append(delivery.contact.street2 == null ? string.Empty : delivery.contact.street2.ToString().Replace("'", "\\\'").Replace("\n", "").Replace("\r", "")).Append("',")
                         .Append("street3:'").Append(delivery.contact.street3 == null ? string.Empty : delivery.contact.street3.ToString().Replace("'", "\\\'").Replace("\n", "").Replace("\r", "")).Append("',")
                  .Append("city:'").Append(delivery.contact.city == null ? string.Empty : delivery.contact.city.ToString()).Append("',")
                    .Append("tel:'").Append(delivery.contact.tel == null ? string.Empty : delivery.contact.tel.ToString()).Append("',")
                  .Append("mobile:'").Append(delivery.contact.mobile == null ? string.Empty : delivery.contact.mobile.ToString()).Append("',")
                  .Append("contact:'").Append(delivery.contact.contact_person == null ? string.Empty : delivery.contact.contact_person.ToString()).Append("',")
                 .Append("remark:'").Append(filter(delivery.remarks == null ? string.Empty : delivery.remarks.ToString().Replace ("'","\\\\'").Replace ("\n","\\n").Replace ("\r","\\r"))).Append("',")
                 .Append("requestby:'").Append(delivery.requested_by == null ? string.Empty : delivery.requested_by.objectId.ToString()).Append("',")
                  .Append("handledbyid:'").Append(delivery.handled_by == null ? string.Empty : delivery.handled_by.objectId.ToString()).Append("',")
                   .Append("deadline:'").Append(delivery.deadline == null ? string.Empty : delivery.deadline.Value.ToString("yyyy-MM-dd")).Append("',")
                    .Append("notes:'").Append(delivery.notes == null ? string.Empty : delivery.notes.ToString().Replace("'", "\\\\'").Replace("\n", "\\n").Replace("\r", "\\r")).Append("',")
            .Append("status:'").Append(delivery.status == null ? string.Empty : delivery.status.ToString()).Append("'}");

            return deliveryJson.ToString();
        }


        internal static string getConsumptionJson(Consumption consumption)
        {
            StringBuilder consumptionJson = new StringBuilder();

            consumptionJson.Append("{").Append("conid:'").Append(consumption.objectId.ToString()).Append("',")
               .Append("total:'").Append(consumption.total.ToString ()).Append("',")
               .Append("store:'").Append(consumption.store.ToString ()).Append("',")
                .Append("totalunit:'").Append(consumption.totalunit.ToString ()).Append("',")
                 .Append("storeunit:'").Append(consumption.storeunit.ToString ()).Append("',")
                  .Append("usedunit:'").Append(consumption.usedunit.ToString ()).Append("',")
                   .Append("asdate:'").Append(consumption.asdate.Value.ToString ("MM/dd/yyyy")).Append("',")
               .Append("used:'").Append(consumption.used.ToString ()).Append("'}");

            return consumptionJson.ToString();
        }

        internal static string getInventoryJson(Inventory inventory)
        {
            StringBuilder deliveryJson = new StringBuilder();

            deliveryJson.Append("{").Append("objectid:'").Append(inventory.objectId.ToString()).Append("',")
                 .Append("category:'").Append(inventory.category == null ? string.Empty : inventory.category.Replace("'", "\\\'")).Append("',")
                  .Append("assetno:'").Append(inventory.assetno == null ? string.Empty : inventory.assetno.Replace("'", "\\\'")).Append("',")
                    .Append("productnameen:'").Append(inventory.productnameen == null ? string.Empty : inventory.productnameen.Replace("'", "\\\'")).Append("',")
                       .Append("productnamecn:'").Append(inventory.productnamecn == null ? string.Empty : inventory.productnamecn.Replace("'", "\\\'")).Append("',")
                      .Append("description:'").Append(inventory.description == null ? string.Empty : inventory.description.Replace("'", "\\\'").Replace ("\r","\\r").Replace ("\n","\\n")).Append("',")
                         .Append("quantity:'").Append(inventory.quantity == null ? string.Empty : inventory.quantity.Replace("'", "\\\'")).Append("',")
                            .Append("asat:'").Append(inventory.updateDate == null ? string.Empty : inventory.updateDate.Value.ToString("yyyy-MM-dd")).Append("',")
                             .Append("stored:'").Append(inventory.stored == null ? string.Empty : inventory.stored).Append("',")
  .Append("productno:'").Append(inventory.productno == null ? string.Empty : inventory.productno).Append("',")
   .Append("dimension:'").Append(inventory.dimension == null ? string.Empty : inventory.dimension).Append("',")
     .Append("unit:'").Append(inventory.unit == null ? string.Empty : inventory.unit).Append("',")
       .Append("unitcost:'").Append(inventory.unitcost == null ? string.Empty : inventory.unitcost).Append("',")
           .Append("receivedby:'").Append(inventory.receivedby == null ? string.Empty : inventory.receivedby.objectId.ToString()).Append("',")
            .Append("person:'").Append(inventory.contactperson == null ? string.Empty : inventory.contactperson.ToString()).Append("',")
             .Append("tel:'").Append(inventory.Tel == null ? string.Empty : inventory.Tel.ToString()).Append("',")
             .Append("deadline:'").Append(inventory.orderdeadline == null ? string.Empty : inventory.orderdeadline.Value.ToString("yyyy-MM-dd")).Append("',")
                          .Append("receiveddate:'").Append(inventory.receiveddate == null ? string.Empty : inventory.receiveddate.Value.ToString("yyyy-MM-dd")).Append("',")

            .Append("remark:'").Append(inventory.remark == null ? string.Empty : inventory.remark.ToString()).Append("'}");

            return deliveryJson.ToString();
        }
    }
}
