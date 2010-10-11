<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="deliverydata" align="center">
    <%
        string sort = ViewData["sortExpression"] as string;
        bool? diretion = ViewData["sortDiretion"] as bool?;
        bool resort = !diretion.Value;
        int? index = ViewData["pageIndex"] as int?;
        int? size = ViewData["pageSize"] as int?;
        int? pageCount = ViewData["pageCount"] as int?;
        int? count = ViewData["count"] as int?;
        List<fpcore.Model.Delivery> deliveries = ViewData["deliveries"] as List<fpcore.Model.Delivery>;
        List<fpcore.Model.UserAC> users = ViewData["users"] as List<fpcore.Model.UserAC>;
        
    %>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="delivery_location">
                Delivery -> Monitor
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <input type="button" class="std_btn" value="New Delivery" target="mainframe" id="btn_new" />
                <input type="button" class="std_btn" value="Delete" target="mainframe" id="btn_delete" />
                <input type="button" class="std_btn" value="Edit" target="mainframe" id="btn_update" />
            </td>
        </tr>
        <tr>
            <td>
                Delivery Type:
                <select size="1" name="D4" id="sltype">
                    <option value ="Send">Send</option>
                    <option value ="Receive">Receive</option>
                </select>
                Handled by:
                <select size="1" name="D16" id="slusers">
                    <%foreach (var u in users)
                      { %>
                    <option value="<%=u.objectId %>">
                        <%=u.eng_name  %></option>
                    <%} %>
                </select>
                Status:
                <select size="1" name="D10" id="slstatus">
                    <option value ="Pending">Pending</option>
                    <option value ="Processing">Processing</option>
                    <option value ="Finish">Finish</option>
                </select>
                (Change those dropdownlist will change the value which table sub item is selected)
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table1">
                    <tr>
                        <td class="delivery_paging">
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
    </table>
    <%--list start--%>
    <table border="1" bordercolor="#29393A" cellpadding="0" cellspacing="0" width="100%"
        style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
        <tr>
            <td align="center" class="delivery_data_dg_header_1" onmouseout="className='delivery_data_dg_header_1'"
                onmouseover="className='delivery_data_dg_header_1_hover'">
                <input id="Checkbox1" type="checkbox"  disabled =disabled />
            </td>
            <td class="delivery_data_dg_header_1" onmouseover="className='delivery_data_dg_header_1_hover'"
                onmouseout="className='delivery_data_dg_header_1'">
                Delivery No.
            </td>
            <td class="delivery_data_dg_header_1" onmouseout="className='delivery_data_dg_header_1'"
                onmouseover="className='delivery_data_dg_header_1_hover'">
                Customer Name
            </td>
            <td class="delivery_data_dg_header_1" onmouseout="className='delivery_data_dg_header_1'"
                onmouseover="className='delivery_data_dg_header_1_hover'">
                District
            </td>
            <td class="delivery_data_dg_header_1" onmouseout="className='delivery_data_dg_header_1'"
                onmouseover="className='delivery_data_dg_header_1_hover'">
                Delivery Type
            </td>
            <td class="delivery_data_dg_header_1" onmouseout="className='delivery_data_dg_header_1'"
                onmouseover="className='delivery_data_dg_header_1_hover'">
                Date
            </td>
            <td class="delivery_data_dg_header_1" onmouseout="className='delivery_data_dg_header_1'"
                onmouseover="className='delivery_data_dg_header_1_hover'">
                Time
            </td>
            <td class="delivery_data_dg_header_1" onmouseout="className='delivery_data_dg_header_1'"
                onmouseover="className='delivery_data_dg_header_1_hover'">
                Handled by
            </td>
            <td class="delivery_data_dg_header_1" onmouseout="className='delivery_data_dg_header_1'"
                onmouseover="className='delivery_data_dg_header_1_hover'">
                Status
            </td>
        </tr>
        <%foreach (var item in deliveries)
          { %>
        <tr>
            <td align="center" class="delivery_data_dg_row_alter">
                <%string str = item.delivery_type + "," + (item.handled_by != null ? item.handled_by.objectId.ToString() : string.Empty) + "," + item.status + ",";%>
                <input id="<%=item.objectId %>" name="checkbox3" type="checkbox" value="<%=str %>" />
            </td>
            <td class="delivery_data_dg_row_alter">
                <a style="cursor: pointer; text-decoration:underline; color:Blue; line-height:22px;" name="anumber" id="<%=item.objectId %>">
                    <%=string.IsNullOrEmpty (item.number)?"null or  empty":item.number%></a>&nbsp;
            </td>
            <td align="center" class="delivery_data_dg_row_alter">
                <%=item.contact.contact_person %>&nbsp;
            </td>
            <td align="center" class="delivery_data_dg_row_alter">
                <%=item.contact.district %>&nbsp;
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.delivery_type %>&nbsp;
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.createDate.Value.ToShortDateString() %>&nbsp;
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.createDate.Value.ToShortTimeString () %>&nbsp;
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.handled_by == null ? string.Empty : item.handled_by.eng_name%>&nbsp;
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.status %>&nbsp;
            </td>
        </tr>
        <%} %>
    </table>
    <%--list end--%>

    <script type="text/javascript">
        var slstatus = $("#slstatus");
        var sltype = $("#sltype");
        var slusers = $("#slusers");

        $("input[name=checkbox3]").each(function() {
            $(this).click(function() {
                if (this.checked == true) {
                    var id = $(this).attr("id");
                    var pid = $(this).val().split(',');

                    $("input[name=checkbox3]").each(function() {

                        if ($(this).attr("id") != id) {
                            $(this).attr("checked", false);
                        }
                    });

                    $("#sltype option").each(function() {
                        if ($.trim($(this).val().toLowerCase()) == $.trim(pid[0].toLowerCase())) {

                            $(this).attr("selected", "selected");
                        } else {
                            $(this).removeAttr("selected");
                        }
                    });

                    $("#slusers option").each(function() {
                        if ($.trim($(this).val().toLowerCase()) == $.trim(pid[1].toLowerCase())) {

                            $(this).attr("selected", "selected");
                        }
                        else {
                            $(this).removeAttr("selected");
                        }
                    });

                    $("#slstatus option").each(function() {
                        if ($.trim($(this).val().toLowerCase()) == $.trim(pid[2].toLowerCase())) {

                            $(this).attr("selected", "selected");
                        } else {
                            $(this).removeAttr("selected");
                        }
                    });
                }
            });
        });

        sltype.change(function() {
            var ids = "";
            $("input[name=checkbox3]").each(function() {
                if (this.checked == true) {
                    ids = $(this).attr("id");
                }
            });
            if (ids != "") {
                updateData(ids, "type", $(this).val());
            }
            else {
                alert("please select one item!");
            }
        });

        slusers.change(function() {
            var ids = "";
            $("input[name=checkbox3]").each(function() {
                if (this.checked == true) {
                    ids = $(this).attr("id");
                }
            });
            if (ids != "") {
                updateData(ids, "user", $(this).val());
            }
            else {
                alert("please select one item!");
            }
        });

        slstatus.change(function() {
            var ids = "";
            $("input[name=checkbox3]").each(function() {
                if (this.checked == true) {
                    ids = $(this).attr("id");
                }
            });
            if (ids != "") {
                updateData(ids, "status", $(this).val());
            }
            else {
                alert("please select one item!");
            }
        });

        function updateData(id,name,value) {
            $.post('<%=Url.Action("updatedelivery","delivery") %>', { id: id, name: name, value: value }, function(result) {
                alert(result);
                reload();
            });
        }

        $("#btn_delete").click(function() {
            var ids = "";
            $("input[name=checkbox3]").each(function() {
                if (this.checked == true) {
                    ids = $(this).attr("id");
                }
            });
            if (ids != "") {
                if (confirm("Are you sure delete it?")) {
                    $.post('<%=Url.Action("deletedelivery","delivery") %>', { ids: ids }, function(result) {
                        alert(result);
                        reload();
                    });
                }
            }
            else {
                alert("please select one item!");
            }
        });

        $("#btn_update").click(function() {
            var ids = "";
            $("input[name=checkbox3]").each(function() {
                if (this.checked == true) {
                    ids = $(this).attr("id");
                }
            });
            if (ids != "") {
                var id = ids;
                $("#a_archive").css("font-weight", "normal");
                $("#a_new").css("font-weight", "bold");
                $("#a_deliverydata").css("font-weight", "normal");
              
                $.get('<%=Url.Action ("New","Delivery") %>', { random: Math.random(), objectid: parseInt(id) }, function(result) {

                    $("#renderData").html(result);
                });

               
            }
            else {
                alert("please select one item!");
            }
        });

        $("a[name='anumber']").each(function() {
            $(this).click(function() {
                var id = $(this).attr("id");
                $("#a_archive").css("font-weight", "normal");
                $("#a_new").css("font-weight", "bold");
                $("#a_deliverydata").css("font-weight", "normal");
              
                $.get('<%=Url.Action ("New","Delivery") %>', { random: Math.random(), objectid: parseInt(id) }, function(result) {

                    $("#renderData").html(result);
                });

               
            });
        });
        $("#btn_new").click(function() {
            $("#a_archive").css("font-weight", "normal");
            $("#a_new").css("font-weight", "bold");
            $("#a_deliverydata").css("font-weight", "normal");
          
            $.get('<%=Url.Action ("New","Delivery") %>', { random: Math.random() }, function(result) {

                $("#renderData").html(result);
            });

          
        });

        $("#select3").change(function() {
            getSizeData($("#select3").val());
        });

        function reload() {
            
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = "<%=index %>";
            var size = "<%=size %>";
            $.get('<%=Url.Action ("DeliveryData","Delivery") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#deliverydata").html(result);
            });
          
        }

        function getSizeData(pagesize) {
          
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = "<%=index %>";
            var size = pagesize;
            $.get('<%=Url.Action ("DeliveryData","Delivery") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#deliverydata").html(result);
            });
            
        }

        function getData(pageindex) {
           
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = pageindex;
            var size = "<%=size %>";
            $.get('<%=Url.Action ("DeliveryData","Delivery") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#deliverydata").html(result);
            });
            
        }
    </script>

</div>
