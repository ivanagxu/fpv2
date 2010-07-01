<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="deliverydata" align="center">
    <%
        string sort = ViewData["sortExpression"] as string;
        bool? diretion = ViewData["sortDiretion"] as bool?;
        int? index = ViewData["pageIndex"] as int?;
        int? size = ViewData["pageSize"] as int?;
        int? pageCount = ViewData["pageCount"] as int?;
    %>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="delivery_location">
                Delivery → Monitor (WIP)
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <input type="button" class="std_btn" value="New Delivery" target="mainframe" id="btn_new" />
                <input type="button" class="std_btn" value="Update" target="mainframe" id="btn_update" />
                <input type="button" class="std_btn" value="Delete" target="mainframe" id="btn_delete" />
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table1">
                    <tr>
                        <td class="delivery_paging">
                            Showing : 1 - <%=index %> of <%=pageCount %> 
                            <%if (index ==null || index <=1)
                              {%>
                            <a class="delivery_show_nav">
                                <img border="0" height="16" src="../content/images/image/control_start.gif" style="vertical-align: middle;
                                    border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                    width="16" /></a><a class="delivery_show_nav">
                                        <img height="16" src="../content/images/image/control_back.gif" style="vertical-align: middle;
                                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            width="16" /></a>
                                            <%}else { %>
                                            <a class="delivery_show_nav" style="cursor: pointer;" onclick="getData(1)">
                                <img border="0" height="16" src="../content/images/image/control_start_blue.gif" style="vertical-align: middle;
                                    border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                    width="16" /></a><a style="cursor: pointer;" onclick="getData(<%=index-1%>)">
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
                                             <a class="delivery_show_nav"style="cursor:pointer ;" onclick ="getData(<%=index+1 %>);">
                                                <img height="16" src="../content/images/image/control_play_blue.gif" style="vertical-align: middle;
                                                    border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                    width="16" /></a> <a class="delivery_show_nav" style="cursor:pointer ;" onclick ="getData(<%=pageCount %>);">
                                                        <img height="16" src="../content/images/image/control_end_blue.gif" style="vertical-align: middle;
                                                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                            width="16" />
                                                    </a><%} %>
                                                    
                                                    Show
                            <select class="dropdownlist" name="select3" id ="select3">
                            <%if(size ==25){ %>
                                <option selected="selected" >25</option>
                                <%}else { %>
                                 <option >25</option>
                                <%} %>
                                 <%if(size ==50){ %>
                                <option selected="selected">50</option>
                                <%}else { %>
                                 <option >50</option>
                                <%} %>
                                 <%if(size ==100){ %>
                                <option selected="selected">100</option>
                                <%}else { %>
                                 <option >100</option>
                                <%} %>
                            </select>
                            per page| Order Status
                                    <select class="dropdownlist" name="select5" size="1">
                                        <option>In Progress</option>
                                        <option>Finished</option>
                                        <option>Hold</option>
                                    	<option>Show All</option>
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
                <input id="Checkbox1" type="checkbox" /></td>
            <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                onmouseout="className='inventory_data_dg_header_1'">
                Category</td>
            <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                onmouseout="className='inventory_data_dg_header_1'">
                Asset No.</td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Asset (Eng)</td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Asset (中文)</td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Description</td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Quantity (Stored)</td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                As at</td>
            <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                onmouseover="className='inventory_data_dg_header_1_hover'">
                Remark</td>
        </tr>
        <tr>
            <td align="center" class="inventory_data_dg_row_alter">
                <input id="Checkbox2" type="checkbox" /></td>
            <td class="inventory_data_dg_row_alter">
                LASER PRINT</td>
            <td class="inventory_data_dg_row_alter">
                <a href="http://">L01</a></td>
            <td class="inventory_data_dg_row_alter">
                BA3</td>
            <td class="inventory_data_dg_row_alter"  align="center">
                黑白</td>
            <td class="inventory_data_dg_row_alter"  align="center">
                B&amp;W COPY A3 s/s, 80gsm</td>
            <td class="inventory_data_dg_row_alter" align="center">
                300PCS</td>
            <td class="inventory_data_dg_row_alter" align="center">
                2010-03-12</td>
            <td class="inventory_data_dg_row_alter" align="center">
                　</td>
        </tr>
    </table>
    <%--list end--%>

    <script type="text/javascript">
        $("#select3").change(function() {
            getSizeData($("#select3").val());
        });
    
        function getSizeData(pagesize){
            $('#loading').show();
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = "<%=index %>";
            var size = pagesize;
            $.get('<%=Url.Action ("inventory","Delivery") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#deliverydata").html(result);
            });
            $('#loading-one').parent().fadeOut('slow');
        }
        
        function getData(pageindex) {
            $('#loading').show();
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = pageindex;
            var size = "<%=size %>";
            $.get('<%=Url.Action ("inventory","Delivery") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#deliverydata").html(result);
            });
            $('#loading-one').parent().fadeOut('slow');
        }
    </script>

</div>