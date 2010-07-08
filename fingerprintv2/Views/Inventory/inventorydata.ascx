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
        string query = ViewData["query"] as string;
        List<fpcore.Model.Inventory> inventories = ViewData["inventories"] as List<fpcore.Model.Inventory>;
    %>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="delivery_location">
                Inventory → Monitor (WIP)
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <input type="button" class="std_btn" value="New Delivery"  id="btn_new" />
                <input type="button" class="std_btn" value="Update"  id="btn_update" />
                <input type="button" class="std_btn" value="Delete" id="btn_delete" />
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table1">
                    <tr>
                        <td class="delivery_paging">
                            Showing : <%=count %> -
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
                            per page| Order Status
                            <select class="dropdownlist" name="slstatus" id="slstatus" size="1">
                                <%if (string.IsNullOrEmpty(query))
                                  { %><option value="Show All" selected>Show All</option>
                                <%}
                                  else
                                  { %><option value="Show All">Show All</option>
                                <% } if (query == "In Progress")
                                  {%>
                                <option value="In Progress" selected>In Progress</option>
                                <%}
                                  else
                                  { %>
                                <option value="In Progress">In Progress</option>
                                <%} %>
                                <%if (query == "Finished")
                                  { %>
                                <option value="Finished" selected>Finished</option>
                                <%}
                                  else
                                  { %>
                                <option value="Finished">Finished</option>
                                <%} %>
                                <%if (query == "Hold")
                                  { %>
                                <option value="Hold" selected>Hold</option>
                                <%}
                                  else
                                  { %>
                                <option value="Hold" >Hold</option>
                                <%} %>
                            </select>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--list start--%>
    <table border="1" bordercolor="#012038" cellpadding="0" cellspacing="0" width="100%"
        style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
        <tr>
            <td align="center" class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                <input id="Checkbox1" type="checkbox" />
            </td>
            <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                onmouseout="className='inventory_data_dg_header_1'">
                Category
            </td>
            <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                onmouseout="className='inventory_data_dg_header_1'">
                Asset No.
            </td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Asset (Eng)
            </td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Asset (中文)
            </td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Description
            </td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Quantity (Stored)
            </td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                As at
            </td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Remark
            </td>
        </tr>
        <%foreach (var item in inventories)
          { %>
        <tr>
            <td align="center" class="inventory_data_dg_row_alter">
                <input id="<%=item.objectId %>" name="checkbox3" type="checkbox" pid="<%=item.status %>" />
            </td>
            <td class="inventory_data_dg_row_alter">
                <%=item.category %>&nbsp;
            </td>
            <td class="inventory_data_dg_row_alter">
                <a pid="<%=item.objectId %>" name="aobj" style="cursor: pointer; text-decoration: underline;
                    line-height: 22px; color: Blue;">
                    <%=item.assetno %></a>&nbsp;
            </td>
            <td class="inventory_data_dg_row_alter">
                <%=item.productnameen  %>&nbsp;
            </td>
            <td class="inventory_data_dg_row_alter" align="center">
                <%=item.productnamecn %>&nbsp;
            </td>
            <td class="inventory_data_dg_row_alter" align="center">
                <%=item.description %>&nbsp;
            </td>
            <td class="inventory_data_dg_row_alter" align="center">
                <%=item.stored%>&nbsp;
            </td>
            <td class="inventory_data_dg_row_alter" align="center">
                <%=item.receiveddate %>&nbsp;
            </td>
            <td class="inventory_data_dg_row_alter" align="center">
                <%=item.remark %>&nbsp;
            </td>
        </tr>
        <%} %>
    </table>
    <%--list end--%>

    <script type="text/javascript">
        $("#select3").change(function() {
            getSizeData($("#select3").val());
        });

        $("a[name='aobj']").each(function() {
            $(this).click(function() {
                var id = $(this).attr("pid");
                renderNew(id);
            });

        });

        $("#btn_update").click(function() {
        $("input[name=checkbox3]").each(function() {
                if (this.checked == true) {
                    renderNew($(this).attr("id"));
                }
            });
        });

        $("#btn_new").click(function() {
            renderNew("");
        });

        $("#slstatus").change(function() {

            $('#loading').show();
            var status = $("#slstatus").val();
            if (status == "Show All") {
                status = "";
            }
            $('#loading').show();
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = "<%=index %>";
            var size = "<%=size %>";
            $.get('<%=Url.Action ("inventorydata","inventory") %>', { random: Math.random(), query: status, sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                
                $("#deliverydata").html(result);
            });
            $('#loading-one').parent().fadeOut('slow');
        });

        $("input[name=checkbox3]").each(function() {
            $(this).click(function() {
                if (this.checked == true) {
                    var id = $(this).attr("id");
                    var pid = $(this).attr("pid");

                    $("input[name=checkbox3]").each(function() {

                        if ($(this).attr("id") != id) {
                            $(this).attr("checked", false);
                        }
                    });


                }
            });
        });

        function renderData() {

            $("#a_history").css("font-weight", "normal");
            $("#a_new").css("font-weight", "normal");
            $("#a_inventory").css("font-weight", "bold");
            $('#loading').show();
            $.get('<%=Url.Action ("inventorydata","Inventory") %>', { random: Math.random() }, function(result) {

                $("#renderData").html(result);
            });

            $('#loading-one').parent().fadeOut('slow');
        }

        function renderNew(objid) {
            $("#a_history").css("font-weight", "normal");
            $("#a_new").css("font-weight", "bold");
            $("#a_inventory").css("font-weight", "normal");
            $('#loading').show();
            $.get('<%=Url.Action ("New","Inventory") %>', { random: Math.random(), objectid: objid }, function(result) {

                $("#renderData").html(result);
            });

            $('#loading-one').parent().fadeOut('slow');
        }

        function getSizeData(pagesize) {
            $('#loading').show();
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = "<%=index %>";
            var query = "<%=query %>";
            var size = pagesize;
            $.get('<%=Url.Action ("inventorydata","inventory") %>', { random: Math.random(),query:query, sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#deliverydata").html(result);
            });
            $('#loading-one').parent().fadeOut('slow');
        }

        function getData(pageindex) {
            $('#loading').show();
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = pageindex;
            var query = "<%=query %>";
            var size = "<%=size %>";
            $.get('<%=Url.Action ("inventorydata","inventory") %>', { random: Math.random(), query: query, sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#deliverydata").html(result);
            });
            $('#loading-one').parent().fadeOut('slow');
        }
    </script>

</div>
