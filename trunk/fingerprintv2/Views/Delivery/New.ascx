<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div align="center">
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
        <tr>
            <td class="delivery_location">
                Delivery → New Delivery
            </td>
        </tr>
        <tr>
            <td align="left" nowrap="nowrap" style="height: 35px">
                <input type="button" class="std_btn" value="Confirm" target="mainframe" />
                <input type="button" class="std_btn" value="Cancel" target="mainframe" />
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
                            <input type="text" name="T1" size="20">
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Part No. (Optional)
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="T2" size="20">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Non-Order (Pls. justify)
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <input type="text" name="T21" size="20">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Dimension :<img border="0" src="../content/images/image/calculator32.png" width="32"
                                height="32" class="sel_button">
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            L
                            <input type="text" name="T14" size="20">
                            cm x&nbsp; W
                            <input type="text" name="T17" size="20">
                            cm x H&nbsp;
                            <input type="text" name="T18" size="20">
                            cm
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Weight :
                        </td>
                        <td class="delivery_request_cell" colspan="3">
                            <input type="text" name="T16" size="20">
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
                            <select size="1" name="D4">
                                <option>-</option>
                                <option>00001</option>
                            </select>
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
                            <input type="text" name="T22" size="20">
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
                            <input type="text" name="T3" size="20">
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
                            <input type="text" name="T6" size="20">
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
                            <input type="text" name="T19" size="20">
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
                            <input type="text" name="T7" size="20">
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
                            &nbsp;<select size="1" name="D6">
                                <option selected>HK</option>
                                <option>Kln.</option>
                                <option>NT.</option>
                            </select><input type="radio" value="V1" name="R14" checked>Other
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
                            <input type="text" name="T10" size="20">
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Tel.
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="T12" size="20">
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
                            <input type="text" name="T13" size="20">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header">
                            Remarks
                        </td>
                        <td class="delivery_request_cell">
                            <textarea rows="6" name="S1" cols="45"></textarea>
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
                            <input type="text" name="T25" size="20">
                        </td>
                        <td align="right" class="delivery_request_column_header">
                            Deadline :
                        </td>
                        <td class="delivery_request_cell" >
                            <input type="text" name="T23" size="20">
                            <img border="0" class="sel_button" src="../content/images/image/calendar-icon.png"
                                alt="Calendar" width="32" height="32">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="delivery_request_column_header" style ="width:100px;">
                            Handled By:
                        </td>
                        <td class="delivery_request_cell">
                            <input type="text" name="T22" size="20">
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
                            <textarea rows="6" name="S2" cols="45"></textarea>
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
                            <input type="text" name="T6" size="20">
                            on
                            <input type="text" name="T24" size="20">
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
</div>
