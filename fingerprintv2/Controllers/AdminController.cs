using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using fingerprintv2.Web;
using fpcore.Model;
using fingerprintv2.Services;

namespace fingerprintv2.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }
        [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult admin(string query,bool? direction)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            if (direction == null)
                direction = true;
            direction = !direction;
            if (string.IsNullOrEmpty(query))
                query = "UserAC.ObjectId";
            string sort = direction == true ? " ASC " : " desc ";

            List<UserAC> users = objectService.getSales(" order by " + query + sort, user);
            ViewData.Add("direction", direction);
            ViewData.Add("users", users);
            return PartialView();
        }

        [AuthenticationFilterAttr]
        [ValidateInput (false )]
        [AcceptVerbs(HttpVerbs.Post)]
        public object AddAdmin(string id,string username, string nameen, string namecn, string post, string email, string pwd, string remark, string status)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            List<UserAC> users = objectService.getSales(null, user);
            var str = string.Empty;

            UserAC opuser = objectService.getUserByID(int.Parse(id), user);

            if (opuser == null)
            {
                if (!users.Exists(u => u.user_name == username.Trim()))
                {
                    bool result = service.addNewUserAC(new UserAC
                    {
                        user_name = username,
                        user_password = pwd,
                        chi_name = namecn,
                        eng_name = nameen,
                        remark = remark,
                        status = status,
                        email = email,
                        post = post,

                    }, user);
                    str = "your information added successfully!";
                }
                else
                {
                    str = "add information failed ,exist the same username!";
                }
            }
            else
            {
                if (!users.Exists(u => u.user_name == username.Trim() && u.objectId != int.Parse(id)))
                {
                    opuser.user_name = username;
                    opuser.user_password = pwd;
                    opuser.chi_name = namecn;
                    opuser.eng_name = nameen;
                    opuser.remark = remark;
                    opuser.status = status;
                    opuser.email = email;
                    opuser.post = post;

                    bool result = service.updateUserAC(opuser, user);
                    str = "your information updated successfully!";
                }
                else
                {
                    str = "update information failed,exist the same username!";
                }
            }
          
            return Json(str);
        }

        [AuthenticationFilterAttr]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public object DeleteAdmin(string ids)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            string str = string.Empty;
            if (!string.IsNullOrEmpty(ids))
            {
                var array = ids.Split(',');
                foreach (var item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int id = 0;
                        int.TryParse(item, out id);
                        var obj = objectService.getUserByID(id, user);
                        service.deleteUserAC(obj, user);                        
                    }
                }                
            }
            str = "delete success!"; 
            return Json(str);
        }

        [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult group()
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

            List<FPRole> roles = objectService.getRoles(null, user);
            List<UserAC> users = objectService.getSales(null, user);
            ViewData.Add("roles", roles);
            ViewData.Add("users", users);
            return PartialView();
        }

        [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult RenderGroup(string roleID)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");

           
            List<UserAC> users = objectService.getUsersByRole(roleID, user);
            FPRole role=objectService.getRoles (" where FPObject.ObjectId='"+roleID +"'",user).FirstOrDefault ();
            ViewData.Add("role", role);
            ViewData.Add("users", users);
            return PartialView();
        }

        [AuthenticationFilterAttr]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public object AddGroup(string roleID, string name, string userids)
        {
            UserAC user = (UserAC)Session["user"];
            IFPService service = (IFPService)FPServiceHolder.getInstance().getService("fpService");
            IFPObjectService objectService = (IFPObjectService)FPServiceHolder.getInstance().getService("fpObjectService");
            int rid = 0;
            int.TryParse(roleID, out rid);
            FPRole role = objectService.getRoles(" where FPObject.ObjectId='" + rid + "'", user).FirstOrDefault();
           
            if (role ==null)//ÐÂÔö
            {
                role = new FPRole();
                role.name = name;
                service.addRole(role, user);               
            }
            else//ÐÞ¸Ä
            {
                role.name = name;
                service.updateRole(role, user);               
            }

            List<UserAC> users = objectService.getUsersByRole(roleID, user);
            if (users != null)
            {
                foreach (var u in users)
                {
                    if (u.roles != null && u.roles.Exists (c=>c.objectId ==role.objectId))
                    {
                        u.roles.RemoveAll(c => c.objectId == role.objectId);
                        u.roles.Remove(role);
                        service.updateUserRole(u);
                    }
                }
            }

            if (!string.IsNullOrEmpty(userids))
            {
                var array = userids.Split(',');
                foreach (var item in array)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        UserAC u = objectService.getUserByID(int.Parse(item), user);
                        if (u.roles .Count()==0|| (u.roles != null && u.roles.Exists(c => c.objectId == role.objectId)==false))
                        {
                            u.roles.Add(role);
                            service.updateUserRole(u);
                        }
                    }
                }
            }

            return Json(true);
        }

        [AuthenticationFilterAttr]
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult customer()
        {
            return PartialView();
        }
    }
}
