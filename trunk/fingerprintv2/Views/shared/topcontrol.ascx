<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="topdiv">
  <%   var currentuser = Session["user"] as fpcore.Model.UserAC;
       string rolename = string.Empty;
       if (currentuser != null)
       {
           var roles = (Session["user"] as fpcore.Model.UserAC).roles;
          
           if (roles.Count() > 0)
           {
               rolename = "your roles :";
               foreach (var role in roles)
               {
                   rolename += role.name + ",";
               }
               rolename = rolename.Substring(0, rolename.Length - 1);
           }
       }
                       %>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="top_getting_opacity">
        <tr>
            <td width="1%" rowspan="2" class="top_style1">
            </td>
            <td width="60%" nowrap="nowrap" class="top_style1">
                &nbsp;&nbsp;<img border="0" src='<%=Html.link("Content/images/001_12.gif") %>' width="20"
                    height="20" align="middle">
                Fingerprint Order and Delivery System
                <img border="0" src='<%=Html.link("Content/images/001_12.gif") %>' width="20" height="20"
                    align="middle">
            </td>
            <td align="right" nowrap="nowrap" class="top_style3">
                <%if (Session["user"] != null)
                  {%><%=rolename  %>
                | <a href="#" class="top_style2">About</a> | <a href="#" class="top_style2">Help</a>
                | <a href="#" class="top_style2" onclick='logout()'>Log Out</a> &nbsp;&nbsp;
                <%} %>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" class="top_style4">
                <%if (Session["user"] != null)
                  {%>
                 
                <div>
                   <b> <%= "" + Session["userName"] %></b>&nbsp;&nbsp;&nbsp;
                </div>
                <%}%>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" valign="bottom" height="30" style="padding: 5px;">
                <%fpcore.Model.UserAC user = Session["user"] as fpcore.Model.UserAC;%>
                <%if (Session["user"] != null && user.roles.Count() > 0)
                  {%>
                <%if (user.roles.Where(r => r.name == "system admin" || r.name == "order admin" || r.name == "order user").Count() > 0)
                  { %>
                <span onclick="fn_click(this);fn_showView('Order');" id="order" class="top_out">Order</span>
                <%}
                  else
                  {%>
                <span id="order" style="display: none;"></span>
                <%}
                  if (user.roles.Where(r => r.name == "system admin" || r.name == "job admin" || r.name == "job user").Count() > 0)
                  { %>
                <span onclick="fn_click(this);fn_showView('Job');" id="job" class="top_out">Job</span>
                <%}
                  else
                  {%>
                <span id="job" style="display: none;"></span>
                <%}
                  if (user.roles.Where(r => r.name == "system admin" || r.name == "delivery admin" || r.name == "delivery user").Count() > 0)
                  { %>
                <span onclick="fn_click(this);fn_showView('Delivery');" id="delivery" class="top_out">
                    Delivery</span>
                <%}
                  else
                  {%>
                <span id="delivery" style="display: none;"></span>
                <%}
                  if (user.roles.Where(r => r.name == "system admin" || r.name == "inventory admin" || r.name == "inventory user").Count() > 0)
                  { %>
                <span onclick="fn_click(this);fn_showView('Inventory');" id="inventory" class="top_out">
                    Inventory</span>
                <%}
                  else
                  {%>
                <span id="inventory" style="display: none;"></span>
                <%}
                  if (user.roles.Where(r => r.name == "system admin").Count() > 0)
                  { %>
                <span onclick="fn_click(this);fn_showView('Admin');" id="admin" class="top_out">Admin</span>
                <%}
                  else
                  {%>
                <span id="admin" style="display: none;"></span>
                <%}
                  }
                  else
                  { %>
                <span style="display: none;" id="order"></span><span style="display: none;" id="job">
                </span><span style="display: none;" id="delivery"></span><span style="display: none;"
                    id="inventory"></span><span style="display: none;" id="admin"></span>
                <%} %>
            </td>
        </tr>
    </table>
</div>
