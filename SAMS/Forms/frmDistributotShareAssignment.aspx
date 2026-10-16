<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributotShareAssignment.aspx.cs" Inherits="frmDistributotShareAssignment"
    Title="SAMS :: Distributor Share Assignment" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
       
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <table>
                            <tr>
                                <td style="width: 100px">
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Location" Width="77px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DrpDistributor" runat="server" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" AutoPostBack="true" CssClass="DropList" Width="205px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel3" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Height="208px" ScrollBars="Vertical" Width="500px">
                                    <asp:GridView ID="GrdCreditLimit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" CssClass="gridRow2" ForeColor="Silver" HorizontalAlign="Center"
                                        Width="100%" >
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" /> <AlternatingRowStyle BackColor="#E0E0E0" />
                                        <Columns>
                                            <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                        
                                            <asp:BoundField DataField="PRINCIPAL" HeaderText="Principal">
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="200px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Share Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSHARE_VALUE" Text='<%# Eval("SHARE_VALUE") %>' runat="server" CssClass="txtBox" Width="98%"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Share Per">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSHARE_PER" Text='<%# Eval("SHARE_PER") %>' runat="server" CssClass="txtBox" Width="98%"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="z-index: 101; left: 740px; width: 100px; position: absolute; top: 134px; height: 100px">
                            &nbsp;<asp:Panel ID="Panel1" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <br />
                        <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Save"
                            ValidationGroup="vg" Width="89px" CssClass="Button" />
                        <asp:Button ID="btnCancel" runat="server" Font-Size="8pt" Text="Cancel" Width="91px"
                            CssClass="Button" />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
