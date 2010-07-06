<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div align="center" id="divnew">
    <%using (Html.BeginForm("Add", "Delivery"))
      { %>

    <script type="text/javascript" src="../Content/js/jquery-autocomplete/lib/jquery.js"></script>

    <script type='text/javascript' src='../Content/js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script>

    <script type='text/javascript' src='../Content/js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>

    <script type='text/javascript' src='../Content/js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

    <script type='text/javascript' src='../Content/js/jquery-autocomplete/jquery.autocomplete.js'></script>

    <link rel="stylesheet" type="text/css" href="../Content/js/jquery-autocomplete/jquery.autocomplete.css" />
    <link rel="stylesheet" type="text/css" href="../Content/js/jquery-autocomplete/lib/thickbox.css" />
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
        <tr>
            <td class="delivery_location">
                Delivery → New Delivery
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
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Order No.
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="number" size="20" id="txtnumber" />
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Part No. (Optional)
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="partno" size="20" id="txtpartno" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Non-Order (Pls. justify)
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <input type="text" name="nonorder" size="20" id="txtnonorder" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Dimension :<img border="0" src="../content/images/image/calculator32.png" width="32"
                                height="32" class="sel_button">
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            L
                            <input type="text" name="length" size="20" id="txtlength" />
                            cm x&nbsp; W
                            <input type="text" name="width" size="20" id="txtwidth" />
                            cm x H&nbsp;
                            <input type="text" name="height" size="20" id="txtheight" />
                            cm
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Weight :
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <input type="text" name="weight" size="20" id="txtweight" />
                            lbs
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
                        <td align="right" class="delivery_request_column_header"  >
                            Company Code
                        </td>
                        <td class="delivery_request_cell">
                            <input id="txtcode" name="code" type="text" />
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
                            <input type="text" name="companyname" size="20" id="txtcompanyname" />
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
                            <input type="text" name="street1" size="20" id="txtstreet1" />
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
                            <input type="text" name="street2" size="20" id="txtstreet2" />
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
                            <input type="text" name="street3" size="20" id="txtstreet3" />
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
                            <input type="text" name="district" size="20" id="txtDistrict" />
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
                            <input type="text" name="city" size="20" id="txtcity" />
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
                            <input type="text" name="contact" size="20" id="txtcontact" />
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Tel.
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="tel" size="20" id="txttel" />
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
                            <input type="text" name="mobile" size="20" id="txtmobile" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Remarks
                        </td>
                        <td class="delivery_request_cell">
                            <textarea rows="6" name="remarks" cols="45" id="txtremark"></textarea>
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
                            <input type="text" name="requestby" size="20" id="txtrequestby" />
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Deadline :
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="deadline" size="20" id="txtdeadline" />
                            <img border="0" class="sel_button" src="../content/images/image/calendar-icon.png"
                                alt="Calendar" width="32" height="32">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header" style="width: 100px;">
                            Handled By:
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="handleby" size="20" id="txthandledby" />
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
                            <textarea rows="6" name="notes" cols="45" id="txtnotes"></textarea>
                        </td>
                        <td align="right" class="delivery_request_column_header">
                        </td>
                        <td class="delivery_request_cell">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Last Updated By :
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="updateby" size="20" id="txtupdateby" />
                            on
                            <input type="text" name="updatedate" size="20" id="txtupdatedate" />
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
            var datas = null;
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
                $('#loading').show();
                $.get('<%=Url.Action ("DeliveryData","Delivery") %>', { random: Math.random() }, function(result) {

                    $("#renderData").html(result);
                });

                $('#loading-one').parent().fadeOut('slow');
            }
        });
    </script>

</div>
