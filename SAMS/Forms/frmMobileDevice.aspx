<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmMobileDevice.aspx.cs" Inherits="Forms_frmMobileDevice" Title="SAMS: Mobile Device" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
         <%--   $('#<%=txtgstno.ClientID %>').mask("99-99-9999-999-99");--%>
            $("select").searchable();
            $.tablesorter.addParser({
                id: "datetime",
                is: function (s) {
                    return false;
                },
                format: function (s, table) {
                    s = s.replace(/\-/g, "/");
                    s = s.replace(/(\d{1,2})[\/\-](\d{1,2})[\/\-](\d{4})/, "$3/$2/$1");
                    return $.tablesorter.formatFloat(new Date(s).getTime());
                },
                type: "numeric"
            });

            $('#<%=GridDistributor.ClientID %>').tablesorter(
         {

             dateFormat: 'dd-mm-yyyy',
             headers:
                 {
                     8: { sorter: 'datetime' }


                 }

         }

         );
        }


        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtDevicName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Device Name');
                return false;
            }
            str = document.getElementById('<%=txtIMEI.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter IMEI');
                return false;
            }
            str = document.getElementById('<%=txtMac.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must MAC Address');
                return false;
            }
            str = document.getElementById('<%=txtMake.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Make');
                return false;
            }

            str = document.getElementById('<%=txtModel.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter device model');
                return false;
            }

            return true;
        }

    </script>
    <div id="right_data">
        <div>
            <table>
                <tr>
                    <td>

                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>

                                        <tr>
                                            <td style="height: 25px; width: 89px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="83px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpLocation" OnSelectedIndexChanged="drpLocation_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td style="height: 25px; width: 120px;" align="left">
                                                <strong>
                                                    <asp:Label ID="lbldesignationID" runat="server" Width="120px" Text="Order Booker"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpOrderBooker" runat="server"  Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px; width: 89px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="77px" Text="Device Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtDevicName" runat="server" Width="197px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 25px"></td>
                                            <td align="left" style="width: 103px">
                                                <strong>
                                                    <asp:Label ID="lblMake" runat="server" Width="69px" Text="Make" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtMake" runat="server" Width="197px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px; width: 89px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="96px" Text="Model" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtModel" runat="server" Width="197px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="height: 25px"></td>
                                            <td align="left" style="width: 103px">
                                                <strong>
                                                    <asp:Label ID="lblPhNo" runat="server" Width="110px" Text="MAC Address" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtMac" runat="server" Width="197px" CssClass="txtBox "></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="height: 40px; width: 89px;" align="left">
                                                <strong>
                                                    <asp:Label ID="lblAddress2" runat="server" Width="76px" Text="IMEI" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIMEI" runat="server" Width="194px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="height: 24px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>

                                        <tr valign="middle">
                                            <td style="width: 400px; height: 32px" colspan="3" valign="middle">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="84px" Font-Size="8pt"
                                                    Text="Save" ValidationGroup="vg" CssClass="Button" />
                                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Width="83px"
                                                    Font-Size="8pt" Text="Cancel" CssClass="Button" />
                                            </td>
                                            <td style="height: 32px; width: 103px;"></td>
                                            <td style="width: 237px; height: 32px"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                    <td style="width: 250px"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <table class="tblhead" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="color: White; font-weight: bold;">
                                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                                </td>
                                                <td style="width: 170px; height: 21px">
                                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                        <asp:ListItem Value="MOBILE_DEVICE_NAME">All Records</asp:ListItem>
                                                        <asp:ListItem Value="MOBILE_DEVICE_NAME">Mobile Name</asp:ListItem>
                                                        <asp:ListItem Value="MAC_ADDRESS">MAC Address</asp:ListItem>
                                                        <asp:ListItem Value="MODLE">Model</asp:ListItem>
                                                        <asp:ListItem Value="ORDER_BOOKERNAME">Order Booker</asp:ListItem>
                                                        <asp:ListItem Value="MAKE">Make</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 224px; height: 21px">
                                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                </td>
                                                <td style="height: 21px; width: 250px">
                                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                        OnClick="btnFilter_Click" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="200px" BorderWidth="1px">
                                                <asp:GridView ID="GridDistributor" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
                                                    AutoGenerateColumns="False" BackColor="White"
                                                    OnPageIndexChanging="GridDistributor_PageIndexChanging" OnRowEditing="GridDistributor_RowEditing" OnRowDeleting="GridDistributor_RowDeleting"
                                                    HorizontalAlign="Center" AlternatingRowStyle-CssClass="odd">

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MOBULE_DEVICE_ID" HeaderText="MOBULE_DEVICE_ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ORDER_BOOKER_ID" HeaderText="ORDER_BOOKER_ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="MOBILE_DEVICE_NAME" HeaderText="Name">
                                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MAKE" HeaderText="Make">
                                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MODLE" HeaderText="Model">
                                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MAC_ADDRESS" HeaderText=" Mac Address">
                                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IMEI" HeaderText="IMEI">
                                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="ORDER_BOOKERNAME" HeaderText="Order Booker">
                                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Silver"></ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    Text="Delete">
                                                    <img src="../images/delete.gif" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="70px" HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                </asp:GridView>
                                            </asp:Panel>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
