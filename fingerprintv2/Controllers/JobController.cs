using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fingerprintv2.Web;
using System.Text;
using fpcore.Model;
using fingerprintv2.Services;
using System.Text.RegularExpressions;

namespace fingerprintv2.Controllers
{
    public class JobController : Controller
    {
        //
        // GET: /Job/
        [AuthenticationFilterAttr]
        public ActionResult getJob()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String start = Request.Params["start"];
            String limit = Request.Params["limit"];
            String sort = Request.Params["sort"];
            String sortDir = Request.Params["dir"];

            String jt = Request.Params["jt"];
            String js = Request.Params["js"];
            String ft = Request.Params["ft"];
            String fv = Request.Params["fv"];
            



            String query = " where IsDeleted = 0 ";
            if (jt != "" && jt != null)
                query = query + " and job_type='" + jt + "' ";
            if (js != "" && js != null)
                query = query + " and job_status = '" + js + "' ";

            if (fv != null && (fv + "").Trim() != "")
            {
                if (ft == "customer_number")
                    query = query + " and Print_Item.ObjectId in (select pi1.ObjectId from Print_Item pi1 inner join Print_Order po1 on pi1.pid = po1.pid inner join customer_contact cc on po1.contact_id = cc.ObjectId where cc.cid = '"+ fv +"') ";
                if (ft == "customer_name")
                    query = query + " and Print_Item.ObjectId in (select pi1.ObjectId from Print_Item pi1 inner join Print_Order po1 on pi1.pid = po1.pid inner join customer_contact cc on po1.contact_id = cc.ObjectId inner join customer c on cc.cid = c.company_code where c.company_name like '%" + fv + "%') ";
                if (ft == "invoice_no")
                    query = query + " and Print_Item.ObjectId in (select pi1.ObjectId from Print_Item pi1 inner join Print_Order po1 on pi1.pid = po1.pid where po1.invoice_no ='" + fv + "') ";
                if (ft == "order_no")
                    query = query + " and pid = '" + fv + "'";
                if (ft == "sales")
                    query = query + " and Print_Item.pid in (select po1.pid from Print_Order po1 inner join UserAC ac on po1.received_by = ac.ObjectId and ac.eng_name like '%" + fv + "%')";
            }



            int iStart = int.Parse(start == null ? "0" : start);
            int iLimit = int.Parse(limit == null ? "20" : limit);

            bool bSortDir = sortDir == "DESC";

            List<PrintItem> jobs = objectService.getAllJob( query,iLimit, iStart, sort, bSortDir, user);
            int count = objectService.countAllJob(query , user);

            if (jobs.Count == 0)
                return Content("{total:0,data:[]}");

            StringBuilder jobJson = new StringBuilder("{total:").Append(count).Append(",").Append("data:[");
            PrintOrder order = null;
            
            for (int i = 0; i < jobs.Count; i++)
            {
                if (i > 0)
                    jobJson.Append(",");
                order = objectService.getPrintOrderByID(jobs[i].pid, user);
                jobJson.Append(JSONTool.getJobJson(jobs[i], order));
            }
            jobJson.Append("]}");
            return Content(jobJson.ToString());
        }
        [AuthenticationFilterAttr]
        public ActionResult getJobByItem()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String jobid = Request.Params["jobid"];
            PrintItem job = objectService.getPrintJobByID(jobid, user);
            if (job == null)
                return Content("[]");

            List<AssetConsumption> jobDetails = job.print_job_detail;

            if(jobDetails == null || jobDetails.Count == 0)
                return Content("[]");

            StringBuilder jobListJson = new StringBuilder();
            jobListJson.Append("[");
            for (int i = 0; i < jobDetails.Count; i++)
            {
                if (i > 0)
                    jobListJson.Append(",");
                jobListJson.Append("[")
                    .Append("'").Append(jobDetails[i].objectId).Append("',")
                    .Append("'").Append(jobDetails[i].product == null ? "":jobDetails[i].product.productnameen).Append("',")
                    .Append("'").Append(jobDetails[i].purpose == 0 ? "Test" : jobDetails[i].purpose == 1 ? "Redo" : "Final" ).Append("',")
                    .Append("'").Append(jobDetails[i].qty).Append("',")
                    .Append("'").Append(jobDetails[i].size).Append("',")
                    .Append("'").Append(jobDetails[i].cost).Append("',")
                    .Append("'").Append(jobDetails[i].createDate == null ? "" : jobDetails[i].createDate.Value.ToString("yyyy-MM-dd hh:mm:ss")).Append("'")
                    .Append("]");
            }
            jobListJson.Append("]");

            return Content(jobListJson.ToString());
        }
        [AuthenticationFilterAttr]
        public ActionResult getItemsByOrder()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String pid = Request.Params["pid"];
            PrintOrder order = objectService.getPrintOrderByID(pid,user);
            
            List<PrintItem> jobs;
            if (order == null)
            {
                order  = (PrintOrder)Session["newOrder"];
                jobs = order.print_job_list;
            }
            else
            {
                jobs = objectService.getPrintJobByOrder(order, user);
            }

            

            if (jobs == null || jobs.Count == 0)
            {
                return Content("[]");
            }

            jobs = jobs.OrderBy(p => p.jobid).ToList();

            String sRequest = "";

            StringBuilder jobListJson = new StringBuilder();
            jobListJson.Append("[");
            for (int i = 0; i < jobs.Count; i++)
            {
                sRequest = "";

                if (jobs[i].newjob)
                    sRequest += "New Job,";
                if (jobs[i].em)
                    sRequest += "EM,";
                if (jobs[i].ftp)
                    sRequest += "FTP,";
                if (jobs[i].cddvd)
                    sRequest += "CD/DVD,";
                if (jobs[i].mac)
                    sRequest += "Mac,";
                if (jobs[i].pc)
                    sRequest += "Pc,";
                if (jobs[i].test_job)
                    sRequest += "Test";

                if (sRequest.EndsWith(","))
                    sRequest = sRequest.Substring(0, sRequest.Length - 1);

                if (i > 0)
                    jobListJson.Append(",");
                jobListJson.Append("[")
                    .Append("'").Append(jobs[i].jobid).Append("',")
                    .Append("'").Append(jobs[i].job_type == null ? "" : jobs[i].job_type.category_name).Append("',")
                    .Append("'").Append(jobs[i].file_name).Append("',")
                    .Append("'").Append(sRequest).Append("',");


                StringBuilder jobJson = new StringBuilder();
                String itemType = "";
                for (int j = 0; j < jobs[i].print_job_items.Count; j++)
                {
                    if (itemType != jobs[i].print_job_items[j].category_name)
                    {
                        jobJson.Append("<br/>").Append(jobs[i].print_job_items[j].category_name).Append(" : ");
                        itemType = jobs[i].print_job_items[j].category_name;
                    }
                    jobJson.Append(jobs[i].print_job_items[j].code_desc).Append(" ");
                }

                if ((jobs[i].qty + "").Trim() != "")
                {
                    String[] qtys = jobs[i].qty.Split('　');
                    String[] sizes = jobs[i].size.Split('　');
                    String[] units = jobs[i].unit.Split('　');

                    if (qtys.Length == sizes.Length && qtys.Length == units.Length)
                    {
                        for (int j = 0; j < qtys.Length; j++)
                        {
                            jobJson.Append("<br/>").Append("Quantity:").Append((qtys[j] + "").Replace("Quantity:", "")).Append(" Size:").Append(sizes[j].Replace("Size:", ""));
                            if (units[j].Replace("Unit:", "") != "")
                                jobJson.Append(" Unit:").Append(units[j].Replace("Unit:", ""));
                        }
                    }
                }

                //if ((jobs[i].qty + "").Trim() != "")
                //    jobJson.Append("<br/>").Append("Quantity : ").Append((jobs[i].qty + "").Replace("Q:", ""));
                //if ((jobs[i].size + "").Trim() != "")
                //    jobJson.Append("<br/>").Append("Size : ").Append((jobs[i].size + "").Replace("Size:", ""));
                //if ((jobs[i].unit + "").Trim() != "")
                //    jobJson.Append("<br/>").Append("Unit : ").Append((jobs[i].unit + "").Replace("Unit:", ""));


                jobListJson.Append("'").Append(jobJson.ToString()).Append("',")
                    .Append("'").Append(jobs[i].notes).Append("']");
            }
            jobListJson.Append("]");
            return Content(jobListJson.ToString());
        }
        [AuthenticationFilterAttr]
        public ActionResult addNewJob()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            try
            {
                PrintJobCategory job_type = objectService.getPrintJobCategoryByID(Request.Params["newjob-jobtype"], user);
                if (job_type == null)
                    return Content("{success:false, result:\"" + "Unknow job type\"}");

                String sCheckItemIds = "";
                for (int i = 0; i < Request.Params.Count; i++)
                {
                    if (Request.Params.Keys[i].StartsWith("item"))
                    {
                        if (Request.Params[Request.Params.Keys[i]] == "on")
                        {
                            if (sCheckItemIds != "")
                                sCheckItemIds += "/";
                            sCheckItemIds = sCheckItemIds + Request.Params.Keys[i].Replace("item", "");
                        }
                    }
                }

                PrintOrder order = (PrintOrder)Session["newOrder"];

                if (order.print_job_list == null)
                    order.print_job_list = new List<PrintItem>();

                PrintItem job = new PrintItem();
                job.cddvd = Request.Params["newjob-cddvd"] == "on";
                job.em = Request.Params["newjob-em"] == "on";

                job.file_name = Request.Params["newjob-filename"];

                job.ftp = Request.Params["newjob-ftp"] == "on";

                job.Fpaper = "" + Request.Params["Fpaper"];
                job.Fcolor = Request.Params["Fcolor"];
                job.Fdelivery_address = Request.Params["Fdelivery_address"];
                job.Fdelivery_date = Request.Params["Fdelivery_date"];
                job.Gcolor = "" + Request.Params["Gcolor"];
                job.Gpage = "" + Request.Params["Gpage"];

                job.handled_by = null;
                job.hold_job = false;
                job.job_deadline = ""; 
                job.job_status = PrintItem.STATUS_NEWG;
                job.job_type = job_type;
                job.jobid = order.print_job_list.Count + "";
                job.mac = Request.Params["newjob-mac"] == "on";
                job.newjob = Request.Params["newjob-newjob"] == "on";
                job.notes = Request.Params["newjob-notes"];
                job.pc = Request.Params["newjob-pc"] == "on";
                job.pid = ""; 
                job.item_detail = sCheckItemIds;
                job.print_job_detail = null; //to do
                job.register_date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                job.test_job = Request.Params["newjob-testjob"] == "on";
                //job.qty = Request.Params["newjob-quantity"];
                //job.size = Request.Params["newjob-size"];
                //job.unit = Request.Params["unit"];

                job.qty = "";
                job.size = "";
                job.unit = "";
                String[] qty_size = Regex.Split(("" + Request.Params["neworder-hidden-quantityvalue"]), "\r\n");
                for (int i = 0; i < qty_size.Length; i++)
                {
                    if (qty_size[i].Trim() != "")
                    {
                        if (i > 0)
                        {
                            job.qty += "　";
                            job.size += "　";
                            job.unit += "　";
                        }
                        job.qty += qty_size[i].Split('　')[0];
                        job.size += qty_size[i].Split('　')[1];
                        job.unit += qty_size[i].Split('　')[2];
                    }
                }

                if (order == null)
                {
                    return Content("{success:false, result:\"" + "Create job failed. order is not found." + "\"}");
                }

                String pid = Request.Params["newjob-pid"];
                PrintOrder oldOrder = objectService.getPrintOrderByID(pid, user);
                if (oldOrder != null)
                {
                    service.addNewJob(oldOrder, job, user);
                }
                else
                {
                    order.print_job_list.Add(job);
                }


            }
            catch(Exception e)
            {
                return Content("{success:false, result:\"" + "Create job failed. ex=" + e.Message + "\"}");
            }

            return Content("{success:true, result:\"" + "Create job successfully." + "\"}");
        }

        [AuthenticationFilterAttr]
        public ActionResult saveJob()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            try
            {
                
                String jobid = Request.Params["newjob-jobid"];
                bool isNewOrder = false;
                PrintItem job = objectService.getPrintJobByID(jobid, user);
                if (job == null)
                {
                    PrintOrder order = (PrintOrder)Session["newOrder"];
                    isNewOrder = true;
                    if (order.print_job_list == null)
                        order.print_job_list = new List<PrintItem>();

                    for (int i = 0; i < order.print_job_list.Count; i++)
                    {
                        if (order.print_job_list[i].jobid == jobid)
                        {
                            job = order.print_job_list[i];
                            break;
                        }
                    }
                }
                else
                {
                    isNewOrder = false;
                }

                PrintJobCategory job_type = objectService.getPrintJobCategoryByID(Request.Params["newjob-jobtype"], user);
                if (job_type == null)
                    return Content("{success:false, result:\"" + "Unknow job type\"}");

                String sCheckItemIds = "";
                for (int i = 0; i < Request.Params.Count; i++)
                {
                    if (Request.Params.Keys[i].StartsWith("item"))
                    {
                        if (Request.Params[Request.Params.Keys[i]] == "on")
                        {
                            if (sCheckItemIds != "")
                                sCheckItemIds += "/";
                            sCheckItemIds = sCheckItemIds + Request.Params.Keys[i].Replace("item", "");
                        }
                    }
                }

                
                job.cddvd = Request.Params["newjob-cddvd"] == "on";
                job.em = Request.Params["newjob-em"] == "on";
                
                job.file_name = Request.Params["newjob-filename"];
                job.Fpaper = "" + Request.Params["Fpaper"];
                job.Fcolor = Request.Params["Fcolor"];
                job.Fdelivery_address = Request.Params["Fdelivery_address"];
                job.Fdelivery_date = Request.Params["Fdelivery_date"];
                job.Gcolor = "" + Request.Params["Gcolor"];
                job.Gpage = "" + Request.Params["Gpage"];
                job.ftp = Request.Params["newjob-ftp"] == "on";
                
                job.job_type = job_type;
                job.mac = Request.Params["newjob-mac"] == "on";
                job.newjob = Request.Params["newjob-newjob"] == "on";
                job.notes = Request.Params["newjob-notes"];
                job.pc = Request.Params["newjob-pc"] == "on";
                job.item_detail = sCheckItemIds;
                job.test_job = Request.Params["newjob-testjob"] == "on";

                job.qty = "";
                job.size = "";
                job.unit = "";
                String[] qty_size = Regex.Split(("" + Request.Params["neworder-hidden-quantityvalue"]), "\r\n");
                for (int i = 0; i < qty_size.Length; i++)
                {
                    if (qty_size[i].Trim() != "")
                    {
                        if (i > 0)
                        {
                            job.qty += "　";
                            job.size += "　";
                            job.unit += "　";
                        }
                        job.qty += qty_size[i].Split('　')[0];
                        job.size += qty_size[i].Split('　')[1];
                        job.unit += qty_size[i].Split('　')[2];
                    }
                }
                //job.qty = Request.Params["newjob-quantity"];
                //job.size = Request.Params["newjob-size"];
                //job.unit = Request.Params["unit"];


                if (!isNewOrder)
                {
                    service.updateJob(job, user);
                }
            }
            catch (Exception e)
            {
                return Content("{success:false, result:\"" + "update job failed. ex=" + e.Message + "\"}");
            }

            return Content("{success:true, result:\"" + "update job successfully." + "\"}");
        }
        [AuthenticationFilterAttr]
        public ActionResult getNewJobs()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            PrintOrder order = (PrintOrder)Session["newOrder"];
            if (order == null)
            {
                return Content("[]");
            }

            if (order.print_job_list == null || order.print_job_list.Count == 0)
            {
                return Content("[]");
            }

            String sRequest = "";

            StringBuilder jobListJson = new StringBuilder();
            jobListJson.Append("[");
            for (int i = 0; i < order.print_job_list.Count; i++)
            {
                sRequest = "";

                if(order.print_job_list[i].newjob)
                    sRequest += "New Job,";
                if(order.print_job_list[i].em)
                    sRequest += "EM,";
                if(order.print_job_list[i].ftp)
                    sRequest += "FTP,";
                if(order.print_job_list[i].cddvd)
                    sRequest += "CD/DVD,";
                if(order.print_job_list[i].mac)
                    sRequest += "Mac,";
                if(order.print_job_list[i].pc)
                    sRequest += "Pc,";
                if(order.print_job_list[i].test_job)
                    sRequest += "Test";

                if(sRequest.EndsWith(","))
                    sRequest = sRequest.Substring(0,sRequest.Length - 1);

                if(i > 0)
                    jobListJson.Append(",");
                jobListJson.Append("[")
                    .Append("'").Append(i).Append("',")
                    .Append("'").Append(order.print_job_list[i].job_type == null ? "" : order.print_job_list[i].job_type.category_name).Append("',")
                    .Append("'").Append(order.print_job_list[i].file_name).Append("',")
                    .Append("'").Append(sRequest).Append("',")
                    .Append("'").Append(""/*To do*/).Append("',")
                    .Append("'").Append(order.print_job_list[i].notes).Append("']");
            }
            jobListJson.Append("]");
            return Content(jobListJson.ToString());
        }
        [AuthenticationFilterAttr]
        public ActionResult deleteNewJob()
        {
            String sJobId = Request.Params["deleteJobId"];

            try
            {
                PrintOrder order = (PrintOrder)Session["newOrder"];
                PrintItem job = null;

                for (int i = 0; i < order.print_job_list.Count; i++)
                {
                    job = order.print_job_list[i];

                    if (job.jobid == sJobId)
                    {
                        order.print_job_list.Remove(job);
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                return Content("{success:false, result:\"" + "Delete job failed. ex=" + e.Message + "\"}");
            }

            return Content("{success:true, result:\"" + "Delete job successfully." + "\"}");
        }
        [AuthenticationFilterAttr]
        public ActionResult getJobById()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String jobid = Request.Params["jobid"];
            PrintItem job = objectService.getPrintJobByID(jobid, user);

            if (job == null)
            {
                return Content("{}");
            }

            PrintOrder order = objectService.getPrintOrderByID(job.pid, user);

            if(order == null)
                return Content("{}");


            return Content(JSONTool.getJobOfOrderJson(order,job));

        }
        [AuthenticationFilterAttr]
        public ActionResult getJobDetailByID()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String jobid = Request.Params["jobid"];
            String pid = Request.Params["pid"];

            PrintOrder order = objectService.getPrintOrderByID(pid,user);
            Boolean isNewOrder = true;
            if(order != null)
            {
                isNewOrder = false;
            }
            else
            {
                isNewOrder = true;
                order = (PrintOrder)Session["newOrder"];
            }

            if(order == null)
                return Content("[]");

            PrintItem job = null;

            if(isNewOrder)
            {
                try
                {
                    job = order.print_job_list[int.Parse(jobid)];

                    if (job.item_detail != null)
                    {
                        job.print_job_items = new List<PrintItemDetail>();

                        String[] itemKeys = job.item_detail.Split('/');
                        for (int j = 0; j < itemKeys.Length; j++)
                        {
                            PrintItemDetail item = objectService.getPrintJobLookupByID(itemKeys[j], user);
                            if (item != null)
                                job.print_job_items.Add(item);
                        }
                    }
                    else
                    {
                        job.print_job_items = new List<PrintItemDetail>();
                    }

                }
                catch(Exception e)
                {
                    return Content("[]");
                }
            }
            else
            {
                job = objectService.getPrintJobByID(jobid, user);
            }

            if (job == null)
            {
                return Content("[]");
            }

            return Content("[" + JSONTool.getJobDetailJson(job) + "]");
        }

        [AuthenticationFilterAttr]
        public ActionResult getJobHandlerComboList()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            try
            {
                List<UserAC> users = objectService.getJobHandlers(user);

                if (users.Count == 0)
                    return Content("{tags:[{id:'0',name:' '}]}");

                StringBuilder usersJson = new StringBuilder("{tags:[");
                for (int i = 0; i < users.Count; i++)
                {
                    if (i > 0)
                        usersJson.Append(",");
                    usersJson.Append("{id:'").Append(users[i].objectId).Append("',name:'").Append(users[i].eng_name).Append("'}");
                }
                usersJson.Append("]}");

                return Content(usersJson.ToString());
            }
            catch (Exception e)
            {
                return Content("[[]]");
            }

            
        }
        [AuthenticationFilterAttr]
        public ActionResult deleteJob()
        {
            String sJobid = Request.Params["jobid"];
            String sPid = Request.Params["pid"];
            String pwd = Request.Params["pwd"];
            

            UserAC user = (UserAC)Session["user"];

            if (user.roles.Where(c => c.name == "system admin" || c.name == "job admin").Count() <= 0)
                return Content("{success:false, result:\"Sorry, You are not authorized to do this action.\"}");

            try
            {

                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

                PrintOrder order = objectService.getPrintOrderByID(sPid, user);


                if (order != null)
                {
                    PrintItem job = objectService.getPrintJobByID(sJobid, user);

                    if (job == null)
                    {
                        return Content("{success:false, result:\"Job is not found.\"}");
                    }
                    service.deleteJob(job, user);
                }
                else
                {
                    order = (PrintOrder)Session["newOrder"];
                    for (int i = 0; i < order.print_job_list.Count; i++)
                    {
                        if (order.print_job_list[i].jobid == sJobid)
                        {
                            order.print_job_list.RemoveAt(i);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                return Content("{success:false, result:\"" + "Delete job failed. ex=" + e.Message + "\"}");
            }

            return Content("{success:true, result:\"Update success\"}");
        }

        [AuthenticationFilterAttr]
        public ActionResult getAllInventory()
        {
            try
            {
                UserAC user = (UserAC)Session["user"];
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
                List<Inventory> inventories = objectService.getInventories("" , 1000, 0, "", false, user);
                StringBuilder inventoryJson = new StringBuilder();
                inventoryJson.Append("[");
                for (int i = 0; i < inventories.Count; i++)
                {
                    if (i > 0)
                        inventoryJson.Append(",");
                    inventoryJson.Append("['").Append(inventories[i].productnameen).Append("','").Append(inventories[i].objectId).Append("','").Append(inventories[i].unit).Append("']");
                }
                inventoryJson.Append("]");
                return Content(inventoryJson.ToString());
            }
            catch (Exception e)
            {

            }
            return Content("[]");
            
        }
        [AuthenticationFilterAttr]
        public ActionResult addJobDetail()
        {
            String jobid = Request.Params["jobid"];
            String pid = Request.Params["pid"];
            String detailx = Request.Params["new-detail-x"];
            String detaily = Request.Params["new-detail-y"];
            String unit = Request.Params["new-detail-unit"];
            String purpose = Request.Params["newjob-purpose"];
            String detailz = Request.Params["new-detail-z"];
            String sProduct = Request.Params["product"];

            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            UserAC user = (UserAC)Session["user"];
            PrintItem job = objectService.getPrintJobByID(jobid,user);

            if (job == null)
            {
                return Content("{success:false, result:\"Item is not found.\"}");
            }

            if(detailx == "" && detaily == "" && unit == "")
                return Content("{success:false, result:\"Please enter the job details.\"}");

            AssetConsumption detail = new AssetConsumption();
            try
            {
                detail.jobid = jobid;
                detail.purpose = int.Parse(purpose);
                detail.qty = detailz;
                detail.size = detailx + "x" + detaily;
                detail.unit = unit;

                if (unit == "MM")
                    detail.qty = "";
                else
                    detail.size = "";

                int iProdcut = int.Parse(sProduct);
                Inventory product = objectService.getInventoryById(iProdcut, user);
                detail.product = product;
                
                detail.createDate = DateTime.Now;
                detail.cost = "";//TODO

                bool success = service.addNewJobDetail(detail, user);

                if(success)
                    return Content("{success:true, result:\"Update success\"}"); 
                else
                    return Content("{success:false, result:\"Update Failed\"}"); 
            }
            catch (Exception e)
            {
                return Content("{success:false, result:\"" + "Add job failed. ex=" + e.Message + "\"}");
            }

            
        }
        [AuthenticationFilterAttr]
        public ActionResult deleteJobDetail()
        {
            String jobid = Request.Params["jobid"];
            String createDate = Request.Params["createDate"];
            String objecgtId = Request.Params["objectId"];

            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            UserAC user = (UserAC)Session["user"];
            PrintItem job = objectService.getPrintJobByID(jobid, user);

            if (job == null)
            {
                return Content("{success:false, result:\"Item is not found.\"}");
            }


            AssetConsumption detail = objectService.getPrintJobDetailByID(objecgtId, user);

            if (detail == null)
            {
                return Content("{success:false, result:\"Job is not found.\"}");
            }

            try
            {

                bool success = service.deleteJobDetail(detail, user);

                if (success)
                    return Content("{success:true, result:\"Update success\"}");
                else
                    return Content("{success:false, result:\"Update Failed\"}");
            }
            catch (Exception e)
            {
                return Content("{success:false, result:\"" + "Add job failed. ex=" + e.Message + "\"}");
            }
        }
        [AuthenticationFilterAttr]
        public ActionResult updateItem()
        {
            String jobid = Request.Params["jobid"];
            String handledBy = Request.Params["handledBy"];
            String status = Request.Params["status"];

            try
            {
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

                UserAC user = (UserAC)Session["user"];

                PrintItem job = objectService.getPrintJobByID(jobid, user);
                if(job == null)
                    return Content("{success:false, result:\"" + "Update failed, Item is not found " + "\"}");

                UserAC handledby = null;

                try
                {
                    handledby = objectService.getUserByID(int.Parse(handledBy), user);
                }
                catch (Exception e) { }

                if(handledby == null)
                    return Content("{success:false, result:\"" + "Update failed, Please select a user to handle" + "\"}");

                job.job_status = status;
                job.handled_by = handledby;

                bool success = service.updateJob(job, user);

                if(success)
                    return Content("{success:true, result:\"Update success\"}");
                else
                    return Content("{success:false, result:\"Update Failed\"}");
            }
            catch (Exception e)
            {
                return Content("{success:false, result:\"" + "Update failed. ex=" + e.Message + "\"}");
            }
        }

        [AuthenticationFilterAttr]
        public ActionResult cloneJob()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");


            String jobid = Request.Params["jobid"];
            PrintItem job = objectService.getPrintJobByID(jobid, user);
            if (job == null)
            {
                PrintOrder order = (PrintOrder)Session["newOrder"];
                if (order.print_job_list == null)
                    order.print_job_list = new List<PrintItem>();

                for (int i = 0; i < order.print_job_list.Count; i++)
                {
                    if (order.print_job_list[i].jobid == jobid)
                    {
                        job = order.print_job_list[i];
                        break;
                    }
                }
            }

            if (job != null)
            {
                PrintItem cloneJob = new PrintItem();
                cloneJob.cddvd = job.cddvd;
                cloneJob.createDate = job.createDate;
                cloneJob.em = job.em;
                cloneJob.Fcolor = job.Fcolor;
                cloneJob.Fdelivery_address = job.Fdelivery_address;
                cloneJob.Fdelivery_date = job.Fdelivery_date;
                cloneJob.file_name = job.file_name;
                cloneJob.Fpaper = job.Fpaper;
                cloneJob.ftp = job.ftp;
                cloneJob.Gcolor = job.Gcolor;
                cloneJob.Gpage = job.Gpage;
                cloneJob.handled_by = job.handled_by;
                cloneJob.hold_job = job.hold_job;
                cloneJob.isDeleted = job.isDeleted;
                cloneJob.item_detail = job.item_detail;
                cloneJob.job_deadline = job.job_deadline;
                cloneJob.job_status = job.job_status;
                cloneJob.job_type = job.job_type;
                cloneJob.jobid = "";
                cloneJob.mac = job.mac;
                cloneJob.newjob = job.newjob;
                cloneJob.notes = job.notes;
                cloneJob.objectId = -1;
                cloneJob.pc = job.pc;
                cloneJob.pid = job.pid;
                cloneJob.print_job_detail = job.print_job_detail;
                cloneJob.print_job_items = job.print_job_items;
                cloneJob.qty = job.qty;
                cloneJob.register_date = job.register_date;
                cloneJob.size = job.size;
                cloneJob.test_job = job.test_job;
                cloneJob.unit = job.unit;
                cloneJob.updateBy = job.updateBy;
                cloneJob.updateDate = job.updateDate;

                String pid = Request.Params["pid"];
                PrintOrder oldOrder = objectService.getPrintOrderByID(pid, user);
                if (oldOrder != null)
                {
                    service.addNewJob(oldOrder, cloneJob, user);
                }
                else
                {
                    PrintOrder order = (PrintOrder)Session["newOrder"];

                    cloneJob.jobid = order.print_job_list.Count + "";
                    order.print_job_list.Add(cloneJob);
                }

                return Content("{success:true, result:\"" + cloneJob.jobid + "\"}");
            }
            else
            {
                return Content("{success:false, result:\"Clone item failed\"}");
            }
        }
    }
}
