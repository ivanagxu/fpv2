<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% var users = ViewData["users"] as List<fpcore.Model.UserAC>;
   var item = ViewData["role"] as fpcore.Model.FPRole;
   var str = string.Empty;
   var ids = string.Empty;%>
<%if (users != null)
  { %>
<%foreach (var u in users)
  { %>
<%str = str + u.eng_name + ",";
  ids = ids + u.objectId + ",";%>
<%} %>
<%} %>
<%if (str != string.Empty)
      str = str.Substring(0, str.Length - 1);
  if (ids != string.Empty)
      ids = ids.Substring(0, ids.Length - 1);
%>
<tr>
    <td align="center" class="admin_user_data_dg_row_alter" width="70px">
        <input id="<%=item.objectId %>" type="checkbox" name="Checkbox2" value="<%=ids %>"
            pid="<%=item.name %>" uid="<%=str %>" />
    </td>
    <td class="admin_user_data_dg_row_alter" width="78px" align="center">
        <%=item.name %>
    </td>
    <td align="center" class="admin_user_data_dg_row_alter">
        <%=str %>
    </td>
</tr>
