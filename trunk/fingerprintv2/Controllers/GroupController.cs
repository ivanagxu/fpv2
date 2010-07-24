using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fpcore.Model;
using fingerprintv2.Web;
using System.Text;
using fingerprintv2.Services;

namespace fingerprintv2.Controllers
{
    public class GroupController : Controller
    {
        //
        // GET: /Group/

        public ActionResult group()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            String start = Request.Params["start"];
            String limit = Request.Params["limit"];
            String sort = Request.Params["sort"];
            String sortDir = Request.Params["dir"];

            int iStart = int.Parse(start);
            int iLimit = int.Parse(limit);
            bool bSortDir = sortDir == "DESC";


            string query = null;
            if (sort != null && sort != "group")
                query = " order by " + sort + " " + sortDir;

            List<FPRole> roles = objectService.getRoles(query, user);
            int count = roles.Count();

            if (roles.Count() == 0)
                return Content("{total:0,data:[]}");

            StringBuilder groupJson = new StringBuilder("{total:").Append(count).Append(",").Append("data:[");
            for (int i = 0; i < roles.Count; i++)
            {
                List<UserAC> users = objectService.getUsersByRole(roles[i].objectId.ToString(), user);
                if (i > 0)
                    groupJson.Append(",");
                groupJson.Append(JSONTool.getGroupJson(roles[i], users));
            }
            groupJson.Append("]}");

            return Content(groupJson.ToString());
        }

        [AuthenticationFilterAttr]
        public ActionResult DeleteGroup()
        {
            try
            {
                String objectid = Request.Params["pid"];
                String pwd = Request.Params["pwd"];

                UserAC user = (UserAC)Session["user"];


                if (pwd != user.user_password)
                    return Content("{success:false, result:\"Incorrect password, delete failed.\"}");

                if (user.roles.Where(c => c.name == "system admin").Count() <= 0)
                    return Content("{success:false, result:\"Sorry, You are not authorized to do this action.\"}");

                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

                int id = 0;
                int.TryParse(objectid, out id);
                FPRole role = objectService.getRoles(" where FPObject.ObjectId='" + id + "'", user).FirstOrDefault();

                if (role == null)
                {
                    return Content("{success:false, result:\"Group is not found.\"}");
                }
                else
                {
                    service.deleteRole(role, user);
                    List<UserAC> users = objectService.getUsersByRole(objectid, user);
                    if (users != null)
                    {
                        foreach (var u in users)
                        {
                            if (u.roles != null && u.roles.Exists(c => c.objectId == role.objectId))
                            {
                                u.roles.RemoveAll(c => c.objectId == role.objectId);
                                service.updateUserRole(u);
                            }
                        }
                    }
                }

                return Content("{success:true, result:\"Update success\"}");
            }
            catch (Exception ex)
            {
                return Content("{success:false,result:\"" + ex.Message + "\"}");
            }
        }

        public object getContactSales(string objectID)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            List<UserAC> users = objectService.getUsersByRole(objectID, user);
            if (users == null)
                users = new List<UserAC>();
            StringBuilder usersJson = new StringBuilder("{").Append("data:[");
            for (int i = 0; i < users.Count(); i++)
            {
                if (i > 0)
                    usersJson.Append(",");
                StringBuilder userJson = new StringBuilder();
                userJson.Append("{").Append("objectid:'").Append(users[i].objectId).Append("',")
                 .Append("name:'").Append(users[i].eng_name == null ? "" : users[i].eng_name.ToString()).Append("'}");
                usersJson.Append(userJson.ToString());
            }
            usersJson.Append("]}");
            return Content(usersJson.ToString());
        }


        public object getLeftSales(string objectID)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            List<UserAC> users = objectService.getUserNotInRole(objectID, user);
            if (users == null)
                users = new List<UserAC>();
            StringBuilder usersJson = new StringBuilder("{").Append("data:[");
            for (int i = 0; i < users.Count(); i++)
            {
                if (i > 0)
                    usersJson.Append(",");
                StringBuilder userJson = new StringBuilder();
                userJson.Append("{").Append("objectid:'").Append(users[i].objectId).Append("',")
                 .Append("name:'").Append(users[i].eng_name == null ? "" : users[i].eng_name.ToString()).Append("'}");
                usersJson.Append(userJson.ToString());
            }
            usersJson.Append("]}");
            return Content(usersJson.ToString());
        }


        [AuthenticationFilterAttr]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public object AddGroup(string roleID, string name, string itemselector)
        {
            try
            {
                UserAC user = (UserAC)Session["user"];
                IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
                IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
                int rid = 0;
                int.TryParse(roleID, out rid);
                FPRole role = objectService.getRoles(" and FPObject.ObjectId='" + rid + "'", user).FirstOrDefault();

                string result = string.Empty;
                bool bresult = false;
                if (role == null)//新增
                {
                    role = new FPRole();
                    role.name = name;
                    service.addRole(role, user);
                    result = "your information added sucessfully!";
                    bresult = true;
                }
                else//修改
                {
                    role.name = name;
                    service.updateRole(role, user);
                    result = "your information updated sucessfully!";
                    bresult = true;
                }

                List<UserAC> users = objectService.getUsersByRole(roleID, user);
                if (users != null)
                {
                    foreach (var u in users)
                    {
                        if (u.roles != null && u.roles.Exists(c => c.objectId == role.objectId))
                        {
                            u.roles.RemoveAll(c => c.objectId == role.objectId);
                            service.updateUserRole(u);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(itemselector))
                {
                    var array = itemselector.Split(',');
                    foreach (var item in array)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            UserAC u = objectService.getUserByID(int.Parse(item), user);
                            if (u.roles.Count() == 0 || (u.roles != null && u.roles.Exists(c => c.objectId == role.objectId) == false))
                            {
                                u.roles.Add(role);
                                service.updateUserRole(u);
                            }
                        }
                    }
                }

                return Content("{success:" + bresult.ToString().ToLower() + ", result:\"" + result + "\"}");
            }
            catch (Exception ex)
            {
                return Content("{success:false, result:\"" + ex.Message + "\"}");
            }
        }

    }
}
