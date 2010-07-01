<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div align="center">
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
        <tr>
            <td class="inventory_location">
                Inventory → New Inventory
            </td>
        </tr>
        <tr>
            <td class="inventory_location">
            </td>
        </tr>
        <tr>
            <td class="inventory_location">
                <table border="0" cellpadding="2" cellspacing="0" width="100%" class="inventory_request_table">
                    <tr>
                        <td class="inventory_request_title" colspan="4">
                            Part I - Product Information
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Order No :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" id="order_no" name="T16" size="20">
                        </td>
                        <td class="inventory_request_column_header">
                        </td>
                        <td class="inventory_request_cell">
                            <input type="radio" value="V1" checked name="R1">Order&nbsp; By Customer Name
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Received Date :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" id="recd_date" name="T14" size="20">
                        </td>
                        <td class="inventory_request_column_header">
                        </td>
                        <td class="inventory_request_cell">
                            <input type="radio" value="V1" checked name="R1">Search By Keyword
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Received By :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" id="recd_by" name="T15" size="20">
                        </td>
                        <td class="inventory_request_column_header">
                        </td>
                        <td class="inventory_request_cell">
                            <input type="radio" value="V1" checked name="R1">Customer Not in List
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Order Deadline :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" name="T17" size="20">
                            <img border="0" class="sel_button" src="../content/images/image/calendar-icon.png" alt="Calendar" width="32"
                                height="32">
                        </td>
                        <td class="inventory_request_column_header" valign="middle" align="right">
                            Search By Customer Name:
                        </td>
                        <td class="inventory_request_cell" valign="middle">
                            <input type="text" name="T18" size="20" id="TextboxRef">
                            <input type="button" class="std_btn" value="Search" target="mainframe" id="btn_search">
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Remarks :
                        </td>
                        <td class="inventory_request_cell" rowspan="3">
                            <textarea rows="8" name="S1" cols="36"></textarea>
                        </td>
                        <td class="inventory_request_column_header" align="right">
                            Tel :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" name="T19" size="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                        </td>
                        <td class="inventory_request_column_header" height="20" align="right">
                            Contact Person :
                        </td>
                        <td class="inventory_request_cell" height="20">
                            <input type="text" name="T20" size="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                        </td>
                        <td class="inventory_request_column_header" height="20">
                        </td>
                        <td class="inventory_request_cell" height="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="inventory_request_column_header" align="right">
                            Updated By :
                        </td>
                        <td class="inventory_request_cell">
                            <input type="text" class="txt_displayOnly" id="" name="T15" size="20" value="byip">
                        </td>
                        <td class="inventory_request_column_header">
                        </td>
                        <td class="inventory_request_cell">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="inventory_location">
                <table border="0" width="100%" id="table1" cellspacing="0" cellpadding="2" class="inventory_request_table">
                    <tr>
                        <td class="inventory_request_title">
                            Part II - Consumption History
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="1" bordercolor="#012038" cellpadding="0" cellspacing="0" width="100%"
                                style="padding-right: 1px; padding-left: 1px; padding-bottom: 1px; padding-top: 1px">
                                <tr>
                                    <td class="inventory_data_dg_header_1" onmouseout="className='inventory_data_dg_header_1'"
                                        onmouseover="className='inventory_data_dg_header_1_hover'">
                                        <p align="center">
                                            <input id="Checkbox4" type="checkbox" name="C3" value="ON" />
                                    </td>
                                    <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                                        onmouseout="className='inventory_data_dg_header_1'">
                                        Quantity (Total)
                                    </td>
                                    <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                                        onmouseout="className='inventory_data_dg_header_1'">
                                        Quantity (Store)
                                    </td>
                                    <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                                        onmouseout="className='inventory_data_dg_header_1'">
                                        Quantity (Used)
                                    </td>
                                    <td class="inventory_data_dg_header_1" onmouseover="className='inventory_data_dg_header_1_hover'"
                                        onmouseout="className='inventory_data_dg_header_1'">
                                        As at
                                    </td>
                                </tr>
                                <tr>
                                    <td class="inventory_data_dg_row_alter" align="center">
                                        <input id="Checkbox2" type="checkbox" name="C1" value="ON" />
                                    </td>
                                    <td class="inventory_data_dg_row_alter">
                                        100
                                    </td>
                                    <td class="inventory_data_dg_row_alter">
                                        80
                                    </td>
                                    <td class="inventory_data_dg_row_alter">
                                        20
                                    </td>
                                    <td class="inventory_data_dg_row_alter">
                                        2010-03-11
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="inventory_data_dg_row">
                                        <input id="Checkbox3" type="checkbox" name="C2" value="ON" />
                                    </td>
                                    <td class="inventory_data_dg_row">
                                        100
                                    </td>
                                    <td class="inventory_data_dg_row">
                                        160
                                    </td>
                                    <td class="inventory_data_dg_row">
                                        40
                                    </td>
                                    <td class="inventory_data_dg_row">
                                        2010-01-23
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <tr>
                            <td>
                                <input type="button" class="std_btn" value="New" target="mainframe" id="btn_new_parts">
                                <input type="button" class="std_btn" value="Edit" target="mainframe" id="btn_new_parts0">
                                <input type="button" class="std_btn" value="Delete" target="mainframe" id="btn_new_parts1"><table
                                    border="0" cellpadding="2" cellspacing="0" width="100%" class="inventory_request_table">
                                    <br>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                            Quantity (Total) :
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" checked name="R1">PCS
                                            <input type="text" id="order_no" name="T16" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" name="R1">MM
                                            <input type="text" id="order_no0" name="T21" size="10">
                                            x
                                            <input type="text" id="order_no1" name="T22" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                            Quantity (Store) :
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" checked name="R2">PCS
                                            <input type="text" id="order_no2" name="T23" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" name="R2">MM
                                            <input type="text" id="order_no6" name="T28" size="10">
                                            x
                                            <input type="text" id="order_no7" name="T29" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                            Quantity (Used) :
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" checked name="R3">PCS
                                            <input type="text" id="order_no5" name="T26" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="radio" value="V1" name="R3">MM
                                            <input type="text" id="order_no8" name="T30" size="10">
                                            x
                                            <input type="text" id="order_no9" name="T31" size="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                            Date :
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="text" name="T27" size="20" id="TextboxRef">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="inventory_request_column_header" align="right">
                                        </td>
                                        <td class="inventory_request_cell">
                                            <input type="button" class="std_btn" value="Save" target="mainframe" id="btn_save">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
