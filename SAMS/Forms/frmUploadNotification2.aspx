<%@ Page Title="SAMS: Upload Notifications" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmUploadNotification2.aspx.cs" Inherits="Forms_frmUploadNotification2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width:10%;" align="right">
                                    <strong><asp:Label ID="lblSubject" runat="server" Text="Subject:"></asp:Label></strong>
                                </td>
                                <td style="width:1%;">                                
                                </td>
                                <td style="width:30%;">
                                    <asp:Label ID="lblSubject2" runat="server" Width="100%"></asp:Label>
                                </td>
                               
                                 <td style="width:14%;" align="right">
                                   <strong><asp:Label ID="lblUploadedBy" runat="server" Text="Uploaded By:"></asp:Label></strong>
                               
                                </td>
                                <td style="width:1%;">                                
                                </td>
                                <td style="width:37%;"><asp:Label ID="lblUploadedBy2" runat="server" Width="100%"></asp:Label>
                                   
                                </td>
                            </tr>
                            <tr><td style="width:20%;" align="right">
                                    <strong><asp:Label ID="lblUploadDateTime" runat="server" Text="Upload DateTime:"></asp:Label></strong>
                                </td>
                                <td style="width:1%;">                                
                                </td>
                                <td style="width:37%;">
                                    <asp:Label ID="lblUploadDateTime2" runat="server" Width="100%"></asp:Label>
                                </td></tr>
                             <tr>
                                <td style="width:10%;" align="right">
                                            <strong><asp:Label ID="lblDocumentName" runat="server" Text="Document Name:"></asp:Label></strong>
                               
                                    </td>
                                <td style="width:1%;">                                
                                </td>
                                <td style="width:30%;" colspan="4">
                                     <asp:Label ID="lblDocumentName2" runat="server" Width="100%"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td colspan="5">&nbsp;</td>
                            </tr>
                        </table>
                        <table width="90%">                                            
                            <tr>
                                <td>
                                    <asp:GridView ID="gvDocument" runat="server" AutoGenerateColumns="False" CellPadding="2" OnRowDataBound="gvDocument_RowDataBound" Width="100%" 
                        DataKeyNames="DOCUMENT_ID" GridLines="Horizontal">
                        <Columns>                           
                             <asp:TemplateField HeaderText="Distributor Name">
                             <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <%# Eval("DISTRIBUTOR_NAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                                                        
                            <asp:TemplateField HeaderText="Town">
                             <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <%# Eval("Town")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="View DateTime">
                                <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <%# Eval("TIME_STAMP2")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                               </asp:TemplateField>
                             <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfSTATUS_ID" runat="server" Value='<%# Eval("STATUS_ID") %>'  />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="HidePanel" />
                                    <ItemStyle CssClass="HidePanel" />
                               </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <HeaderStyle BackColor="#006699" ForeColor="White"/>
                       
                    </asp:GridView>
                                </td>
                            </tr>   
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="Button" 
                                        onclick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>                                                                  
                    </ContentTemplate>                    
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>                
</div>
</asp:Content>

