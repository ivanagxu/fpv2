<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div align="center" id="divnew">

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
                <input type="button" class="std_btn" value="Confirm" id="btn_save" />
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
                            <input type="text" name="T1" size="20" id="txtnumber">
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Part No. (Optional)
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="T2" size="20" id="txtpartno">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Non-Order (Pls. justify)
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <input type="text" name="T21" size="20" id="txtnonorder">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Dimension :<img border="0" src="../content/images/image/calculator32.png" width="32"
                                height="32" class="sel_button">
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            L
                            <input type="text" name="T14" size="20" id="txtlength">
                            cm x&nbsp; W
                            <input type="text" name="T17" size="20" id="txtwidth">
                            cm x H&nbsp;
                            <input type="text" name="T18" size="20" id="txtheight">
                            cm
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Weight :
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <input type="text" name="T16" size="20" id="txtweight">
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
                        <td align="right" class="delivery_request_column_header">
                            Company Code
                        </td>
                        <td class="delivery_request_cell">
                            <input id="txtcode"  type ="text"/>
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
                            <input type="text" name="T22" size="20" id="txtcompanyname">
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
                            <input type="text" name="T3" size="20" id="txtstreet1">
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
                            <input type="text" name="T6" size="20" id="txtstreet2">
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
                            <input type="text" name="T19" size="20" id="txtstreet3">
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
                            <input type="text" name="T7" size="20" id="txtDistrict">
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
                            &nbsp;<select size="1" name="D6" id="slcity">
                                <option selected>HK</option>
                                <option>Kln.</option>
                                <option>NT.</option>
                            </select><input type="radio" value="V1" name="R14" checked id="txtcity">Other
                            <input type="text" name="T5" size="20">
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
                            <input type="text" name="T10" size="20" id="txtcontact">
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Tel.
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="T12" size="20" id="txttel">
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
                            <input type="text" name="T13" size="20" id="txtmobile">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Remarks
                        </td>
                        <td class="delivery_request_cell">
                            <textarea rows="6" name="S1" cols="45" id="txtremark"></textarea>
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
                            <input type="text" name="T25" size="20" id="txtrequestby">
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Deadline :
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="T23" size="20" id="txtdeadline">
                            <img border="0" class="sel_button" src="../content/images/image/calendar-icon.png"
                                alt="Calendar" width="32" height="32">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header" style="width: 100px;">
                            Handled By:
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="T22" size="20" id="txthandledby">
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
                            <textarea rows="6" name="S2" cols="45" id="txtnotes"></textarea>
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
                            <input type="text" name="T6" size="20" id="txtupdateby">
                            on
                            <input type="text" name="T24" size="20" id="txtupdatedate">
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
<input id ="txttest" />
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
                        return row.company_code + "(" + row.company_name + ")";
                    },
                    formatResult: function(row) {
                        return row.company_code;
                    }
                });
            }, "json");

            txtcode.result(function(event, data, formatted) {
                var hidden = $("#txttest");
                txtcompanyname.val(data.company_name);
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

            btn_save.click(function() {
                var city = "";
                $.post('<%=Url.Action("add","Delivery") %>',
            { city: city,companyname: txtcompanyname.val(),contact: txtcontact.val(),deadline: txtdeadline.val(),
                district: txtDistrict.val(),
                handleby: txthandledby.val(),
                height: txtheight.val(),
                length: txtlength.val(),
                mobile: txtmobile.val(),
                nonorder: txtnonorder.val(),
                notes: txtnotes.val(),
                number: txtnumber.val(),
                partno: txtpartno.val(),
                remark: txtremark.val(),
                requestby: txtrequestby.val(),
                street1: txtstreet1.val(),
                street2: txtstreet2.val(),
                street3: txtstreet3.val(),
                tel: txttel.val(),
                updateby: txtupdateby.val(),
                updatedate: txtupdatedate.val(),
                weight: txtweight.val(),
                width: txtwidth.val()

            }, function(result) {
                alert("fsdf");
            });
                returnData();

            });
        });
    </script>

</div>
