<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmTownClasification.aspx.cs" Inherits="Forms_frmTownClasification" Title="SAMS: Town Classification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
           <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
     <script language="JavaScript" type="text/javascript">
        function pageLoad() {
           
            $("select").searchable();
        }
         </script>

    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                      <%--  <tr>
                                            <td colspan="2" align="left">
                                                <asp:Label ID="lblmsg" runat="server" Visible="False" Width="175px" ForeColor="Red"></asp:Label>&nbsp;
                                            </td>
                                            <td style="height: 17px; width: 94px;">
                                            </td>
                                            <td></td><td rowspan="5" valign="top">
                                               All Assigned Town
                                                <asp:Panel id="pnl" runat="server" Width="255px" Height="150px" BackColor="WindowFrame" BorderWidth="1px" >
                                                     <asp:ListBox ID="lstAssignedallTown" runat="server" Width="255px" Height="150px" 
                                                         CssClass="DropList" >
                                                </asp:ListBox>
                                                </asp:Panel>
                                            </td>
                                        </tr>--%>
                                     <%--   <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="58px" Text="Locaton"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 94px"></td>

                                            
                                            
                                        </tr>--%>
                                         <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="58px" Text="Classification"></asp:Label></strong>
                                            </td>
                                            <td align="left" colspan="2">
                                                <table><tr><td>
                                                <asp:DropDownList ID="drpClassification" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpClassification_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtClassificationName" runat="server" Visible="false" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                </td><td>
                                                    <asp:Button ID="btnNewClassification" runat="server" OnClick="btnNewClassification_Click" CssClass="Button" Width="50px" Text="New"  />
                                            </td></tr></table>
                                                </td>
                                           
                                        </tr>
                                           <tr >
                                       
                                        <td style="width: 95px" align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="78px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                       
                                        <td style="height: 25px" align="left">
                                            
                                           <%-- <asp:CheckBox ID="ChkSelectall" runat="server" Text="Select All" AutoPostBack="true" OnCheckedChanged="ChkSelectall_CheckedChanged" />
                                            <asp:Panel ID="pnlPrincipal" runat="server" CssClass="pnlCheckboxlist" ScrollBars="Auto" BorderWidth="1px" Width="200px" Height="100px">
                                                <asp:CheckBoxList ID="chbPrincipal" runat="server" AutoPostBack="true" Width="100%"  CssClass="chkboxlist"></asp:CheckBoxList>
                                            </asp:Panel>--%>
                                             <asp:DropDownList ID="chbPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" >
                                                </asp:DropDownList>
                                        </td> 
                                        
                                        <td></td>
                                    </tr>
                                        <tr>
                                            <td style="height: 25px;" align="left">
                                                <strong>
                                                    <asp:Label ID="Label11" runat="server" Width="56px" Text="Region" CssClass="lblbox"></asp:Label></strong>
                                            </td> 
                                            <td align="left">
                                                <asp:DropDownList ID="ddRegion" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddRegion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="43px" Text="Zone" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddZone" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddZone_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="51px" Text="Territory" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddTerritory" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddTerritory_SelectedIndexChanged">
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
                                                    <asp:Label ID="Label4" runat="server" Width="142px" Text="Unassigned Town" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 102px" valign="bottom" align="center">
                                            </td>
                                            <td style="width: 102px" rowspan="1">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Text="Assigned Town"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px" rowspan="4">
                                                <asp:Panel id="Panel1" runat="server" Width="300px" Height="250px" BackColor="WindowFrame" BorderWidth="1px" >
                                                <asp:ListBox ID="lstUnAssignTown" runat="server" Width="300px" Height="100%" 
                                                    CssClass="DropList">
                                                </asp:ListBox>
                                                    </asp:Panel>
                                            </td>
                                            <td valign="middle" align="center">
                                                <asp:Button ID="btnAssign" runat="server" Width="30px" Font-Size="8pt" Text=">" OnClick="btnAssign_Click"
                                                    CssClass="Button" />
                                            </td>
                                            <td style="width: 102px" rowspan="4">
                                                <asp:Panel id="Panel2" runat="server" Width="300px" Height="250px" BackColor="WindowFrame" BorderWidth="1px" >
                                                <asp:ListBox ID="lstAssignTown" runat="server" Width="300px" Height="100%" CssClass="DropList">
                                                </asp:ListBox>
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
                                            <td style="height: 24px" rowspan="1">
                                            </td>
                                            <td style="width: 102px; height: 24px">
                                            </td>
                                            <td style="height: 24px" rowspan="1">
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
    </div>
</asp:Content>
