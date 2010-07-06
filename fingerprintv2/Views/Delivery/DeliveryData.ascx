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
    %>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="delivery_location">
                Delivery → Monitor
            </td>
        </tr>
        <tr>
            <td class="delivery_location">
                <input type="button" class="std_btn" value="New Delivery" target="mainframe" id="btn_new" />
                
                <input type="button" class="std_btn" value="Delete" target="mainframe" id="btn_delete" />
                <select size="1" name="D4">
                    <option>Send</option>
                    <option>Receive</option>
                </select>
                <select size="1" name="D16">
                    <option selected>Tim</option>
                    <option>Wa</option>
                    <option>Lok</option>
                </select>
                  <select size="1" name="D10">
                    <option>Pending</option>
                </select>
                
                <input type="button" class="std_btn" value="Update" target="mainframe" id="btn_update" />
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
                <input id="Checkbox1" type="checkbox" />
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
                <input id="<%=item.objectId %>" name="checkbox3" type="checkbox" />
            </td>
            <td class="delivery_data_dg_row_alter">
                <a  style ="cursor:pointer ;" name="anumber" id="<%=item.objectId %>">
                    <%=string.IsNullOrEmpty (item.number)?"null or  empty":item.number%></a>
            </td>
            <td align="center" class="delivery_data_dg_row_alter">
                <%=item.contact.contact_person %>
            </td>
            <td align="center" class="delivery_data_dg_row_alter">
                Kwonloon
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.delivery_type %>
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.createDate.Value.ToShortDateString() %>
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.createDate.Value.ToShortTimeString () %>
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.handled_by == null ? string.Empty : item.handled_by.eng_name%>
            </td>
            <td class="delivery_data_dg_row_alter" align="center">
                <%=item.status %>
            </td>
        </tr>
        <%} %>        
    </table>
    <%--list end--%>

    <script type="text/javascript">

        $("a[name='anumber']").each(function() {
            $(this).click(function() {
                var id = $(this).attr("id");
                $("#a_archive").css("font-weight", "normal");
                $("#a_new").css("font-weight", "bold");
                $("#a_deliverydata").css("font-weight", "normal");
                $('#loading').show();
                $.get('<%=Url.Action ("New","Delivery") %>', { random: Math.random(), objectid:parseInt (id) }, function(result) {

                    $("#renderData").html(result);
                });

                $('#loading-one').parent().fadeOut('slow');
            });
        });
        $("#btn_new").click(function() {
            $("#a_archive").css("font-weight", "normal");
            $("#a_new").css("font-weight", "bold");
            $("#a_deliverydata").css("font-weight", "normal");
            $('#loading').show();
            $.get('<%=Url.Action ("New","Delivery") %>', { random: Math.random() }, function(result) {

                $("#renderData").html(result);
            });

            $('#loading-one').parent().fadeOut('slow');
        });
    
        $("#select3").change(function() {
            getSizeData($("#select3").val());
        });

        function getSizeData(pagesize) {
            $('#loading').show();
            var sort = "<%=sort %>";
            var diretion = "<%=diretion %>";
            var index = "<%=index %>";
            var size = pagesize;
            $.get('<%=Url.Action ("DeliveryData","Delivery") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
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
            $.get('<%=Url.Action ("DeliveryData","Delivery") %>', { random: Math.random(), sortExpression: sort, sortDiretion: diretion, pageIndex: index, pageSize: size }, function(result) {
                $("#deliverydata").html(result);
            });
            $('#loading-one').parent().fadeOut('slow');
        }
    </script>

</div>
