<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%var customercontact = ViewData["customercontact"] as fpcore.Model.CustomerContact;
  if (customercontact == null)
      customercontact = new fpcore.Model.CustomerContact();
  var companyid = ViewData["companyid"] as string;
  var company_code = ViewData["companycode"] as string ;
  var company_name = ViewData["companyname"] as string; %>
  <%var str = companyid + "5,,,,,5" + company_code + "5,,,,,5" + company_name + "5,,,,,5" + customercontact.contact_person + "5,,,,,5" + customercontact.tel + "5,,,,,5" + customercontact.address; %>
<td align="center" class="admin_user_data_dg_row_alter">
    <input id="<%=companyid %>" type="checkbox" name="checkbox2" value="<%=str %>" />
</td>
<td class="admin_user_data_dg_row_alter" width="78" align="center">
    <%=company_code%>
</td>
<td align="center" class="admin_user_data_dg_row_alter">
    <%=company_name%>
</td>
<%if (customercontact != null)
  { %>
<td align="center" class="admin_user_data_dg_row_alter">
    <%=customercontact.contact_person  %>&nbsp;
</td>
<td class="admin_user_data_dg_row_alter" align="center">
    <%=customercontact.tel %>&nbsp;
</td>
<td class="admin_user_data_dg_row_alter" align="center">
    <%=customercontact.address %>&nbsp;
</td>
<%}
  else
  { %>
<td align="center" class="admin_user_data_dg_row_alter">
    &nbsp;
</td>
<td class="admin_user_data_dg_row_alter" align="center">
    &nbsp;
</td>
<td class="admin_user_data_dg_row_alter" align="center">
    &nbsp;
</td>
<%} %>