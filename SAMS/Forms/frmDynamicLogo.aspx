<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDynamicLogo.aspx.cs" Inherits="Forms_frmDynamicLogo" Title="SAMS: Add Logo" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="60%">
            <tr>
                <td style="width: 100px">
                   
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True" AccessKey="C"></asp:Label><br />
                                        </td>
                                        <td style="width: 100px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 49px; height: 29px">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="65px" Text="Add Logo"></asp:Label></strong>
                                        </td>
                                        <td style="width: 100px; height: 29px">
                                           <asp:FileUpload ID="fuImage" ToolTip="Select Image"   runat="server"></asp:FileUpload>
                                        </td>
                                        <td style="width: 100px; height: 29px"></td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSaveDesignation" OnClick="btnSaveDesignation_Click" runat="server"
                                                Width="85px" Font-Size="8pt" Text="Upload Logo" CssClass="Button" />
                                        </td>
                                        <td style="width: 100px; height: 37px"></td>
                                    </tr>
                                </tbody>
                            </table>
                      
                </td>
                <td style="width: 100px"></td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Vertical" Width="50%" BorderWidth="1px">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvDesignation" runat="server" Width="100%" ForeColor="SteelBlue"
                                    CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                    AutoGenerateColumns="False"  OnRowDeleting="gvDesignation_RowDeleting"
                                  >
                                  
                                    <AlternatingRowStyle BackColor="#E0E0E0" />
                                    <Columns>
                                        <asp:BoundField DataField="LOGO_ID" HeaderText="Id">
                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                CssClass="HidePanel"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:ImageField DataImageUrlField="LGOG_NAME" ControlStyle-Height="100" ControlStyle-Width="200" HeaderText="Image">
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            </asp:ImageField>
                                        
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
