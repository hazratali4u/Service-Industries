<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptPartyWiseSale.aspx.cs" Inherits="RptPartyWiseSale" Title="SAMS :: Discount Slab Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ID" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            var $checkBoxAll = $('[id$="ChbAllCatagories"]'); // assign main checkbox into a variable
            var $ChkBoxlst = $("table.checkboxlst input:checkbox"); // assign all checkboxes in to a variable
            $checkBoxAll.click(function () { // trigger click event when main checkbox will be clicked
                $ChkBoxlst
                .attr('checked', $checkBoxAll // attr() method checks all checkboxes
                .is(':checked'));
            });
            $ChkBoxlst.click( // trigger an event click
            function (e) {
                if (!$(this)[0].checked) { // check if all checkboxes is checked
                    $checkBoxAll.attr("checked", false); // un check the all checkbxes using attr() method, passing false as a second parameter
                }
            });

            jQuery('#<%=txtFromDate.ClientID %>').keydown(MonthTextBox);
            jQuery('#<%=txtToDate.ClientID %>').keydown(MonthTextBox);
        }

        function MonthTextBox(event) {
            event.preventDefault();
        }</script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged"></asp:DropDownList></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Width="48px" Text="Principal" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr >
                                        <td style="height: 25px" align="left"></td>
                                        <td style="width: 90px; height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="48px" Text="Catagory" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="drpCategory" runat="server" Width="200px" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList> </td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left"></td>
                                        <td style="width: 90px; height: 2px" align="left">
                                            <strong>
                                                <asp:Label ID="Label8" runat="server" Width="48px" Text="Skus " CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                        <td style="width: 204px; height: 2px" align="left">
                                            <asp:DropDownList ID="DrpSkus" runat="server" Width="200px"></asp:DropDownList> </td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Route" Width="52px"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="DrpRoute" runat="server" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="true" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Market" Width="52px"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="DrpMarket" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="100px" Height="9px" Text="From Month"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="55px" Text="To Month"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" EnableViewState="False" PopupButtonID="ImgBntFromDate" TargetControlID="txtFromDate"
                                Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate"
                                Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                            &nbsp; 
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button"
                        Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" />

              
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
