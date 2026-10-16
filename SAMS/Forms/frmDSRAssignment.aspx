<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDSRAssignment.aspx.cs" Inherits="Forms_frmBankAssignment" Title="SAMS: DSR Mapping" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
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
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="3">
                                                <asp:HiddenField ID="HdnDistributorID" runat="server" />

                                            </td>


                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label13" runat="server" Text="NSM" Width="58px"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="DrpNSM" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpNSM_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width : 20px"></td>
                                             <td align="left" style="width: 45px; height: 28px">
                                                <strong>
                                                    <asp:Label ID="Label15" runat="server" Text="RSM" Width="58px"></asp:Label></strong>
                                            </td>
                                            <td align="left" style="height: 16px; width: 2px;">
                                                <asp:DropDownList ID="DrpRSM" runat="server" AutoPostBack="True" CssClass="DropList"
                                                    OnSelectedIndexChanged="DrpRSM_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td style="width: 45px; height: 28px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label33" runat="server" Text="ASM/ASO" Width="58px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 16px; width: 2px;" align="left">
                                                <asp:DropDownList ID="DrpASM_ASO" runat="server" Width="200px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DrpASM_ASO_SelectedIndexChanged" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td ></td>
                                             <td style="width: 45px; height: 28px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label16" runat="server" Text="SO/SR" Width="58px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 16px; width: 2px;" align="left">
                                                <asp:DropDownList ID="DrpSO_SR" runat="server"
                                                    Width="200px"
                                                    CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td style="width: 90px; height: 28px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Text="Distributor" Width="58px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 16px; width: 2px;" align="left">
                                                <asp:DropDownList ID="DrpDistributor" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged"
                                                    Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
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
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 100px" rowspan="1">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="142px" Text="Unassigned DSR" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 102px" valign="bottom" align="center"></td>
                                            <td style="width: 102px" rowspan="1">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Text="Assigned DSR"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" rowspan="4">
                                                 <asp:Panel ID="pnlLocation" runat="server" 
                                                        __designer:wfdid="w55" HorizontalAlign="Left" BorderWidth="1px" CssClass="pnlCheckboxlist"
                                           >
                                                <asp:ListBox ID="lstUnAssignDSR" runat="server" Width="287px" Height="200px" CssClass="DropList"></asp:ListBox>
                                            </asp:Panel>
                                                     </td>
                                            <td valign="middle" align="center">
                                                <asp:Button ID="btnAssign" runat="server" Width="30px" Font-Size="8pt" Text=">" OnClick="btnAssign_Click"
                                                    CssClass="Button" Height="28px" />
                                            </td>
                                            <td style="width: 102px" rowspan="4">
                                                  <asp:Panel ID="Panel2" runat="server" 
                                                        __designer:wfdid="w55" HorizontalAlign="Left" BorderWidth="1px" CssClass="pnlCheckboxlist"
                                           >
                                                <asp:ListBox ID="lstAssignDSR" runat="server" Width="287px" Height="200px" CssClass="DropList"></asp:ListBox>
                                            </asp:Panel>
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
                                <asp:Panel ID="Panel1" runat="server" Width="700px" Height="200px" ScrollBars="Vertical"
                                    BackColor="#E0E0E0">
                                    <asp:GridView ID="GrdDSR_Assignment" runat="server" Width="100%" Height="1px" ForeColor="SteelBlue"
                                        CssClass="gridRow2" BackColor="White" OnRowEditing="GrdDSR_Assignment_RowEditing"
                                        AutoGenerateColumns="False" BorderColor="White" HorizontalAlign="Center">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous"></PagerSettings>
                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                        <Columns>
                                            <asp:BoundField DataField="NSM_ID" HeaderText="NSM_ID">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NSM" HeaderText="NSM">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RSM_ID" HeaderText="RSM_ID">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RSM" HeaderText="RSM">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ASM_ID" HeaderText="ASM_ID">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ASM" HeaderText="ASM/ASO">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TSM_ID" HeaderText="TSM_ID">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TSM" HeaderText="TSO/TSE">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Distributor_ID" HeaderText="Distributor_ID">
                                                <ItemStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Distributor">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="DSR">
                                                <ItemTemplate>
                                                    <asp:ListBox ID="lstDSR" runat="server" Width="100%" CssClass="DropList" AutoPostBack="True"></asp:ListBox>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TemplateField>
                                         <%--   <asp:CommandField ShowEditButton="True" HeaderText="Edit" Visible="true" ButtonType="Image" EditImageUrl="../images/edit.gif">
                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"></ItemStyle>
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
        </div>
    </div>
</asp:Content>
