<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="adminData" align="center">
 <%
        string sort = ViewData["sortExpression"] as string;
        bool? diretion = ViewData["sortDiretion"] as bool?;
        int? index = ViewData["pageIndex"] as int?;
        int? size = ViewData["pageSize"] as int?;
        int? pageCount = ViewData["pageCount"] as int?;
        List<fpcore.Model.UserAC> users = ViewData["users"] as List<fpcore.Model.UserAC>;
        bool? direction = ViewData["direction"] as bool?;
    %>
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
        <tr>
            <td class="admin_user_location">
                Administration → User
            </td>
        </tr>
        <tr>
            <td align="left" nowrap="nowrap" style="height: 35px">
                <input type="button" class="std_btn" value="Add"  id="btn_add" />
                <input type="button" class="std_btn" value="Edit" id="btn_edit" />
                <input type="button" class="std_btn" value="Delete" id="btn_delete" />
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
                        <td class="admin_user_data_dg_header_1" onmouseover="className='admin_user_data_dg_header_1_hover'" onclick ='javascript:getResult("UserAC.user_name");'
                            onmouseout="className='admin_user_data_dg_header_1'">
                            Username
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'" onclick ='javascript:getResult("UserAC.eng_name");'
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Name
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'" onclick ='javascript:getResult("UserAC.post");'
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Post
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'" onclick ='javascript:getResult("UserAC.email");'
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Email
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'" 
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Group
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'"　onclick ='javascript:getResult("UserAC.status");'
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Status
                        </td>
                    </tr>
                    <%foreach (var item in users)
                      {
                          var str = item.objectId + "," + item.user_name + "," + item.eng_name + "," + item.chi_name + "," + item.post + "," + item.email + "," + item.user_password + "," + item.remark + "," + item.status + ",";
                          %>
                    <tr>
                        <td align="center" class="admin_user_data_dg_row_alter">
                            <input id="<%=item.objectId %>" type="checkbox" name ="checkbox2" value="<%=str %>"/>
                        </td>
                        <td class="admin_user_data_dg_row_alter" width="78" align="center">
                            <%=item.user_name %>&nbsp;
                        </td>
                        <td align="center" class="admin_user_data_dg_row_alter">
                            <%=item.eng_name  %>&nbsp;
                        </td>
                        <td align="center" class="admin_user_data_dg_row_alter">
                            <%=item.post %>&nbsp;
                        </td>
                        <td class="admin_user_data_dg_row_alter" align="center">
                            <%=item.email %>&nbsp;
                        </td>
                        <td class="admin_user_data_dg_row_alter" align="center">
                        <%string name = string.Empty; %>
                           <%if (item.roles != null)
                             {
                                 var roles = item.roles;
                                 foreach (var r in roles)
                                 {
                                     name = name+r.name + ",";   
                                 }
                                 if (roles.Count > 0)
                                     name = name.Substring(0, name.Length - 1);
                             } %>
                             <%=name %>&nbsp;
                        </td>
                        <td class="admin_user_data_dg_row_alter" align="center">
                            <%=item.status%>&nbsp;
                        </td>
                    </tr>
                    <%} %>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="55%"  id="dialogForm" style ="display:none;">
                    <tr>
                        <td colspan="2" height="40px">
                            <input type="button" class="std_btn" value="Save" target="mainframe" id="btn_save" />
                            <input type="button" class="std_btn" value="Cancel" target="mainframe" id="btn_cancel" />
                            <input type ="hidden" id ="hdobjid" value ="0" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Username :
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T14" size="20" id="txtusername">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Name (English) :
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T16" size="20"  id="txtnameen">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Name (Chinese)
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T21" size="20" id="txtnamecn">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Post
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T22" size="20" id="txtpost">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Email
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T23" size="20" id="txtemail">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Password
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T24" size="20" id="txtpwd">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Remark
                        </td>
                        <td class="admin_user_cell">
                            <input type="text" name="T25" size="20" id="txtremark">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="admin_user_column_header">
                            Status
                        </td>
                        <td class="admin_user_cell" id="slstatus">
                            <select size="1" name="D7">
                                <option value ="Active">Active</option>
                                <option value ="Inactive">Inactive</option>
                            </select>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var txtusername = $("#txtusername");
        var txtnameen = $("#txtnameen");
        var txtnamecn = $("#txtnamecn");
        var txtpost = $("#txtpost");
        var txtemail = $("#txtemail");
        var txtpwd = $("#txtpwd");
        var txtremark = $("#txtremark");
        var slstatus = $("#slstatus");
        var hdobjid = $("#hdobjid");
     

        function getResult(result) { 
            $.get('<%=Url.Action ("admin","Admin") %>', { random: Math.random(), query: result, direction: "<%=direction %>" }, function(result) {
                $("#adminData").html(result);
            });
        }
        function reLoad() {
            $.get('<%=Url.Action ("admin","Admin") %>', { random: Math.random() }, function(result) {
                $("#adminData").html(result);
            });
        }



        $("#btn_save").click(function() {

            $.post('<%=Url.Action ("AddAdmin","Admin") %>', { id: hdobjid.val(), random: Math.random(), username: txtusername.val(), nameen: txtnameen.val(), namecn: txtnamecn.val(), post: txtpost.val(), email: txtemail.val(), pwd: txtpwd.val(), remark: txtremark.val(), status: slstatus.find("option:selected").text() }, function(result) {
                alert(result.replace("\"", "").replace("\"", ""));
                location.href = "#adminData";
                reLoad();
            });
        }, "Json");
        $("#btn_cancel").click(function() {

            location.href = "#adminData";
            $("#dialogForm").hide();
        });

        $("#btn_add").click(function() {
        location.href = "#adminData";
            $("#dialogForm").hide();
            hdobjid.val("0");
            txtusername.val("");
            txtnameen.val("");
            txtnamecn.val("");
            txtpost.val("");
            txtemail.val("");
            txtpwd.val("");
            txtremark.val("");
            $("#dialogForm").show();
            location.href = "#dialogForm";
        });

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

        $("#btn_edit").click(function() {
        location.href = "#adminData";
            $("#dialogForm").hide();
            var i = 0;
            $("input[name=checkbox2]").each(function() {
                if (this.checked == true) {
                    i++;
                }
            });
            if (i > 0) {
                $("#dialogForm").show();
                $("input[name=checkbox2]").each(function() {
                    if (this.checked == true) {
                        var str = $(this).val();

                        var arr = str.split(',');
                        hdobjid.val(arr[0]);
                        txtusername.val(arr[1]);
                        txtnameen.val(arr[2]);
                        txtnamecn.val(arr[3]);
                        txtpost.val(arr[4]);
                        txtemail.val(arr[5]);
                        txtpwd.val(arr[6]);
                        txtremark.val(arr[7]);
                        $("#slstatus option").each(function() {
                            if (this.text == arr[8]) {
                                this.selected = true;
                            }
                        });

                    }
                });

                location.href = "#dialogForm";
            } else {
                alert("Please select one item.");
            }
        });


        $("#Checkbox1").click(function() {
            var ids = "";
            if (this.checked == true) {
                $("input[name=checkbox2]").each(function() {

                    $(this).attr("checked", true);
                    ids = ids + $(this).attr("id") + ",";
                });
            } else {
                $("input[name=checkbox2]").each(function() {
                    $(this).attr("checked", false);
                });
            }
        });

        $("#btn_delete").click(function() {
            var ids = "";

            $("input[name=checkbox2]").each(function() {
                if (this.checked == true) {
                    ids = ids + $(this).attr("id") + ",";
                }
            });
            if (ids != "") {

                $.post('<%=Url.Action("DeleteAdmin","Admin")%>', { ids: ids }, function(result) {
                    alert(result);
                });
                reLoad();
            } else {
                alert("Please select one item.");
            }
        });
    </script>
</div>
