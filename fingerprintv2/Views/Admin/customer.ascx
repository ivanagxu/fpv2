<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div align="center" id="customerdata">
    <%
        string sort = ViewData["sortExpression"] as string;
        bool? diretion = ViewData["sortDiretion"] as bool?;
        var boolvalue = !diretion;
        int? index = ViewData["pageIndex"] as int?;
        int? size = ViewData["pageSize"] as int?;
        int? pageCount = ViewData["pageCount"] as int?;
        List<fpcore.Model.Customer> customers = ViewData["customers"] as List<fpcore.Model.Customer>;
        int? count = ViewData["count"] as int?;
    %>
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
        <tr>
            <td class="admin_user_location">
                Administration → Customer
            </td>
        </tr>
        <tr>
            <td align="left" nowrap="nowrap" style="height: 35px">
                <input type="button" class="std_btn" value="Add" id="btn_add" />
                <input type="button" class="std_btn" value="Edit" id="btn_edit" />
                <input type="button" class="std_btn" value="Delete" id="btn_delete" />
            </td>
        </tr>
        <tr>
            <td class="admin_user_location">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table1">
                    <tr>
                        <td class="admin_user_paging">
                            Showing :
                            <%=count %>
                            -
                            <%=index %>
                            of
                            <%=pageCount %>
                            <%if (index == null || index <= 1)
                              {%>
                            <a class="delivery_show_nav">
                                <img border="0" height="16" src="../content/images/image/control_start.gif" style="vertical-align: middle;
                                    border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                    width="16" /></a><a class="delivery_show_nav">
                                        <img height="16" src="../content/images/image/control_back.gif" style="vertical-align: middle;
                                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            width="16" /></a>
                            <%}
                              else
                              { %>
                            <a class="delivery_show_nav" style="cursor: pointer;" onclick="getData(1)">
                                <img border="0" height="16" src="../content/images/image/control_start_blue.gif"
                                    style="vertical-align: middle; border-top-style: none; border-right-style: none;
                                    border-left-style: none; border-bottom-style: none" width="16" /></a><a style="cursor: pointer;"
                                        onclick="getData(<%=index-1%>)">
                                        <img height="16" src="../content/images/image/control_back_blue.gif" style="vertical-align: middle;
                                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            width="16" /></a>
                            <%} %>
                            <%if (index >= pageCount)
                              { %>
                            <a class="delivery_show_nav">
                                <img height="16" src="../content/images/image/control_play.gif" style="vertical-align: middle;
                                    border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                    width="16" /></a> <a class="delivery_show_nav">
                                        <img height="16" src="../content/images/image/control_end.gif" style="vertical-align: middle;
                                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            width="16" />
                                    </a>
                            <%}
                              else
                              { %>
                            <a class="delivery_show_nav" style="cursor: pointer;" onclick="getData(<%=index+1 %>);">
                                <img height="16" src="../content/images/image/control_play_blue.gif" style="vertical-align: middle;
                                    border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                    width="16" /></a> <a class="delivery_show_nav" style="cursor: pointer;" onclick="getData(<%=pageCount %>);">
                                        <img height="16" src="../content/images/image/control_end_blue.gif" style="vertical-align: middle;
                                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            width="16" />
                                    </a>
                            <%} %>
                            Show
                            <select class="dropdownlist" name="select3" id="select3">
                                <%if (size == 10)
                                  { %>
                                <option selected="selected">10</option>
                                <%}
                                  else
                                  { %>
                                <option>10</option>
                                <%} %>
                                <%if (size == 25)
                                  { %>
                                <option selected="selected">25</option>
                                <%}
                                  else
                                  { %>
                                <option>25</option>
                                <%} %>
                                <%if (size == 50)
                                  { %>
                                <option selected="selected">50</option>
                                <%}
                                  else
                                  { %>
                                <option>50</option>
                                <%} %>
                                <%if (size == 100)
                                  { %>
                                <option selected="selected">100</option>
                                <%}
                                  else
                                  { %>
                                <option>100</option>
                                <%} %>
                            </select>
                            per page
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="admin_user_location">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="admin_user_data_dg_table">
                    <tr>
                        <td align="center" class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'"
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            <input id="Checkbox1" type="checkbox" />
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseover="className='admin_user_data_dg_header_1_hover'"
                            onmouseout="className='admin_user_data_dg_header_1'" onclick='javascript:getResult("company_code");'>
                            Company Code
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'"
                            onmouseover="className='admin_user_data_dg_header_1_hover'" onclick='javascript:getResult("company_name");'>
                            Company Name
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'"
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Contact Person
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'"
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Contact Tel.
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'"
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Address
                        </td>
                    </tr>
                    <%foreach (var item in customers)
                      { %>
                    <tr>                       
                        <%Html.RenderAction("customercontact", "admin", new { companyid = item.objectId.ToString(), companyname = item.company_name, code = item.company_code });%>                     
                    </tr>
                    <%} %>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="55%"  id="dialogForm"
                    style="display: none;">
                    <tr>
                        <td colspan="2" height="40px">
                            <input type="button" class="std_btn" value="Save" id="btn_save" />
                            <input type="button" class="std_btn" value="Cancel" id="btn_cancel" />
                            <input type ="hidden" value ="0" id ="hfcustomerid" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Company Code :
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T14" size="20" id="txtcompanycode">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Company Name :
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T16" size="20" id="txtcompanyname">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Contact Person :
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T21" size="20" id="txtcontactperson">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Contact Tel. :
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T22" size="20" id="txtcontacttel">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Address
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T23" size="20" id="txtaddress">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        var txtcompanycode = $("#txtcompanycode");
        var txtcompanyname = $("#txtcompanyname");
        var txtcontactperson = $("#txtcontactperson");
        var txtcontacttel = $("#txtcontacttel");
        var txtaddress = $("#txtaddress");        
        var hfcustomerid = $("#hfcustomerid");

        var btn_add = $("#btn_add");
        var btn_edit = $("#btn_edit");
        var btn_delete = $("#btn_delete");
        var btn_save = $("#btn_save");
        var btn_cancel = $("#btn_cancel");

        btn_save.click(function() {
            var code = txtcompanycode.val();
            var name = txtcompanyname.val();
            var person = txtcontactperson.val();
            var tel = txtcontacttel.val();
            var address = txtaddress.val();
            var cid = hfcustomerid.val();
            $.post('<%=Url.Action("addcustomer","admin") %>', { code: code, name: name, person: person, tel: tel, address: address, cid: cid }, function(result) {
                alert(result);
                hidedialogForm();
                reload();
            });
        });



        btn_delete.click(function() {
            var ids = "";

            $("input[name=checkbox2]").each(function() {
                if (this.checked == true) {
                    ids = ids + $(this).attr("id") + ",";
                }
            });

            $.post('<%=Url.Action("deletecustomer","admin") %>', { ids: ids }, function(result) {
                alert(result);
                reload();
            });

        });

        btn_add.click(function() {
            hidedialogForm();
            showdialogForm();
            clearForm();
        });
        btn_cancel.click(function() {
            hidedialogForm();
            clearForm();
        });

        $("#btn_edit").click(function() {
            hidedialogForm();
            var i = 0;
            $("input[name=checkbox2]").each(function() {
                if (this.checked == true) {
                    i++;
                }
            });
            if (i > 0) {

                $("input[name=checkbox2]").each(function() {
                    if (this.checked == true) {
                        var str = $(this).val();
                        var arr = str.split('5,,,,,5');
                        hfcustomerid.val(arr[0]);
                        txtcompanycode.val(arr[1]);
                        txtcompanyname.val(arr[2]);
                        txtcontactperson.val(arr[3]);
                        txtcontacttel.val(arr[4]);
                        txtaddress.val(arr[5]);
                    }
                });
                showdialogForm();

            } else {
                alert("Please select one item.");
            }
        });
        
        function clearForm() {
            txtaddress.val("");
            txtcompanycode.val("");
            txtcompanyname.val("");
            txtcontactperson.val("");
            txtcontacttel.val("");
            hfcustomerid.val("");
        }

  
        $("input[name=checkbox2]").each(function() {
            $(this).click(function() {
                if (this.checked == true) {
                    var id = $(this).attr("id");

                    $("input[name=checkbox2]").each(function() {
                        if ($(this).attr("id") != id) {
                            $(this).attr("checked", false);
                        }
                    });
                }
            });
        });

        function showdialogForm() {
            $("#dialogForm").show();
            location.href = "#dialogForm";
        }

        function hidedialogForm() {

            location.href = "#customerdata";
            $("#dialogForm").hide();
        }

        function getResult(result) {
           
            var sort = "<%=sort %>";
            var diretion = "<%=boolvalue %>";
            var index = "<%=index %>";
            var size = "<%=size %>";
            $.get('<%=Url.Action ("customer","admin") %>', { random: Math.random(), sortExpression: result, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#customerdata").html(result);
              
            });
        }
        function reload() {
            
            $.get('<%=Url.Action ("customer","admin") %>', { random: Math.random() }, function(result) {

                $("#renderData").html(result);
            });

          
        }
    
        $("#select3").change(function() {
            getSizeData($("#select3").val());
        });

        function getSizeData(pagesize) {
           
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = "<%=index %>";
            var size = pagesize;
            $.get('<%=Url.Action ("customer","admin") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#customerdata").html(result);
            });
           
        }

        function getData(pageindex) {
           
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = pageindex;
            var size = "<%=size %>";
            $.get('<%=Url.Action ("customer","admin") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#customerdata").html(result);
            });
          
        }
    </script>

</div>
