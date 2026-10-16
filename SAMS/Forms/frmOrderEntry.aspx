<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOrderEntry.aspx.cs" Inherits="Forms_frmOrderEntry" Title="SAMS :: Order/Invoice Step 2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("#<%=ddlSkus.ClientID %>").searchable();
            $("#<%=ddlCustomer.ClientID %>").searchable();
            $("#<%=ddlTransporter.ClientID %>").searchable();

            var Str = $('#<%=ddlSkus.ClientID %> option:selected').text();
            if (Str != 'Select Product') {
                var Str2 = Str.split(':');
                if (Str2.length > 0) {
                    document.getElementById("<%= txtUnitRate.ClientID %>").value = Str2[1];
                    document.getElementById("<%= txtStock.ClientID %>").value = Str2[2];
                }
            }
            $('#<%=ddlSkus.ClientID %>').change(function () {
                var Str = $('#<%=ddlSkus.ClientID %> option:selected').text();
                var Str2 = Str.split(':');
                document.getElementById("<%= txtUnitRate.ClientID %>").value = Str2[1];
                document.getElementById("<%= txtStock.ClientID %>").value = Str2[2];
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = true;
        }
        function endRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = false;
        }
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                return false;
            }
            return true;
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
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                        Width="31px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
           <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
        </div>

    
        <div>
            <span class="heading">Order/Invoice Step 2</span>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <strong>
                                                                        <asp:Label ID="Label6" runat="server" Text="Principal"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:DropDownList ID="DrpPrincipal" runat="server" Width="231px"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <strong>
                                                                        <asp:Label ID="lblBuiltyNo" TabIndex="1" runat="server" Text="Builty #:"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="width: 50%">
                                                                    <asp:TextBox ID="txtBuiltyNo" runat="server" Width="125px" MaxLength="50"></asp:TextBox>
                                                                    <asp:TextBox ID="txtBuiltyAmount" runat="server" Width="116px" placeholder="Trade Expense"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <strong>
                                                                        <asp:Label ID="lblDocumentNo" runat="server" Text="Order No"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:DropDownList ID="drpDocumentNo" runat="server" Width="231px"
                                                                        OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                    <asp:CheckBox ID="ChbDiscount" runat="server" Visible="False" Width="90px" Text="Promotion"
                                                                        AutoPostBack="True" Checked="True"></asp:CheckBox>
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <strong>
                                                                        <asp:Label ID="lblTransporter" TabIndex="1" runat="server" Text="Transporter:"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="width: 50%">
                                                                    <asp:DropDownList ID="ddlTransporter" runat="server" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td valign="middle" align="left">
                                                                        <strong>
                                                                            <asp:Label ID="Label11" runat="server" Width="77px" Text="Order Type" CssClass="lblbox"></asp:Label></strong>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="2">
                                                                        <asp:RadioButtonList ID="RblPayMode" runat="server" Width="228px" Height="1px" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="214">Cash</asp:ListItem>
                                                                            <asp:ListItem Selected="True" Value="215">Credit</asp:ListItem>
                                                                            <asp:ListItem Value="216">Advance</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="1"></td>
                                                                    <td rowspan="2" valign="bottom"> 
                                                                        <asp:Panel runat="server" ID="pnlshare" BorderWidth="1px" Width="182px" Height="40px" HorizontalAlign="Left" >
                                                                             <asp:Label ID="lblDistributorSh" runat="server"  Text="Dist. Share"></asp:Label>
                                                                         
                                                                           
                                                                            <strong>
                                                                              <asp:Label ID="lblShareValue" Font-Bold="true" Font-Size="Medium"  runat="server"  Text="0"></asp:Label>
                                                                            </strong><br />
                                                                            <asp:Label ID="lblDistrSharePer" runat="server"  Text="Dist. Share"></asp:Label>
                                                                         
                                                                           
                                                                            <strong>
                                                                              <asp:Label ID="lblSharePer" Font-Bold="true" Font-Size="Medium"  runat="server"  Text="0"></asp:Label>
                                                                            </strong>
                                                                                </asp:Panel>
                                                                            </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="left">
                                                                        <strong>
                                                                            <asp:Label ID="Label12" runat="server" Width="101px" Text="Route/Saleman" CssClass="lblbox"></asp:Label></strong>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="2">
                                                                        <asp:TextBox ID="txtprincipal" runat="server" Width="277px" CssClass="txtBox " ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td valign="top" align="left" colspan="1">
                                                                        <asp:TextBox ID="txtDeliveryMan" runat="server" Width="180px" CssClass="txtBox "
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 23px" align="left">
                                                                        <strong>
                                                                            <asp:Label ID="Label1" runat="server" Text="Customer"></asp:Label>
                                                                        </strong>
                                                                    </td>
                                                                    <td style="height: 23px" align="left" colspan="2">
                                                                        <asp:DropDownList ID="ddlCustomer" runat="server" Width="278px" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="height: 23px" align="left">
                                                                        <asp:TextBox ID="txtDiscountType" onkeyup="SearchList()" runat="server" Width="180px"
                                                                            CssClass="txtBox " ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td><asp:TextBox ID="txtPriceGroup"  runat="server" Width="132px"
                                                                            CssClass="txtBox"></asp:TextBox></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="height: 126px"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 260px; background-color: #006699; padding: 2px;">
                                                                        <label style="color: White; font-weight: bold;">
                                                                            SKU Description
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 61px; background-color: #006699; padding: 2px;">
                                                                        <label style="color: White; font-weight: bold;">
                                                                            Quantity
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 61px; background-color: #006699; padding: 2px;">
                                                                        <label style="color: White; font-weight: bold;">
                                                                            Discount
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 62px; background-color: #006699; padding: 2px;">
                                                                        <label style="color: White; font-weight: bold;">
                                                                            Unit Price
                                                                        </label>
                                                                    </td>
                                                                     <td style="width: 62px; background-color: #006699; padding: 2px;">
                                                                        <label style="color: White; font-weight: bold;">
                                                                            Share Type
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 65px; background-color: #006699; padding: 2px;">
                                                                        <label style="color: White; font-weight: bold;">
                                                                            Dist. Share
                                                                        </label>
                                                                    </td>
                                                                    <td style="width: 72px; background-color: #006699; padding: 2px;">
                                                                        <label style="color: White; font-weight: bold;">
                                                                            Amount
                                                                        </label>
                                                                    </td>

                                                                    <td style="height: 16px">
                                                                        <strong>
                                                                            <asp:Label ID="lblStock" runat="server" Text="Stock:"></asp:Label>
                                                                            <asp:TextBox ID="txtStock" runat="server" Enabled="false" Text="0" Width="50px"></asp:TextBox>
                                                                        </strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSkus" runat="server" Width="260px" Style="z-index: inherit;">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQuantity" runat="server" Width="61px"></asp:TextBox>
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="txtBox " Enabled="false" Width="61px"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbe_discount" TargetControlID="txtDiscount"
                                                                            ValidChars="0123456789." runat="server">
                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtUnitRate" runat="server" Width="61px" CssClass="txtBox" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpdisType" runat="server" Width="80px">
                                                                            <asp:ListItem Value="0" Text="By Value"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="By Per"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDistShare" runat="server" Width="62px"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtDistShare"
                                                                            ValidChars=".0123456789" runat="server">
                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="txtBoxnNum" Enabled="False"
                                                                            Width="72px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add"
                                                                            ValidationGroup="vg" Width="95px" AccessKey="A" CssClass="Button" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="8">
                                                                        <asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" Width="100%"
                                                                            BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                                            <asp:GridView ID="GrdPurchase" runat="server"
                                                                                BackColor="White" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False"
                                                                                OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing" Width="100%">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="75px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                                        <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="200px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
                                                                                        <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="65px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="STANDARD_DISCOUNT" HeaderText="Discount" DataFormatString="{0:F2}">
                                                                                        <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="65px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
                                                                                        <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                                            Width="65px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="DISTRIBUTOR_SHARE" HeaderText="Dist. Share">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                                            Width="80px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="QUANTITY_CTN" HeaderText="QUANTITY_CTN">
                                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                                        <ItemStyle CssClass="HidePanel" />
                                                                                    </asp:BoundField>
                                                                                       <asp:BoundField DataField="SKU_GST" HeaderText="QUANTITY_CTN">
                                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                                        <ItemStyle CssClass="HidePanel" />
                                                                                    </asp:BoundField>
                                                                                    <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:CommandField>
                                                                                    <asp:TemplateField HeaderText="Delete">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                                                CommandName="Delete"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px"
                                                                                            HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 30%" valign="top">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <strong>
                                                                                    <asp:Label ID="Label10" runat="server" Width="60px" Text="Free SKU"></asp:Label>
                                                                                </strong>
                                                                                <asp:Panel
                                                                                    ID="Panel4" runat="server" Width="350px" Height="90px" BorderColor="Silver" BorderStyle="Groove"
                                                                                    BorderWidth="1px" ScrollBars="Auto">
                                                                                    <asp:GridView ID="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                                                                        BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                                            PreviousPageText="Previous"></PagerSettings>
                                                                                        <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black"></RowStyle>
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                                                                                                <ItemStyle Width="80px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
                                                                                                <ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="Quantity" HeaderText="Qty">
                                                                                                <ItemStyle Width="50px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                        <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                                <br />
                                                                                <asp:TextBox ID="txtRemarks" runat="server" Width="350px" TextMode="MultiLine" Height="40" placeholder="Remarks"></asp:TextBox>
                                                                                <br />
                                                                                <br />
                                                                                <asp:Button AccessKey="C" ID="btnCalculate" TabIndex="100" OnClick="btnCalculate_Click"
                                                                                runat="server" Width="100px" Font-Size="8pt" Text="Calculate" Enabled="False"
                                                                                CssClass="Button" />
                                                                                &nbsp;&nbsp;
                                                                                <asp:Button AccessKey="S" ID="btnSaveOrder" TabIndex="101" OnClick="btnSaveOrder_Click"
                                                                                runat="server" Width="110px" Font-Size="8pt" Text="Save Order" Enabled="False"
                                                                                CssClass="Button" />
                                                                                &nbsp;&nbsp;
                                                                                <asp:Button AccessKey="H" ID="btnCancel" TabIndex="102" runat="server" Width="110px"
                                                                                Font-Size="8pt" Text="Home" OnClick="btnCancel_Click" CssClass="Button" />
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 30%" valign="top">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="lblDocumentNo1" runat="server" Width="94px" Text="Gross Sale"></asp:Label>
                                                                                    </strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="txtGrossAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                                        CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                              <tr>
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="Label3" runat="server" Width="94px" Text="GST"></asp:Label>
                                                                                    </strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="txtSKuGst" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                                        CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="Label4" runat="server" Width="110px" Text="Amount Incl.GST"></asp:Label>
                                                                                    </strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="txtAmountINclSKuGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                                        CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="Label51" runat="server" Width="110px" Text="Discount" CssClass="lblbox"></asp:Label></strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="numTxtTotalStndrdDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                                                        Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeStndrdDiscnt" TargetControlID="numTxtTotalStndrdDiscnt"
                                                                                        ValidChars="0123456789." runat="server">
                                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 177px" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="lblfromLocation1" runat="server" Width="94px" Text="Scheme"></asp:Label></strong>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:TextBox ID="numtxtTotalExtraDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                                                        Font-Bold="True" ReadOnly="true"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 177px" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="lblDistShare" runat="server" Text="Distributor Share" Enabled="false"></asp:Label>
                                                                                    </strong>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:TextBox ID="numtxtTotalDistShare" runat="server" Width="150px" ForeColor="Black"
                                                                                        Font-Bold="True" ReadOnly="true"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                           <%-- <tr style="visibility:hidden">
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="Label81" runat="server" Width="93px" Text="GST Amount" CssClass="lblbox"></asp:Label></strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="numTxtTotalGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                                        ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="visibility:hidden">
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="Label9" runat="server" Width="93px" Text="TST Amount"></asp:Label></strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="numTxtTotalTST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                                        ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>--%>
                                                                            <tr>
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="Label2" runat="server" Width="93px" Text="Builty Amount"></asp:Label>
                                                                                    </strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="numtxtBuiltyAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                                        ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="Label91" runat="server" Width="105px" Text="Net Amount" CssClass="lblbox"></asp:Label></strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="numTxtTotlAmnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                                                        ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 18px; width: 177px;" align="right">
                                                                                    <strong>
                                                                                        <asp:Label ID="Label5" runat="server" Width="105px" Text="Cash Received"
                                                                                            Visible="false"></asp:Label></strong>
                                                                                </td>
                                                                                <td style="width: 7px; height: 18px" align="right">
                                                                                    <asp:TextBox ID="txtCashReceived" runat="server" Width="150px" Visible="false" ForeColor="Black"
                                                                                        Font-Bold="True" CssClass="txtBoxnNum" Enabled="False"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 40%" valign="top">
                                                                        <asp:Panel ID="Panel1" runat="server" Width="250px" Height="160px" BorderColor="Silver" BorderStyle="Groove"
                                                                            BorderWidth="1px" ScrollBars="Auto">
                                                                            <asp:GridView ID="gvCategory" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                                                                BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                                                                <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black"></RowStyle>
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="SKU_HIE_ID" HeaderText="SKU_HIE_ID">
                                                                                        <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                                        <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Category">
                                                                                        <ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                                        <ItemStyle Width="50px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                                <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:HiddenField ID="hfBillBookNo" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:TextBox ID="numTxtTotalSED" runat="server" CssClass="txtBox " Font-Bold="False"
                            ForeColor="Black" Width="139px" ReadOnly="True" Visible="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
