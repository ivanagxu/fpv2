<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div align="center">
    <% var roles = ViewData["roles"] as List<fpcore.Model.FPRole>;
       var userlist = ViewData["users"] as List<fpcore.Model.UserAC>; %>
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
        <tr>
            <td class="admin_user_location">
                Administration -> Group
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
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="admin_user_data_dg_table">
                    <tr>
                        <td align="center" class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'"
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            <input id="Checkbox1" type="checkbox" />
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseover="className='admin_user_data_dg_header_1_hover'"
                            onmouseout="className='admin_user_data_dg_header_1'">
                            Group
                        </td>
                        <td class="admin_user_data_dg_header_1" onmouseout="className='admin_user_data_dg_header_1'"
                            onmouseover="className='admin_user_data_dg_header_1_hover'">
                            Member
                        </td>
                    </tr>
                    <%foreach (var item in roles)
                      { %>
                      <%Html.RenderAction("RenderGroup", "Admin", new { roleID = item.objectId }); %>
                    <%} %>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGroup">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="display: none;"
                        id="dialogForm">
                        <tr>
                            <td colspan="3" height="40px">
                                <input type="button" class="std_btn" value="Save" id="btn_save" />
                                <input type="button" class="std_btn" value="Cancel" id="btn_cancel" />
                                <input type ="hidden" id ="hfobjectid" value ="0" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admin_user_column_header" colspan="3">
                                Selected Group :
                                <input type="text" id="txtname" size="20" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="admin_user_cell">
                                Members
                            </td>
                            <td class="admin_user_cell">
                            </td>
                            <td class="admin_user_cell">
                                User
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="admin_user_cell">
                                <select size="8" name="D8" id="slmembers" style="width: 100px;" multiple="multiple">
                                </select>
                            </td>
                            <td class="admin_user_cell">
                                <p align="center">
                                    <a id="addsl">
                                        <img border="0" src="../content/images/image/control_back.gif" width="16" height="16"
                                            class="sel_button"></a>
                                    <br>
                                    <a id="removesl">
                                        <img border="0" src="../content/images/image/control_play.gif" width="16" height="16"
                                            class="sel_button"></a>
                            </td>
                            <td class="admin_user_cell">
                                <select size="8" name="D9" id="slusers" style="width: 100px;" multiple="multiple">
                                    <%foreach (var item in userlist)
                                      { %>
                                    <option value="<%=item.objectId %>">
                                        <%=item.eng_name %></option>
                                    <%} %>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var btn_add = $("#btn_add");
        var btn_edit = $("#btn_edit");
        var btn_delete = $("#btn_delete");

        var dialogForm = $("#dialogForm"); //form

        var btn_save = $("#btn_save");
        var btn_cancel = $("#btn_cancel");

        var txtname = $("#txtname");
        var slmembers = $("#slmembers");
        var slusers = $("#slusers");

        var addsl = $("#addsl");
        var removesl = $("#removesl");
        var hfobjectid = $("#hfobjectid");

        function showDialogForm() {
            dialogForm.show();
        }

        function hideDialogForm() {
            dialogForm.hide();
        }


        btn_add.click(function() {
            showDialogForm();
            txtname.val("");
            hfobjectid.val("0");
            txtname.removeAttr("disabled");
            $("#slmembers option").appendTo(slusers);
            $("input[name=Checkbox2]").each(function() {
                $(this).attr("checked", false);
            });
        });

        $("input[name=Checkbox2]").each(function() {
            $(this).click(function() {
                if (this.checked == true) {
                    var id = $(this).attr("id");

                    $("input[name=Checkbox2]").each(function() {
                        if ($(this).attr("id") != id) {
                            $(this).attr("checked", false);
                        }
                    });
                }
            });
        });

        btn_edit.click(function() {
            var i = 0;
            $("input[name=Checkbox2]").each(function() {
                if (this.checked == true) {
                    i++;
                }
            });
            if (i > 0) {
                showDialogForm();
                $("input[name=Checkbox2]").each(function() {
                    if (this.checked == true) {
                        txtname.val($(this).attr("pid"));
                        hfobjectid.val($(this).attr("id"));
                        var ids = $(this).val().split(',');
                        var names = $(this).attr("uid").split(',');
                        $("#slmembers option").appendTo(slusers);
                        for (var i = 0; i < names.length; i++) {

                            $("#slusers option").each(function() {
                                if ($.trim($(this).val()) == $.trim(ids[i])) {
                                    $(this).appendTo(slmembers);
                                }
                            });
                        }
                    }
                });
                txtname.attr("disabled", "disabled");
            }
            else {
                alert("Please select one item.");
            }
        });

        $("#Checkbox1").click(function() {
            var ids = "";
            if (this.checked == true) {
                $("input[name=Checkbox2]").each(function() {

                    $(this).attr("checked", true);
                    ids = ids + $(this).attr("id") + ",";
                });
            } else {
                $("input[name=Checkbox2]").each(function() {
                    $(this).attr("checked", false);
                });
            }
        });

        addsl.click(function() {

            $("#slusers option:selected").appendTo(slmembers);

        });

        removesl.click(function() {
            $("#slmembers option:selected").appendTo(slusers);
        });


        btn_delete.click(function() {
            var i = 0;
            var ids = "";
            $("input[name=Checkbox2]").each(function() {
                if (this.checked == true) {
                    i++;
                    ids = ids + $(this).attr("id") + ",";

                }
            });
            if (i > 0) {
                $.post('<%=Url.Action("DeleteGroup","admin")%>', { ids: ids }, function(result) {
                    alert(result);
                    reload();
                });

            } else {
                alert("Please select one item.");
            }
        });

        btn_save.click(function() {
            var roleid = hfobjectid.val();
            var name = txtname.val();
            var userids = "";
            $("#slmembers option").each(function() {
                userids = userids + $(this).val() + ",";
            });
            $.post('<%=Url.Action("AddGroup","admin") %>', { roleID: roleid, name: name, userids: userids, random: Math.random() }, function(result) {
                alert(result);
            });
            reload();
        });

        function reload() {
            $.get('<%=Url.Action ("group","admin") %>', { random: Math.random() }, function(result) {

                $("#renderData").html(result);
            });
        }
        btn_cancel.click(function() {
            hideDialogForm();
            txtname.val("");
            hfobjectid.val("0");
            txtname.removeAttr("disabled");
            $("#slmembers option").appendTo(slusers);
        });
        
        
    </script>

</div>
