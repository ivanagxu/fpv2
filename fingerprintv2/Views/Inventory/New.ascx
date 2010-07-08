<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div align="center">
    <%var inventory = ViewData["inventory"] as fpcore.Model.Inventory;
      var consumptions = ViewData["consumptions"] as List<fpcore.Model.Consumption>;
      if (inventory == null)
          inventory = new fpcore.Model.Inventory();
      if (consumptions == null)
          consumptions = new List<fpcore.Model.Consumption>();
    %>
    <%using (var form = Html.BeginForm("add", "inventory"))
      { %>
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
        <tr>
            <td class="inventory_location">
                Inventory → New Inventory
            </td>
        </tr>
        <tr>
            <td class="inventory_location">
            </td>
        </tr>
        <tr>
            <td class="inventory_location">
                <table border="0" cellpadding="2" cellspacing="0" width="100%" class="inventory_request_table">
                    <tr>
                        <td class="inventory_request_title" colspan="4">
                            Part I - Product Information
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Order No :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" id="order_no" name="orderno" size="20" value="<%=inventory.orderno  %>" />
                        </td>
                        <td class="inventory_request_column_header">
                        </td>
                        <td class="inventory_request_cell">
                            <input type="radio" value="V1" checked name="R1">Order&nbsp; By Customer Name
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Received Date :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" id="recd_date" name="receiveddate" size="20" value="<%=inventory.receiveddate %>" />
                        </td>
                        <td class="inventory_request_column_header">
                        </td>
                        <td class="inventory_request_cell">
                            <input type="radio" value="V1" checked name="R1">Search By Keyword
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Received By :
                        </td>
                        <td class="inventory_request_cell">
                            <%=Html.Hidden("receivedby", inventory.receivedby==null ?string.Empty :inventory.receivedby.objectId.ToString (), new { @size = "20" }) %>
                            <input type="text" id="recd_by" name="receivedbyname" size="20" value="<%=(inventory.receivedby == null ? string.Empty : inventory.receivedby.eng_name) %>" />
                        </td>
                        <td class="inventory_request_column_header">
                        </td>
                        <td class="inventory_request_cell">
                            <input type="radio" value="V1" checked name="R1">Customer Not in List
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Order Deadline :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" name="orderdeadline" size="20" value="<%=inventory.orderdeadline %>" />
                            <img border="0" class="sel_button" src="../content/images/image/calendar-icon.png"
                                alt="Calendar" width="32" height="32">
                        </td>
                        <td class="inventory_request_column_header" valign="middle" align="right">
                            Search By Customer Name:
                        </td>
                        <td class="inventory_request_cell" valign="middle">
                            <input type="text" name="T18" size="20" id="TextboxRef">
                            <input type="button" class="std_btn" value="Search" target="mainframe" id="btn_search">
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Remarks :
                        </td>
                        <td class="inventory_request_cell" rowspan="3">
                            <%=Html.TextArea("remark", inventory.remark, new {@rows="8",@cols="36"}) %>
                        </td>
                        <td class="inventory_request_column_header" align="right">
                            Tel :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" name="Tel" size="20" value="<%=inventory.Tel %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                        </td>
                        <td class="inventory_request_column_header" height="20" align="right">
                            Contact Person :
                        </td>
                        <td class="inventory_request_cell" height="20">
                            <input type="text" name="T20" size="20" value="<%=inventory.contactperson %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                        </td>
                        <td class="inventory_request_column_header" height="20">
                        </td>
                        <td class="inventory_request_cell" height="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Updated By :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" class="txt_displayOnly" id="" name="updateBy" size="20" value="<%=inventory.updateBy %>">
                        </td>
                        <td class="inventory_request_column_header">
                        </td>
                        <td class="inventory_request_cell">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="inventory_location">
                <table border="0" width="100%" id="table1" cellspacing="0" cellpadding="2" class="inventory_request_table">
                    <tr>
                        <td class="inventory_request_title">
                            Part II - Consumption History
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="1" bordercolor="#012038" cellpadding="0" cellspacing="0" width="100%"
                                style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                <tr>
                                    <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                                        onmouseover="className='inventory_data_dg_header_1_hover'">
                                        <p align="center">
                                            <input id="Checkbox4" type="checkbox" name="C3" value="ON" />
                                    </td>
                                    <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                                        onmouseout="className='inventory_data_dg_header_1'">
                                        Quantity (Total)
                                    </td>
                                    <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                                        onmouseout="className='inventory_data_dg_header_1'">
                                        Quantity (Store)
                                    </td>
                                    <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                                        onmouseout="className='inventory_data_dg_header_1'">
                                        Quantity (Used)
                                    </td>
                                    <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                                        onmouseout="className='inventory_data_dg_header_1'">
                                        As at
                                    </td>
                                </tr>
                                <%foreach (var item in consumptions)
                                  { %>
                                <tr>
                                    <td class="inventory_data_dg_row_alter" align="center">
                                        <input id="<%=item.objectId %>" type="checkbox" name="checkbox1" value="<%=item.objectId %>" />
                                    </td>
                                    <td class="inventory_data_dg_row_alter">
                                        <%if (item.totalunit.Trim() == "PCS")
                                          { %>
                                        <%=item.total %>
                                        PCS
                                        <%}
                                          else if (item.totalunit.Trim() == "MM")
                                          { %>
                                        <%=item.total%>
                                        MM X
                                        <%=item.subtotal%>
                                        MM
                                        <%}
                                          else
                                          { %>
                                        <%=item.total  %>
                                        <%} %>
                                    </td>
                                    <td class="inventory_data_dg_row_alter">
                                        <%if (item.storeunit.Trim() == "PCS")
                                          { %>
                                        <%=item.store %>
                                        PCS
                                        <%}
                                          else if (item.storeunit.Trim() == "MM")
                                          { %>
                                        <%=item.store%>
                                        MM X
                                        <%=item.substore%>
                                        MM
                                        <%}
                                          else
                                          { %>
                                        <%=item.subtotal  %>
                                        <%} %>
                                    </td>
                                    <td class="inventory_data_dg_row_alter">
                                        <%if (item.usedunit.Trim() == "PCS")
                                          { %>
                                        <%=item.used%>
                                        PCS
                                        <%}
                                          else if (item.usedunit.Trim() == "MM")
                                          { %>
                                        <%=item.used%>
                                        MM X
                                        <%=item.subused%>
                                        MM
                                        <%}
                                          else
                                          { %>
                                        <%=item.used  %>
                                        <%} %>
                                    </td>
                                    <td class="inventory_data_dg_row_alter">
                                        <%=item.updateDate %>
                                    </td>
                                </tr>
                                <%} %>
                            </table>
                        </td>
                        <tr>
                            <td>
                                <input type="button" class="std_btn" value="New" id="btn_new_parts">
                                <input type="button" class="std_btn" value="Edit" id="btn_new_parts0">
                                <input type="button" class="std_btn" value="Delete" id="btn_new_parts1"><table border="0"
                                    cellpadding="2" cellspacing="0" width="100%" class="inventory_request_table">
                                    <br>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                            Quantity (Total) :
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" checked name="R1" />PCS
                                            <input type="text" id="order_no" name="T16" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" name="R1">MM
                                            <input type="text" id="order_no0" name="T21" size="10">
                                            x
                                            <input type="text" id="order_no1" name="T22" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                            Quantity (Store) :
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" checked name="R2">PCS
                                            <input type="text" id="order_no2" name="T23" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" name="R2">MM
                                            <input type="text" id="order_no6" name="T28" size="10">
                                            x
                                            <input type="text" id="order_no7" name="T29" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                            Quantity (Used) :
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" checked name="R3">PCS
                                            <input type="text" id="order_no5" name="T26" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" name="R3">MM
                                            <input type="text" id="order_no8" name="T30" size="10">
                                            x
                                            <input type="text" id="order_no9" name="T31" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                            Date :
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="text" name="T27" size="20" id="TextboxRef">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="button" class="std_btn" value="Save" id="btn_save">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </table>

                <script>
                
                </script>

            </td>
        </tr>
    </table>

    <script type="text/javascript">

        $("input[name='checkbox1']").each(function() {
            $(this).click(function() {
                var id = $(this).val();
                $("input[name='checkbox1']").each(function() {
                    if ($(this).val() != id) {
                        $(this).attr("checked", false);
                    }
                });
            });
        });

        $("#btn_new_parts0").click(function() {

            var id = "";
            var str = "";
            $("input[name='checkbox1']").each(function() {
                if (this.checked == true) {
                    id = $(this).val();
                    $(this).parent().parent().children().each(function() {
                        alert($(this).html());
                    });
                }
            });
        });
 
    </script>

    <%} %>
</div>
