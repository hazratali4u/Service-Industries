<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseEntry.aspx.cs" Inherits="Forms_frmPurchaseEntry" Title="SAMS :: Stock Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
            $("#<%=ddlSkus.ClientID %>").searchable();
        }

        function ValidateForm() {
            var str;

          
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                return false;
            }

            str = document.getElementById('<%=txtDocumentNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Invoice/DC No');
                 document.getElementById('<%=txtDocumentNo.ClientID%>').focus();
                return false;
            }

            str = document.getElementById('<%=txtBuiltyNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Builty No');
                 document.getElementById('<%=txtBuiltyNo.ClientID%>').focus();
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
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
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
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width:50%" valign="top">
                                            <table width="100%" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>
                                                            <asp:Label ID="Label2" runat="server" Text="Transaction Type"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:DropDownList ID="DrpDocumentType" runat="server" AutoPostBack="True" CssClass="DropList"
                                                            OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" Width="200px">
                                                            <asp:ListItem Value="2">Purchase</asp:ListItem>
                                                            <asp:ListItem Value="5">Transfer Out</asp:ListItem>
                                                            <asp:ListItem Value="3">Purchase Return</asp:ListItem>
                                                            <asp:ListItem Value="4">Transfer In</asp:ListItem>
                                                            <asp:ListItem Value="6">Damage</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>
                                                            <asp:Label ID="lblDocumentNo" runat="server" Text="Document No"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                            OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>
                                                            <asp:Label ID="lbltoLocation" runat="server" Text="Principal"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" CssClass="DropList"
                                                            OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>
                                                            <asp:Label ID="lblfromLocation" runat="server" Text="Purchase For"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" 
                                                            Width="200px" onselectedindexchanged="drpDistributor_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>
                                                            <asp:Label ID="lblChildLocation" runat="server" Text="Child Location"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:DropDownList ID="ddlChildLocation" runat="server" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>
                                                            <asp:Label ID="Label4" runat="server" Text="Transfer To" Visible="False"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:DropDownList ID="DrpTransferFor" runat="server" Visible="False"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>
                                                            <asp:Label ID="Label1" runat="server" Text="INV/DC  No"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:TextBox ID="txtDocumentNo" runat="server" Width="195px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>
                                                            <asp:Label ID="Label3" runat="server" Text="Builty No"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:TextBox ID="txtBuiltyNo" runat="server" Width="195px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%;" valign="middle" align="left">
                                            <asp:Panel ID="pnlSummary" runat="server" Width="70%" ScrollBars="None" BorderWidth="1px"
                                            BorderStyle="Groove" BorderColor="Silver" Style="padding: 10px;">
                                            <table width="100%" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>Gross Amount</strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:TextBox ID="txtGrossAmount" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>GST</strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:TextBox ID="txtGST" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <strong>Net Value</strong>
                                                    </td>
                                                    <td style="width: 70%">
                                                        <asp:TextBox ID="txtNetValue" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 450px; background-color: #006699; padding: 2px;">
                                                <label style="color: White; font-weight: bold;">
                                                    SKU Description
                                                </label>
                                            </td>
                                            <td style="width: 73px; background-color: #006699; padding: 2px;">
                                                <label style="color: White; font-weight: bold;">
                                                    Quantity    
                                                </label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSkus" runat="server" Width="450px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="95px"
                                                    Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Panel ID="Panel2" runat="server" Width="640px" Height="130px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing"
                                                        ShowHeader="False" Width="100%">
                                                        
                                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                                    Width="205px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                             <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                  <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                        <img src="../images/edit.gif" />
                                                  </asp:LinkButton>
                                           </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                         
                                        <asp:TemplateField HeaderText="Delete">
                                                 <ItemTemplate>
                                                      <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                           CommandName="Delete">
                                                            <img src="../images/delete.gif" width="16" height="16">
                                                      </asp:LinkButton>
                                                 </ItemTemplate>
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center"></ItemStyle>
                                          </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                                <strong>
                                    <asp:Label ID="Label7" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalQuantity" onkeyup="SearchList()" runat="server" Width="88px"
                                    Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
