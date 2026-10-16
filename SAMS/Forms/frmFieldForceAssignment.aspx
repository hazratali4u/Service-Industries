<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmFieldForceAssignment.aspx.cs" Inherits="Forms_frmBankAssignment" Title="SAMS: Field Force Mapping" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
     <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
      <script language="javascript" type="text/javascript">
            function pageLoad() {

                $("select").searchable();
            }
          </script>
    <div id="right_data">

        <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 129px; height: 100px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                        Wait Update
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="3"></td>


                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label13" runat="server" Text="Designation" Width="88px"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpDesignation" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px" OnSelectedIndexChanged="DrpDesignation_SelectedIndexChanged">
                                                    <asp:ListItem Text="National Sales Manager" Value="681"></asp:ListItem>
                                                    <asp:ListItem Text="Regional Sales Manager" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="Area Sales Manager" Value="42"></asp:ListItem>
                                                    <asp:ListItem Text="Area Sale Officer" Value="691"></asp:ListItem>
                                                    <asp:ListItem Text="Territory Sale Executive" Value="693"></asp:ListItem>
                                                    <asp:ListItem Text="Territory Sale Office" Value="692"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width : 15px;"></td>
                                            <td align="left" style="width: 45px; height: 28px">
                                                <strong>
                                                    <asp:Label ID="lblDesignation" runat="server" Text="National Sales Manager" Width="170px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 16px; width: 2px;">
                                                <asp:DropDownList ID="drpEmployee" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        

                                    </tbody>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 100px" rowspan="1">
                                                                    <strong>
                                                                        <asp:Label ID="Label4" runat="server" Width="142px" Text="Unassigned Employee" CssClass="lblbox"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 102px" valign="bottom" align="center"></td>
                                                                <td style="width: 102px" rowspan="1">
                                                                    <strong>
                                                                        <asp:Label ID="Label6" runat="server" Text="Assigned Employee"></asp:Label></strong>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px" rowspan="4">
                                                                    <asp:ListBox ID="lstUnAssignEmployee" runat="server" Width="288px" Height="230px" CssClass="DropList"></asp:ListBox>
                                                                </td>
                                                                <td valign="middle" align="center">
                                                                    <asp:Button ID="btnAssign" runat="server" Width="30px" Font-Size="8pt" Text=">" OnClick="btnAssign_Click"
                                                                        CssClass="Button" Height="28px" />
                                                                </td>
                                                                <td style="width: 102px" rowspan="4">
                                                                    <asp:ListBox ID="lstAssignEmployee" runat="server" Width="288px" Height="230px" CssClass="DropList"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="btnAssignAll" runat="server" Width="30px" Font-Size="8pt" Text=">>"
                                                                        OnClick="btnAssignAll_Click" CssClass="Button" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="btnUnAssign" runat="server" Width="30px" Font-Size="8pt" Text="<<"
                                                                        OnClick="btnUnAssign_Click" CssClass="Button" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="middle" align="center">
                                                                    <asp:Button ID="btnUnAssignAll" runat="server" Width="30px" Font-Size="8pt" Text="<"
                                                                        OnClick="btnUnAssignAll_Click" CssClass="Button" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 24px" rowspan="1"></td>
                                                                <td style="width: 102px; height: 24px" align="center">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnSave_Click"
                                                                        Text="Save" ValidationGroup="vg" Width="62px" />
                                                                </td>
                                                                <td style="height: 24px" rowspan="1"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel1" runat="server" Width="695px" Height="200px" ScrollBars="Vertical"
                                                        BackColor="#E0E0E0">
                                                        <asp:GridView ID="GrdDesignation" runat="server" Width="100%" Height="1px" ForeColor="SteelBlue"
                                                            CssClass="gridRow2" BackColor="White" OnRowEditing="GrdDesignation_RowEditing"
                                                            AutoGenerateColumns="False" BorderColor="White" HorizontalAlign="Center">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                            <AlternatingRowStyle BackColor="#E0E0E0" />
                                                            <Columns>
                                                                <asp:BoundField DataField="DESIGNATION_ID" HeaderText="Designation Id">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DESIGNATION" HeaderText="Designation">
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="USER_ID" HeaderText="USER_ID">
                                                                    <ItemStyle CssClass="HidePanel" />
                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="USER_NAME" HeaderText="Employee Name">
                                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Assigned Employees">
                                                                    <ItemTemplate>
                                                                        <asp:ListBox ID="lstEmployee" runat="server" Width="100%" CssClass="DropList" AutoPostBack="True"></asp:ListBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                </asp:TemplateField>
                                                              <%--  <asp:CommandField Visible="true" ShowEditButton="True" HeaderText="Edit" ButtonType="Image" EditImageUrl="../images/edit.gif">
                                                                    <ItemStyle BorderColor="Silver" HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                                </asp:CommandField>--%>
                                                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>

            </table>
        </div>

    </div>
</asp:Content>
