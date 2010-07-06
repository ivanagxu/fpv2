<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;">
        <tr>
            <td height="100%" rowspan="3" valign="top" class="left_getting_opacity" style="width: 180px">
                <table width="180" height="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="left_pad">
                            <table width="160" height="220" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span id="nav"></span>
                                        <table border="0" bordercolor="#666666" class="left_table" cellpadding="0" cellspacing="0"
                                            height="220" width="160">
                                            <tr>
                                                <td class="leftstyle2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="left_td">
                                                    <div style="padding: 2px 0px 0x 10px">
                                                        -
                                                        <a style="cursor: pointer; font-weight:bold;" class="leftstyle1_2" id="a_inventory">Monitor
                                                        </a>
                                                    </div>
                                                  <%--  <div style="padding: 2px 0px 0x 10px">
                                                        - <a style="cursor: pointer;" class="leftstyle1_2" id="a_history">History 
                                                        </a>
                                                    </div>--%>
                                                    <div style="padding: 2px 0px 0x 10px">
                                                        -
                                                        <a style="cursor: pointer;" class="leftstyle1_2" id="a_new">New Inventory 
                                                        </a>   
                                                    </div>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="left_pad">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <div align="right">
                                            <table class="delivery_filter_header_table" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 500px; overflow: scroll;">
            <td valign="top">
                <div id="renderData">
                    <%Html.RenderAction("inventory", "Inventory"); %>
                </div>
            </td>
        </tr>
        <tr>
            <td class="delivery_getting_opacity" style="width: 100%; height: 31px">
                <br>
            </td>
        </tr>
    </table>
       <script type="text/javascript">
           $("#a_history").click(function() {
           $("#a_history").css("font-weight", "bold");
           $("#a_new").css("font-weight", "normal");
           $("#a_inventory").css("font-weight", "normal");
               $('#loading').show();
               $.get('<%=Url.Action ("History","Inventory") %>', { random: Math.random() }, function(result) {
              
                   $("#renderData").html(result);
               });

               $('#loading-one').parent().fadeOut('slow');
           });
           $("#a_inventory").click(function() {

//           $("#a_history").css("font-weight", "normal");
//           $("#a_new").css("font-weight", "normal");
//           $("#a_inventory").css("font-weight", "bold");
//               $('#loading').show();
//               $.get('<%=Url.Action ("inventory","Inventory") %>', { random: Math.random() }, function(result) {

//                   $("#renderData").html(result);
//               });

//               $('#loading-one').parent().fadeOut('slow');
//           });

           $("#a_new").click(function() {

               $("#a_history").css("font-weight", "normal");
               $("#a_new").css("font-weight", "bold");
               $("#a_inventory").css("font-weight", "normal");
               $('#loading').show();
               $.get('<%=Url.Action ("New","Inventory") %>', { random: Math.random() }, function(result) {

                   $("#renderData").html(result);
               });

               $('#loading-one').parent().fadeOut('slow');
           });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="styleSheetContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scriptContent" runat="server">

 

</asp:Content>