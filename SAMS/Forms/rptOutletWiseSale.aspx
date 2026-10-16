<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="rptOutletWiseSale.aspx.cs" Inherits="Forms_rptOutletWiseSale" Title="SAMS :: Catagory Wise Customer Report" %>

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
    }
    </script>

    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 1px" align="left" colspan="1"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="200px" AutoPostBack="True"
                                                OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged"></asp:DropDownList></td>
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
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="48px" Text="Route"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="ddlRoute" runat="server" Width="200px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Width="48px" Text="Market"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="ddlMarket" runat="server" Width="200px"></asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left"></td>
                                        <td style="width: 90px; height: 2px" align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="48px" Text="Catagory" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                        <td style="width: 204px; height: 2px" align="left">
                                            <asp:CheckBox ID="ChbAllCatagories" runat="server" Text="All Catagories"></asp:CheckBox></td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left"></td>
                                        <td></td>
                                        <td></td>
                                        <td style="height: 2px" align="left" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Width="200px" Height="150px" ScrollBars="Vertical" BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="ListCatagory" runat="server" class="checkboxlst"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left"></td>
                                    </tr>

                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="lblTopCustomer" runat="server" Text="Top Customer"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtTopCustomer" runat="server" Width="200px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ftbeTopCustomer" runat="server" FilterType="Custom"
                                                ValidChars="0123456789" TargetControlID="txtTopCustomer">
                                            </cc1:FilteredTextBoxExtender>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                      <tr>
                                    <td align="left">
                                    </td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label8" runat="server" CssClass="lblbox" Text="Order Type" Width="78px"></asp:Label></strong>
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="DrpOrderType" runat="server" Width="200px" CssClass="DropList">
                                             <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                                             <asp:ListItem Value="214">Cash</asp:ListItem>
                                             <asp:ListItem Value="215">Credit</asp:ListItem>
                                             
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="lblSortBy" runat="server" Text="Sort By"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="ddlSortBy" runat="server" Width="200px">
                                                <asp:ListItem Value="1" Text="Quantity"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Value" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="lblOrderBy" runat="server" Text="Order By"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="ddlOrderBy" runat="server" Width="200px">
                                                <asp:ListItem Value="1" Text="Ascending"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Descending" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Height="9px" Text="From Date" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Text="To Date" CssClass="lblbox"></asp:Label></strong></td>
                                        <td style="width: 1px" align="left"></td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" EnableViewState="False" PopupButtonID="ImgBntFromDate" TargetControlID="txtFromDate"></cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" EnableViewState="False" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate"></cc1:CalendarExtender>
                            &nbsp; 
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button"
                        Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>

    </div>
</asp:Content>
