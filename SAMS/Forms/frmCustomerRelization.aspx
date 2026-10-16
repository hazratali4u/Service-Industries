<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCustomerRelization.aspx.cs" Inherits="Forms_frmCustomerRelization"
    Title="SAMS :: Bank Transaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            var oControl = args.get_postBackElement();

            if (oControl.value == "Save" || oControl.value == "Update Order"
                 || oControl.value == "Save Invoice" || oControl.value == "Sale Return"
                  || oControl.value == "Filter" || oControl.value == "Add"
                 || oControl.value == "Calculate"
                 ) {
                oControl.value = "Wait...";
                oControl.disabled = true;
            }
        }




        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }

        }

        function pageLoad() {
            $("select").searchable();

            $('#<%=GrdOrder.ClientID %>').tablesorter(
               {
                   headers: {
                       3: {
                           sorter: false
                       },
                       6: {
                           sorter: false
                       },
                       10: {
                           sorter: false
                       },
                       11: {
                           sorter: false
                       },
                       12: {
                           sorter: false
                       },
                       13: {
                           sorter: false
                       }
                   }
               }
               );
            $('#<%=gvSaleForceCash.ClientID %>').tablesorter(
             {
                 headers: {
                     0: {
                         sorter: false
                     },
                     1: {
                         sorter: false
                     },
                     2: {
                         sorter: false
                     },
                     3: {
                         sorter: false
                     },
                     4: {
                         sorter: false
                     },
                     5: {
                         sorter: false
                     },
                     7: {
                         sorter: false
                     },
                     8: {
                         sorter: false
                     }
                 }
             }
             );
        }
    </script>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;
        window.onload = function () {

            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.GrdCredit.Rows.Count %>');

            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {

            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.GrdCredit.ClientID %>');
            var TargetChildControl = "ChbIsAssigned";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                          Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {

            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter; 
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }
    </script>
    <div id="right_data">
        <%-- <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                        Width="31px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
        </div>--%>

        <div style="z-index: 101; left: 325px; width: 100px; position: absolute; top: 225px; height: 100px">
            <asp:Panel ID="Panel2" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="27px" />
                        Wait Update.......
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 100px" align="left"></td>
                                            <td style="width: 94px" align="left" colspan="1"></td>
                                            <td align="left" colspan="1"></td>
                                            <td align="left" colspan="1"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px" align="left"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="62px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpDeliveryMan_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label8" runat="server" Width="89px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px" align="left"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="58px" Text="Route      " CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="99px" Text="Account Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" align="left">
                                                <asp:DropDownList ID="DrpAccountType" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpAccountType_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Value="19">Cash Realization</asp:ListItem>
                                                    <asp:ListItem Value="21">Cash Advance</asp:ListItem>
                                                    <asp:ListItem Value="22">Bank Deposit (Branch)</asp:ListItem>
                                                    <asp:ListItem Value="23">Income Tax</asp:ListItem>
                                                    <asp:ListItem Value="28">Credit Transfer Out</asp:ListItem>
                                                    <asp:ListItem Value="29">Advance Return</asp:ListItem>
                                                    <asp:ListItem Value="222">Bank Deposit (DM)</asp:ListItem>
                                                    <asp:ListItem>Cash From DM</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="25px" Text=""></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="55px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="240px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" rowspan="8"></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="97px" Text="Account" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px">
                                                <asp:DropDownList ID="DrpAccountDetail" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpAccountDetail_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 100px"></td>
                                            <td valign="middle" align="left" colspan="2" rowspan="8">
                                                <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" BorderColor="Silver"
                                                    BorderStyle="Solid" BorderWidth="1px">
                                                    <asp:GridView ID="GrdCredit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        Width="320px" DataKeyNames="SALE_INVOICE_ID" OnRowCreated="GrdCredit_RowCreated">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" /> <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server" Width="14px" Checked="true" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="cbAll" runat="server" onclick="javascript:HeaderClick(this);" />
                                                                </HeaderTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Invoice No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount" DataFormatString="{0:N}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"  />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="94px" Text="Chque No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">&nbsp; <strong>
                                                <asp:Label ID="Label3" runat="server" Width="94px" Text="Slip No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="94px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtSlipNo" runat="server" Width="94px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="74px" Text="Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Width="73px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtAmount" runat="server" Width="94px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 201px" valign="top" align="left">
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="194px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 100px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px" valign="top" align="left" colspan="2"></td>
                                            <td style="width: 100px; height: 20px" valign="top" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2"></td>
                                            <td style="width: 100px" align="left" colspan="1"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 36px" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="102px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                                    Text="Cancel" CssClass="Button" />
                                            </td>
                                            <td style="width: 100px; height: 36px" valign="middle" align="left"></td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td align="right"></td>
                                            <td align="left">
                                                <asp:Label ID="Label12" runat="server" Text="" Width="90px"></asp:Label>
                                                <asp:Label ID="Label11" runat="server" Text="Total :" Width="70px">

                                                </asp:Label><asp:Label ID="grdTotal" runat="server" Text="0.00" ></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                &nbsp;
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DrpRoute" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table  width="850px" style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid; border-bottom: silver thin solid">
                        <tbody>
                                <tr style="background-color: silver"" >
                                <td style="height: 21px" align="left">
                                    <asp:Label ID="Label13" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 21px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="c.CUSTOMER_CODE">Customer Code</asp:ListItem>
                                        <asp:ListItem Value="C.CUSTOMER_NAME">Customer Name</asp:ListItem>
                                        <asp:ListItem Value="L.Manual_Document_no">Document No</asp:ListItem>
                                       
                                        <asp:ListItem Value="L.Voucher_no">Voucher No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 21px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="height: 21px" align="left" width="200px" colspan="2">
                                    <asp:Button ID="btnSearch" runat="server" Width="85px" CssClass="btn" Font-Size="8pt" Text="Filter"
                                        OnClick="btnSearch_Click"></asp:Button>
                                </td>
                                    
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="Panel12" runat="server" Width="100%" Height="200px" ScrollBars="Vertical">
                                        <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
                                            BorderColor="White" OnRowDeleting="GrdOrder_RowDeleting" HorizontalAlign="Center"
                                            BackColor="White" AutoGenerateColumns="False"> <AlternatingRowStyle BackColor="#E0E0E0" />
                                            <Columns>
                                                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="voucher_type_id" HeaderText="voucher_type_id">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Principal">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Manual_Document_no" HeaderText="Document No">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Ledger_date" HeaderText="Ledger Date">
                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" Width="80px" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Voucher_no" HeaderText="Voucher No">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Balance" DataFormatString="{0:F2}" HeaderText="Amount">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Document_no" HeaderText="Document_no">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                

                                         
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
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead"></HeaderStyle>
                                        </asp:GridView>
                                        <asp:GridView ID="gvSaleForceCash" runat="server" Visible="False" Width="100%" ForeColor="SteelBlue"
                                            CssClass="tablesorter" BorderColor="White" OnRowDeleting="gvSaleForceCash_RowDeleting"
                                            HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False" OnRowEditing="gvSaleForceCash_RowEditing">
                                             <AlternatingRowStyle BackColor="#E0E0E0" />
                                            <Columns>
                                                <asp:BoundField DataField="SALE_FORCE_CASH_ID" HeaderText="SALE_FORCE_CASH_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRINCIPAL" HeaderText="PRINCIPAL">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DELIVERYMAN" HeaderText="DELIVERY MAN">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="DOCUMENT DATE">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
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
                                       <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead"></HeaderStyle>
                                           </asp:GridView>
                                        <asp:HiddenField ID="hfSALE_FORCE_CASH_ID" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfPRINCIPAL_ID" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfDELIVERYMAN_ID" runat="server"></asp:HiddenField>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="4">
                                    <strong>Total Amount</strong><asp:TextBox ID="txtTotalAmount" ReadOnly="true" runat="server" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
