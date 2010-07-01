<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="topdiv">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="top_getting_opacity">
<tr>
    <td width="1%" rowspan="2" class="top_style1">
    </td>
    <td width="60%" nowrap="nowrap" class="top_style1">
        &nbsp;&nbsp;<img border="0" src=<%=Html.link("Content/images/001_12.gif") %> width="20" height="20" align="middle">
        Fingerprint Order and Delivery System
        <img border="0" src=<%=Html.link("Content/images/001_12.gif") %> width="20" height="20" align="middle">
    </td>
    <td align="right" nowrap="nowrap" class="top_style3">
        <%if (Session["user"] != null)
          {%>
        | <a href="#" class="top_style2">About</a> | <a href="#"
            class="top_style2">Help</a> | <a href="#" class="top_style2" onclick='logout()'>Log Out</a> &nbsp;&nbsp;
        <%} %>
    </td>
</tr>
<tr>
    <td colspan="2" align="right" class="top_style4">
        <%if (Session["user"] != null){%>
            <div>
                <%= "" + Session["userName"] %>&nbsp;&nbsp;&nbsp;
            </div>
        <%}%>
    </td>
</tr>
<tr>
    <td colspan="3" align="center" valign="bottom" height="30" style="padding: 5px;">
        <%if (Session["user"] != null)
          {%>
            <span onclick="fn_click(this);fn_showView('Order');" id="order" class="top_out">Order</span>
            <span onclick="fn_click(this);fn_showView('Job');" id="job" class="top_out">Job</span>
            <span onclick="fn_click(this);fn_showView('Delivery');" id="delivery" class="top_out">Delivery</span> 
            <span onclick="fn_click(this);fn_showView('Inventory');" id="inventory" class="top_out">Inventory</span> 
            <span onclick="fn_click(this);fn_showView('Admin');" id="admin" class="top_out">Admin</span>
        <%} %>
    </td>
</tr>
</table>
</div>