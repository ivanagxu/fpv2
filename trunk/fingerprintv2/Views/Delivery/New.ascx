<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div align="center" id="divnew">
    <%var delivery = ViewData["delivery"] as fpcore.Model.Delivery;
      if (delivery == null)
          delivery = new fpcore.Model.Delivery();
      if (delivery.contact == null)
          delivery.contact = new fpcore.Model.CustomerContact();
      if (delivery.contact.customer == null)
          delivery.contact.customer = new fpcore.Model.Customer();
      if (delivery.requested_by == null)
          delivery.requested_by = new fpcore.Model.UserAC();
      if (delivery.handled_by == null)
          delivery.handled_by = new fpcore.Model.UserAC();
    %>
    <%using (Html.BeginForm("Add", "Delivery"))
      { %>

    <script type="text/javascript" src="../Content/js/jquery-autocomplete/lib/jquery.js"></script>

    <script type='text/javascript' src='../Content/js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script>

    <script type='text/javascript' src='../Content/js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>

    <script type='text/javascript' src='../Content/js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

    <script type='text/javascript' src='../Content/js/jquery-autocomplete/jquery.autocomplete.js'></script>

    <link rel="stylesheet" type="text/css" href="../Content/js/jquery-autocomplete/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="../Content/js/jquery-autocomplete/lib/thickbox.css" />
    
    
    <link href="../Content/js/calendar/jquery-calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Content/js/calendar/jquery-calendar.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
        <tr>
            <td class="delivery_location">
                Delivery ->
                <%=string.IsNullOrEmpty (delivery.number)?"New Delivery":delivery.number %>
            </td>
        </tr>
        <tr>
            <td align="left" nowrap="nowrap" style="height: 35px">
                <input type="submit" class="std_btn" value="Confirm" id="btn_save" />
                <input type="button" class="std_btn" value="Cancel" id="btn_cancel" />
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="" class="delivery_request_table">
                    <tr>
                        <td class="delivery_request_title" colspan="4">
                            Part I - Item Information
                            <%=Html.Hidden("objectid", delivery.objectId, new { @id = "objectid" }) %>
                        </td>
                    </tr>
                    <tr>
                       <%-- <td align="right" class="delivery_request_column_header">
                            Order No.
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("number", delivery.number, new { @size = "20", @id = "txtnumber" }) %>
                        </td>--%>
                        <td align="right" class="delivery_request_column_header">
                            Part No. (Optional)
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <%=Html.TextBox("partno", delivery.part_no, new { @size = "20", @id = "txtpartno" })%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Non-Order (Pls. justify)
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <%=Html.TextBox("nonorder", delivery.non_order, new { @size = "20", @id = "txtnonorder" })%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Dimension :<img border="0" src="../content/images/image/calculator32.png" width="32"
                                height="32" class="sel_button">
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            L
                            <%=Html.TextBox("length", delivery.length, new { @size = "20", @id = "txtlength" })%>
                            cm x&nbsp; W
                            <%=Html.TextBox("width", delivery.width, new { @size = "20", @id = "txtwidth" })%>
                            cm x H&nbsp;
                            <%=Html.TextBox("height", delivery.height, new { @size = "20", @id = "txtheight" })%>
                            cm
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Weight :
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <%=Html.TextBox("weight", delivery.weight, new { @size = "20", @id = "txtweight" })%>
                            lbs
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Delivery Type :
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <select id="delivery_type" name ="delivery_type">
                                <%if (delivery.delivery_type != null && delivery.delivery_type.Trim() == "Send")
                                  { %>
                                <option selected value="Send">Send</option>
                                <%}
                                  else
                                  { %>
                                <option value="Send">Send</option>
                                <%} %>
                                <%if (delivery.delivery_type != null && delivery.delivery_type.Trim() == "Receive")
                                  { %>
                                <option selected value="Receive">Receive</option>
                                <%}
                                  else
                                  { %>
                                <option value="Receive">Receive</option>
                                <%} %>
                            </select>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="" class="delivery_request_table">
                    <tr>
                        <td class="delivery_request_title" colspan="4">
                            Part II - Shipping Address
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Company Code
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("code", delivery.contact.customer.company_code, new { @size = "20", @id = "txtcode" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Company Name
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("companyname", delivery.contact.customer.company_name, new { @size = "20", @id = "txtcompanyname" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Street
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("street1", delivery.contact.street1, new { @size = "20", @id = "txtstreet1" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("street2", delivery.contact.street2, new { @size = "20", @id = "txtstreet2" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("street3", delivery.contact.street3, new { @size = "20", @id = "txtstreet3" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            District
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("district", delivery.contact.district, new { @size = "20", @id = "txtDistrict" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            City
                        </td>
                        <td class="delivery_request_cell">
                            <%-- &nbsp;<select size="1" name="D6" id="slcity">
                                <option selected>HK</option>
                                <option>Kln.</option>
                                <option>NT.</option>
                            </select>--%>
                            <%=Html.TextBox("city", delivery.contact.city, new { @size = "20", @id = "city" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Contact
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("contact", delivery.contact.contact_person, new { @size = "20", @id = "txtcontact" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Tel.
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("tel", delivery.contact.tel, new { @size = "20", @id = "txttel" })%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Mobile
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("mobile", delivery.contact.mobile, new { @size = "20", @id = "txtmobile" })%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Remarks
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextArea("remarks", delivery.contact.remarks, new { @rows = "6", @id = "txtremark" }) %>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="" class="delivery_request_table">
                    <tr>
                        <td class="delivery_request_title" colspan="4">
                            Part III - Assignment
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Request By :
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.Hidden("requestby", delivery.requested_by.objectId, new { @id = "txtrequestby" }) %>
                            <%=Html.TextBox("requestname", delivery.requested_by.eng_name, new { @id = "txtrequestname", @size = "20" }) %>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Deadline :
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextBox("deadline", delivery.deadline, new { @id = "txtdeadline", @size = "20" }) %><br />
                             (Click the input text will show the calendar panel.)
                            <img border="0" class="sel_button" src="../content/images/image/calendar-icon.png"
                                alt="Calendar" width="32" height="32" style ="display:none ;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header" style="width: 100px;">
                            Handled By:
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.Hidden("handleby", delivery.requested_by.objectId, new { @id = "txthandledby" }) %>
                            <%=Html.TextBox("handlename", delivery.handled_by.eng_name, new { @id = "txthandlename", @size = "20" })%>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Notes :
                        </td>
                        <td class="delivery_request_cell">
                            <%=Html.TextArea("notes", delivery.notes, new { @cols = "45", @rows = "6", @id = "txtnotes" }) %>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%} %>

    <script type="text/javascript">

        var btn_cancel = $("#btn_cancel");
        var btn_save = $("#btn_save");

        var txtcity = $("#txtcity");
        var txtcompanyname = $("#txtcompanyname");
        var txtcontact = $("#txtcontact");
        var txtdeadline = $("#txtdeadline");
        var txtDistrict = $("#txtDistrict");
        var txthandledby = $("#txthandledby");
        var txtheight = $("#txtheight");
        var txtlength = $("#txtlength");
        var txtmobile = $("#txtmobile");
        var txtnonorder = $("#txtnonorder");
        var txtnotes = $("#txtnotes");
        var txtnumber = $("#txtnumber");
        var txtpartno = $("#txtpartno");
        var txtremark = $("#txtremark");
        var txtrequestby = $("#txtrequestby");
        var txtstreet1 = $("#txtstreet1");
        var txtstreet2 = $("#txtstreet2");
        var txtstreet3 = $("#txtstreet3");
        var txttel = $("#txttel");
        var txtupdateby = $("#txtupdateby");
        var txtupdatedate = $("#txtupdatedate");
        var txtweight = $("#txtweight");
        var txtwidth = $("#txtwidth");
        var slcity = $("#slcity");
        var txtcode = $("#txtcode");

        $(document.body).ready(function() {
            $("input[type=text]").bind('keydown', function(event) {
                if (event.keyCode == 13) {
                    event.keyCode = 0;
                    return false;
                }
            });

            $("#txtdeadline").calendar({
                autoPopUp: 'both',
                buttonImageOnly: true,
                buttonImage: '../content/images/image/calendar-icon.png',

                closeAtTop: false
            });

            $.post('<%=Url.Action("getUsers","Delivery") %>', {}, function(result) {
                $("#txtrequestname").autocomplete(result, {
                    width: 155,
                    scrollHeight: 100,
                    matchContains: true,
                    highlightItem: false,
                    formatItem: function(row, i, max, term) {
                        return row.eng_name;
                    },
                    formatResult: function(row) {
                        return row.eng_name;
                    }
                });
                $("#txthandlename").autocomplete(result, {
                    width: 155,
                    scrollHeight: 100,
                    matchContains: true,
                    highlightItem: false,
                    formatItem: function(row, i, max, term) {
                        return row.eng_name;
                    },
                    formatResult: function(row) {
                        return row.eng_name;
                    }
                });
            }, "json");
            $("#txtrequestname").result(function(event, data, formatted) {
                var txtrequestby = $("#txtrequestby");
                var txtrequestname = $("#txtrequestname");
                txtrequestby.val(data.objectId);
                txtrequestname.val(data.eng_name);
            });
            $("#txthandlename").result(function(event, data, formatted) {
                var txthandledby = $("#txthandledby");
                var txthandlename = $("#txthandlename");
                txthandledby.val(data.objectId);
                txthandlename.val(data.eng_name);

            });

            $.post('<%=Url.Action("GetCustomers","Delivery") %>', {}, function(result) {

                txtcode.autocomplete(result, {
                    width: 155,
                    scrollHeight: 100,
                    matchContains: true,
                    highlightItem: false,
                    formatItem: function(row, i, max, term) {
                        return row.cid + "(" + row.cname + ")";
                    },
                    formatResult: function(row) {
                        return row.cid;
                    }
                });
            }, "json");

            txtcode.result(function(event, data, formatted) {

                var hidden = $("#txttest");
                if (data.street1 == null)
                    data.street1 = "";
                if (data.street2 == null)
                    data.street2 = "";
                if (data.customer.company_name == null)
                    data.company_name = "";
                if (data.street3 == null)
                    data.street3 = "";
                if (data.district == null)
                    data.district = "";
                if (data.remarks == null)
                    data.remarks = "";
                if (data.mobile == null)
                    data.mobile = "";
                if (data.tel == null)
                    data.tel = "";

                txtcompanyname.val(data.customer.company_name);
                txtcity.val(data.city);
                txtcontact.val(data.contact_person);
                txtstreet1.val(data.street1);
                txtstreet2.val(data.street2);
                txtstreet3.val(data.street3);
                txtDistrict.val(data.district);
                txtremark.val(data.remarks);
                txtmobile.val(data.mobile);
                txttel.val(data.tel);
            });

            btn_cancel.click(function() {

                returnData();
            });

            function returnData() {
                $("#a_archive").css("font-weight", "normal");
                $("#a_new").css("font-weight", "normal");
                $("#a_deliverydata").css("font-weight", "bold");

                $.get('<%=Url.Action ("DeliveryData","Delivery") %>', { random: Math.random() }, function(result) {

                    $("#renderData").html(result);
                });


            }
        });
    </script>

</div>
