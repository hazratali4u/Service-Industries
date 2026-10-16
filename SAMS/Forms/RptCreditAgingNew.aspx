<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptCreditAgingNew.aspx.cs" Inherits="Forms_RptCreditAgingNew" Title="SAMS :: Credit Aging Report New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {

            return true;
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
                                        <td align="left"></td>
                                        <td valign="top" align="left" colspan="3">
                                            <asp:RadioButtonList ID="rblReportBehaviour" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblReportBehaviour_SelectedIndexChanged">
                                            <asp:ListItem Text="Detail" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Summary" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td valign="top" align="left" colspan="3">
                                            <asp:RadioButtonList ID="rbtSotBy" runat="server" Width="301px" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtSotBy_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Selected="True" Value="0">Principal Wise</asp:ListItem>
                                                <asp:ListItem Value="1">Customer Wise</asp:ListItem>
                                                <asp:ListItem>Date Wise</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="78px" Text="Principal" CssClass="lblbox"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="79px" Text="Sale Force"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlSaleForce" runat="server" Width="200px" ></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lblChannelType" runat="server" Width="79px" Text="Channel Type" CssClass="lblbox" __designer:wfdid="w8"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpChannelType" runat="server" Width="200px" CssClass="DropList" __designer:wfdid="w7"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lblRoute" runat="server" Width="79px" Text="Route" Visible="false"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlRoute" runat="server" Width="200px" Visible="false" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lblMarket" runat="server" Width="79px" Text="Market" Visible="false"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="ddlMarket" runat="server" Width="200px" Visible="false"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" Visible="false"></asp:Label>
                                        </strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px" Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblSortBy" runat="server" Text="Sort By" Visible="false"></asp:Label>
                                        </strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="ddlSortBy" runat="server" Width="200px" Visible="false">
                                            <asp:ListItem Value="1" Text="Customer"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Amount"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Days"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Invoice Date"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"></td>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblOrderBy" runat="server" Text="Order By" Visible="false"></asp:Label>
                                        </strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:DropDownList ID="ddlOrderBy" runat="server" Width="200px" Visible="false">
                                            <asp:ListItem Value="1" Text="Ascending"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Descending"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lblStartDate" runat="server" Visible="False" Width="70px" Height="13px" Text="Start Date"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:TextBox ID="txtDocmentDate" onkeyup="BlockStartDateKeyPress()" runat="server" Visible="False" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox><asp:ImageButton ID="ibtnStartDate" runat="server" Visible="False" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lblEndDate" runat="server" Width="80px" Height="13px" Text="End Date"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:TextBox ID="txtEndDate" runat="server" Width="149px" CssClass="txtBox " MaxLength="10"></asp:TextBox><asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton></td>
                                    </tr>
                                      <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="110px" Height="13px" Text="Select Duration"></asp:Label></strong></td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:CheckBox ID="cbONetorthirty" runat="server" Checked="true" Text="1 to 30" />
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:CheckBox ID="cb31to45" runat="server" Checked="true" Text="31 to 45" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="cb46to75" runat="server" Checked="true" Text="46 to 75" />
                                            </td>
                                    </tr>
                                    <tr> <td align="left"></td>
                                        <td align="left"></td>
                                          <td align="left"></td>
                                        <td > 
                                            <asp:CheckBox ID="cb76to90" runat="server" Checked="true" Text="76 to 90" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="cbabove90" runat="server" Checked="true" Text="Above 90" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtDocmentDate" PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button"
                        Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button"
                        Text="View Excel" Width="90" OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
