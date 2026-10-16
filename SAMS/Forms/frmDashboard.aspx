<%@ Page Title="SAMS: Dashboard" Language="C#" MasterPageFile="~/Forms/DashboardMaster.master" AutoEventWireup="true" CodeFile="frmDashboard.aspx.cs" Inherits="Forms_frmDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCopy" runat="Server">
    <style>
        .ajax__calendar .ajax__calendar_container {
            border: 1px solid #646464;
            background-color: #ffffff;
            color: #000000;
            z-index: 100;
        }
    </style>
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
            //find the current popup
            var popUp = $find('ModelPopup');

            //check it exists so the script won't fail
            if (popUp) {
                //Add the function below as the event
                popUp.add_hidden(HidePopupPanel);
            }
        }

        function HidePopupPanel(source, args) {
            //find the panel associated with the extender
            objPanel = document.getElementById(source._PopupControlID);

            //check the panel exists
            if (objPanel) {
                //set the display attribute, so it remains hidden on postback
                objPanel.style.display = 'none';
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
    }
    </script>
    <div id="right_data">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                        Width="31px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1"
            UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="timerDashboard" runat="server"
                    Interval="90000" OnTick="timerDashboard_Tick">
                </asp:Timer>
                <table width="100%">
                    <tr>
                        <td style="width: 15%">
                            <strong>
                                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                            </strong>
                            <br />
                            <asp:TextBox ID="txtDate" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                            <asp:ImageButton ID="ibDate" runat="server"
                                ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                            <cc1:CalendarExtender ID="ceDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibDate"
                                TargetControlID="txtDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 17%">
                            <strong>
                                <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label>
                            </strong>
                            <br />
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="200px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 17%">
                            <strong>
                                <asp:Label ID="lblZone" runat="server" Text="Division"></asp:Label>
                            </strong>
                            <br />
                            <asp:DropDownList ID="ddlZone" runat="server" Width="200px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 17%">
                            <strong>
                                <asp:Label ID="lblTerritory" runat="server" Text="District"></asp:Label>
                            </strong>
                            <br />
                            <asp:DropDownList ID="ddlTerritory" runat="server" Width="200px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 17%">
                            <strong>
                                <asp:Label ID="lblLocation" runat="server" Text="Distributor"></asp:Label>
                            </strong>
                            <br />
                            <asp:DropDownList ID="ddlLocation" runat="server" Width="200px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 17%">
                            <strong>
                                <asp:Label ID="lblLOB" runat="server" Text="Principal"></asp:Label>
                            </strong>
                            <br />
                            <asp:DropDownList ID="ddlPrincipal" runat="server" Width="200px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlPrincipal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        </td>
                    </tr>
                </table>
                <div style="background-color: #006699; height: 30px; text-align: center">
                    <marquee onmouseover="this.stop();" onmouseout="this.start();" SCROLLDELAY="150">
<asp:Label ID="lblMarqueeText" runat="server" Font-Bold="True" Font-Names="Arial, Helvetica, sans-serif" font-size="16px"
ForeColor="#FFFFFF"  Text="News:"></asp:Label>
</marquee>
                </div>
                <div class="gray-bg2">
                    <div class="blocks">
                        <div class="thumb">
                            <div class="icon">
                                <img src="../images/Primery-sale.png" />
                            </div>
                            <div class="text-red">
                                <div class="red">
                                    <asp:Label ID="lblPrimarySales2" runat="server" Text="0.00"></asp:Label>
                                </div>

                                <div class="gray">Primary Sales</div>

                            </div>
                        </div>

                        <div class="thumb-2">
                            <div class="icon">
                                <img src="../images/sec.png" />
                            </div>
                            <div class="text-red">
                                <div class="blue">
                                    <asp:Label ID="lblGrossSale2" runat="server" Text="0.00"></asp:Label>
                                </div>
                                <div class="gray">Secondary Sales(Gross)</div>
                            </div>
                        </div>

                        <div class="thumb-3">
                            <div class="icon">
                                <img src="../images/in.png" />
                            </div>
                            <div class="text-red">
                                <div class="brown">
                                    <asp:Label ID="lblInventory2" runat="server" Text="0.00"></asp:Label>
                                </div>
                                <div class="gray">Inventory (Qty)</div>
                            </div>
                        </div>

                        <div class="thumb-4">
                            <div class="icon">
                                <img src="../images/book.png" />
                            </div>
                            <div class="text-red">
                                <div class="green">
                                    <asp:Label ID="lblBooking2" runat="server" Text="0.00"></asp:Label>
                                </div>
                                <div class="gray">Order Booking</div>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
                <div id="section-3">
                    <div class="blocks">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 49%;" valign="top">
                                    <div class="block-01">
                                        <div class="head-icon">
                                            <img src="../images/sales-icon.png" />
                                        </div>
                                        <div class="heading">Total Primary Sales (Current Month)</div>
                                        <div class="three">
                                            <div class="one">
                                                <div style="margin-bottom: 2px;">
                                                    <div class="sales-head">SALES</div>
                                                    <div class="sales-icon3">
                                                        <img src="../images/sales-icon2.png" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Principal</div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Value</div>
                                            </div>
                                            <asp:Repeater ID="rPrimarySales" runat="server">
                                                <ItemTemplate>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text"><%# Eval("Principal")%></div>
                                                        </div>
                                                    </div>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text">
                                                                <%# DataBinder.Eval(Container.DataItem, "GrossSale", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        Total
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lblTotalPrimarySales" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 1%;"></td>
                                <td style="width: 49%;" valign="top">
                                    <td style="width: 49%;" valign="top">
                                        <div class="block-01">
                                            <div class="head-icon">
                                                <img src="../images/sales-icon.png" />
                                            </div>
                                            <div class="heading">Total Secondary Sales (Current Month)</div>
                                            <div class="three">
                                                <div class="one">
                                                    <div style="margin-bottom: 10px;">
                                                        <div class="sales-head">SALES</div>
                                                        <div class="sales-icon3">
                                                            <img src="../images/S.Sale.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="two">
                                                    <div class="Value-head">Principal</div>
                                                </div>
                                                <div class="two">
                                                    <div class="Value-head">Value</div>
                                                </div>
                                                <asp:Repeater ID="rSecondarySales" runat="server">
                                                    <ItemTemplate>
                                                        <div class="two">
                                                            <div class="Value-sub">
                                                                <div class="Value-text"><%# Eval("Principal")%></div>
                                                            </div>
                                                        </div>
                                                        <div class="two">
                                                            <div class="Value-sub">
                                                                <div class="Value-text">
                                                                    <%# DataBinder.Eval(Container.DataItem, "GrossSale", "{0:0,00}")%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <div class="two">
                                                    <div class="Value-sub">
                                                        <div class="total-text" style="margin-top: 3px;">
                                                            Total
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="two">
                                                    <div class="Value-sub">
                                                        <div class="total-text" style="margin-top: 3px;">
                                                            <asp:Label ID="lblTotalSecondarysales" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </td>
                                <td style="width: 1%;"></td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 49%;" valign="top">
                                    <div class="block-01">
                                        <div class="head-icon">
                                            <img src="../images/sales-icon.png" />
                                        </div>
                                        <div class="heading">Total Recovery (Current Month)</div>
                                        <div class="three">
                                            <div class="one">
                                                <div style="margin-bottom: 2px;">
                                                    <div class="sales-head">Recovery</div>
                                                    <div class="sales-icon3">
                                                        <img src="../images/cash.recovery.png" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Principal</div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Value</div>
                                            </div>
                                            <asp:Repeater ID="rRecovery" runat="server">
                                                <ItemTemplate>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text"><%# Eval("Principal")%></div>
                                                        </div>
                                                    </div>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text">
                                                                <%# DataBinder.Eval(Container.DataItem, "value", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        Total
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lbltotalRecovery" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 1%;"></td>
                                <td style="width: 49%;" valign="top">
                                    <td style="width: 49%;" valign="top">
                                        <div class="block-01">
                                            <div class="head-icon">
                                                <img src="../images/sales-icon.png" />
                                            </div>
                                            <div class="heading">Total Expense (Current Month)</div>
                                            <div class="three">
                                                <div class="one">
                                                    <div style="margin-bottom: 10px;">
                                                        <div class="sales-head">Expense</div>
                                                        <div class="sales-icon3">
                                                            <img src="../images/expenses.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="two">
                                                    <div class="Value-head">Principal</div>
                                                </div>
                                                <div class="two">
                                                    <div class="Value-head">Value</div>
                                                </div>
                                                <asp:Repeater ID="rExpense" runat="server">
                                                    <ItemTemplate>
                                                        <div class="two">
                                                            <div class="Value-sub">
                                                                <div class="Value-text"><%# Eval("Principal")%></div>
                                                            </div>
                                                        </div>
                                                        <div class="two">
                                                            <div class="Value-sub">
                                                                <div class="Value-text">
                                                                    <%# DataBinder.Eval(Container.DataItem, "value", "{0:0,00}")%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <div class="two">
                                                    <div class="Value-sub">
                                                        <div class="total-text" style="margin-top: 3px;">
                                                            Total
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="two">
                                                    <div class="Value-sub">
                                                        <div class="total-text" style="margin-top: 3px;">
                                                            <asp:Label ID="lblTotalExpense" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </td>
                                <td style="width: 1%;"></td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 49%;" valign="top">
                                    <div class="block-01">
                                        <div class="head-icon">
                                            <img src="../images/sales-icon.png" />
                                        </div>
                                        <div class="heading">Market Credit</div>
                                        <div class="three">
                                            <div class="one">
                                                <div style="margin-bottom: 2px;">
                                                    <div class="sales-head">Credit</div>
                                                    <div class="sales-icon3">
                                                        <img src="../images/market-credit.png" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Principal</div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Value</div>
                                            </div>
                                            <asp:Repeater ID="rMarketCredit" runat="server">
                                                <ItemTemplate>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text"><%# Eval("Principal")%></div>
                                                        </div>
                                                    </div>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text">
                                                                <%# DataBinder.Eval(Container.DataItem, "value", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        Total
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lblTotalMarketCredit" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 1%;"></td>
                                <td style="width: 49%;" valign="top">
                                    <div class="block-01">
                                        <div class="head-icon">
                                            <img src="../images/sales-icon.png" />
                                        </div>
                                        <div class="heading">Total SKU's</div>
                                        <div class="three">
                                            <div class="one">
                                                <div style="margin-bottom: 2px;">
                                                    <div class="sales-head">Counter</div>
                                                    <div class="sales-icon3">
                                                        <img src="../images/counter.png" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Principal</div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Skus</div>
                                            </div>
                                            <asp:Repeater ID="rSkusCounter" runat="server">
                                                <ItemTemplate>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text"><%# Eval("Principal")%></div>
                                                        </div>
                                                    </div>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text">
                                                                <%# DataBinder.Eval(Container.DataItem, "value", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        Total
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lblTotalSkuscounter" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 1%;"></td>
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 49%;" valign="top">
                                    <div class="block-01">
                                        <div class="head-icon">
                                            <img src="../images/sales-icon.png" />
                                        </div>
                                        <div class="heading">Last 5 Month Gross Sales</div>
                                        <div class="three">
                                            <div class="one">
                                                <div style="margin-bottom: 80px;">
                                                    <div class="sales-head">SALES</div>
                                                    <div class="sales-icon">
                                                        <img src="../images/sales-icon2.png" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Month</div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-head">Value</div>
                                            </div>
                                            <asp:Repeater ID="rSales" runat="server">
                                                <ItemTemplate>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text"><%# Eval("MonthName")%></div>
                                                        </div>
                                                    </div>
                                                    <div class="two">
                                                        <div class="Value-sub">
                                                            <div class="Value-text">
                                                                <%# DataBinder.Eval(Container.DataItem, "GrossSale", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        Total
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="two">
                                                <div class="Value-sub">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lblTotalPakValue" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 1%;"></td>
                                <td style="width: 49%;" valign="top">
                                    <div class="block-02">
                                        <div class="head-icon">
                                            <img src="../images/user.png" />
                                        </div>
                                        <div class="heading">Top 5 Distributors</div>
                                        <div class="three">

                                            <!-----------Header---------->
                                            <div class="top-10-distributer-header">

                                                <div class="two2">
                                                    <div class="Dis-head">Distributors</div>
                                                </div>
                                                <div class="two2">
                                                    <div class="Dis-head">MTD Sales Current Month</div>

                                                </div>
                                                <div class="two2">
                                                    <div class="Dis-head">MTD Sales Last Month</div>

                                                </div>

                                                <div class="Value-sub">
                                                    <div class="Value-text2">&nbsp;</div>
                                                </div>
                                                <div class="value-div">
                                                    <div class="Value-text2" style="color: #006699 !important; font-weight: bold;">Value</div>
                                                </div>
                                                <div class="value-div">
                                                    <div class="Value-text2" style="color: #006699 !important; font-weight: bold;">Value/DSR</div>
                                                </div>
                                                <div class="value-div">
                                                    <div class="Value-text2"  style="color: #006699 !important; font-weight: bold;"></div>
                                                </div>
                                                <div class="value-div">
                                                    <div class="Value-text2" style="color: #006699 !important; font-weight: bold;">Value</div>
                                                </div>
                                                <div class="ton-div">
                                                    <div class="Value-text2" style="color: #006699 !important; font-weight: bold;">Qty</div>

                                                </div>

                                            </div>
                                            <!-----------Header---------->

                                            <asp:Repeater ID="rDistributors" runat="server">
                                                <ItemTemplate>
                                                    <div class="two2">
                                                        <div class="Value-sub">
                                                            <div class="Value-text2"><%# Eval("DISTRIBUTOR_NAME")%></div>
                                                        </div>
                                                    </div>

                                                    <div class="two2">
                                                        <div class="value-div">
                                                            <div class="Value-text2">
                                                                <%# DataBinder.Eval(Container.DataItem, "CurrentSaelesValue", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                        <div class="value-div">
                                                            <div class="Value-text2">
                                                                <%# DataBinder.Eval(Container.DataItem, "ValuePerDSR", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                          <div class="value-div">
                                                            <div class="Value-text2"></div>
                                                        </div>
                                                        
                                                    </div>

                                                    <div class="two2">
                                                        <div class="value-div">
                                                            <div class="Value-text2">
                                                                <%# DataBinder.Eval(Container.DataItem, "PreviousSaelesValue", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                        <div class="ton-div">
                                                            <div class="Value-text2">
                                                                <%# DataBinder.Eval(Container.DataItem, "PreviousSaelesTon", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <div class="two2">
                                                <div class="value-div">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        Total
                                                    </div>
                                                </div>
                                                <div class="ton-div">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="two2">
                                                <div class="value-div val-text">
                                                    <div class="total-text t-val-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lblTotalCurrentValue" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="value-div">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lblValuePerDSR" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="ton-div">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label Visible="false" ID="lblTotalCurrentTon" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="two2">
                                                <div class="value-div">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lblTotalPreviousValue" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="ton-div">
                                                    <div class="total-text" style="margin-top: 3px;">
                                                        <asp:Label ID="lblTotalPreviousTon" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </td>
                                <td style="width: 1%;"></td>
                            </tr>
                        </table>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="clear"></div>
                <div id="section-4">
                    <div class="blocks">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 49%;" valign="top">
                                    <div class="block-01">
                                        <div class="head-icon">
                                            <img src="../images/DSR.png" />
                                        </div>
                                        <div class="heading">Top 5 DSR </div>

                                        <div class="three">

                                            <div class="thumb-62">
                                                <div class="icon-n">
                                                    <img src="../images/name-icon.png" />
                                                </div>
                                                <div class="text-blue">Name</div>
                                            </div>
                                            <div class="thumb-62">
                                                <div class="icon-n">
                                                    <img src="../images/dis.png" />
                                                </div>
                                                <div class="text-green">Distributor</div>
                                            </div>
                                            <div class="thumb-62">
                                                <div class="icon-n">
                                                    <img src="../images/bill.png" />
                                                </div>
                                                <div class="text-pur">No of Bill</div>
                                            </div>
                                            <div class="thumb-62">
                                                <div class="icon-n">
                                                    <img src="../images/br.png" />
                                                </div>
                                                <div class="text-brown">Sales Value</div>
                                            </div>
                                            <div class="thumb-62">
                                                <div class="icon-n">
                                                    <img src="../images/ton.png" />
                                                </div>
                                                <div class="text-dbrown">Sales Qty</div>
                                            </div>

                                            <asp:Repeater ID="rDSR" runat="server">
                                                <ItemTemplate>
                                                    <div class="thumb-62">
                                                        <div class="thumb-72">
                                                            <div class=" sku-textsmal">
                                                                <%# Eval("USER_NAME")%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="thumb-62">
                                                        <div class="thumb-72">
                                                            <div class=" sku-textsmal">
                                                                <%# Eval("DISTRIBUTOR_NAME")%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="thumb-62">
                                                        <div class="thumb-72">
                                                            <div class=" sku-text">
                                                                <%# DataBinder.Eval(Container.DataItem, "NoOfBill", "{0:0}")%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="thumb-62">
                                                        <div class="thumb-72">
                                                            <div class=" sku-text">
                                                                <%# DataBinder.Eval(Container.DataItem, "SalesValue", "{0:0,00}")%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="thumb-62">
                                                        <div class="thumb-72">
                                                            <div class=" sku-text">
                                                                <%# DataBinder.Eval(Container.DataItem, "SalesTon", "{0:0}")%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <div class="thumb-62">
                                                <div class="thumb-72">
                                                    <div class="thumb-82">
                                                        <div class="sku-tot">&nbsp;</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="thumb-62">
                                                <div class="thumb-72">
                                                    <div class="thumb-82">
                                                        <div class="sku-tot">Total</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="thumb-62">
                                                <div class="thumb-72">
                                                    <div class="thumb-82">
                                                        <div class="sku-tot">
                                                            <asp:Label ID="lblTotalBill" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="thumb-62">
                                                <div class="thumb-72">
                                                    <div class="thumb-82">
                                                        <div class="sku-tot">
                                                            <asp:Label ID="lblTotalDSRValue" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="thumb-62">
                                                <div class="thumb-72">
                                                    <div class="thumb-82">
                                                        <div class="sku-tot">
                                                            <asp:Label ID="lblTotalDSRTon" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 1%;"></td>
                                <td style="width: 49%;" valign="top">
                                    <div class="block-02">
                                        <div class="head-icon">
                                            <img src="../images/DSR.png" />
                                        </div>
                                        <div class="heading">Top 5 SKUs</div>
                                        <div class="three">
                                            <div class="thumb-6">
                                                <div class="icon-n">
                                                    <img src="../images/gr.png" />
                                                </div>
                                                <div class="text-blue2">SKU</div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="icon-n">
                                                    <img src="../images/22.png" />
                                                </div>
                                                <div class="text-green2">Sales Qty</div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="icon-n">
                                                    <img src="../images/br.png" />
                                                </div>
                                                <div class="text-brown2">Sales Value</div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="icon-n">
                                                    <img src="../images/ton.png" />
                                                </div>
                                                <div class="text-dbrown2">Free Qty</div>
                                            </div>

                                            <div class="thumb-6">
                                                <div class="thumb-7">
                                                    <asp:Repeater ID="rSKU" runat="server">
                                                        <ItemTemplate>
                                                            <div class="thumb-7">
                                                                <div class=" sku-textsmal"><%# Eval("SKU_NAME")%></div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="thumb-7">
                                                    <asp:Repeater ID="rSKU1" runat="server">
                                                        <ItemTemplate>
                                                            <div class="thumb-7">
                                                                <div class=" sku-text"><%# DataBinder.Eval(Container.DataItem, "SalesQty", "{0:0,00}")%> </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="thumb-7">
                                                    <asp:Repeater ID="rSKU2" runat="server">
                                                        <ItemTemplate>
                                                            <div class="thumb-7">
                                                                <div class=" sku-text"><%# DataBinder.Eval(Container.DataItem, "SalesValue", "{0:0,00}")%> </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="thumb-7">
                                                    <asp:Repeater ID="rSKU3" runat="server">
                                                        <ItemTemplate>
                                                            <div class="thumb-7">
                                                                <div class=" sku-text"><%# DataBinder.Eval(Container.DataItem, "SalesTon", "{0:0,00}")%> </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>

                                            <div class="thumb-6">
                                                <div class="thumb-7">
                                                    <div class="thumb-8">
                                                        <div class="sku-tot">Total</div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="thumb-7">
                                                    <div class="thumb-8">
                                                        <div class="sku-tot">
                                                            <asp:Label ID="lblTotalSKUQty" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="thumb-7">
                                                    <div class="thumb-8">
                                                        <div class="sku-tot">
                                                            <asp:Label ID="lblTotalSKUValue" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="thumb-6">
                                                <div class="thumb-7">
                                                    <div class="thumb-8">
                                                        <div class="sku-tot">
                                                            <asp:Label ID="lblTotalSKUTon" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </td>
                                <td style="width: 1%;"></td>
                            </tr>
                        </table>
                        <div class="clear"></div>
                    </div>
                    <div class="clear"></div>
                </div>
                <!--------------------------------Bottom Section End----------------------------------->
                <div class="clear"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
