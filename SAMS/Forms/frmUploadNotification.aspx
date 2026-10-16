<%@ Page Title="SAMS: Upload Notifications" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmUploadNotification.aspx.cs" Inherits="Forms_frmUploadNotification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <script language="javascript" type="text/javascript">
            function HeaderClick(CheckBox) {
                //Get target base & child control.
                var TargetBaseControl = document.getElementById('<%= this.cblLocation.ClientID %>');
                var TargetChildControl = "cbAll";


                //Get all the control of the type INPUT in the base control.
                var Inputs = TargetBaseControl.getElementsByTagName("input");

                //Checked/Unchecked all the checkBoxes in side the GridView.
                for (var n = 0; n < Inputs.length; ++n)

                    if (Inputs[n].type == 'checkbox')

                        if (!Inputs[n].disabled) {
                            Inputs[n].checked = CheckBox.checked;
                        }

                //Reset Counter
                Counter = CheckBox.checked ? TotalChkBx : 0;

            }
        </script>
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="31px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                                    PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td style="width: 15%" align="right">
                                        <strong>
                                            <asp:Label ID="lblSubject" runat="server" Text="Subject:"></asp:Label></strong>
                                    </td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 35%">
                                        <asp:TextBox ID="txtSubject" runat="server" Width="298px"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 15%" align="right">
                                        <strong>
                                            <asp:Label ID="lblRegion" runat="server" Text="Region:"></asp:Label></strong>
                                    </td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 35%">
                                        <asp:DropDownList ID="ddlRegion" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td><strong>Notification:</strong></td>
                                </tr>
                                <tr>
                                    <td style="width: 15%" align="right"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 35%">
                                        <br />
                                        &nbsp;
                                        <asp:CheckBox ID="cbAll" runat="server" Text="All Location" onclick="javascript:HeaderClick(this);" />
                                        <asp:Panel ID="pnlLocation" runat="server" BorderColor="Silver" BorderWidth="1"
                                            ScrollBars="Vertical" Width="300px" Height="300px">
                                            <asp:CheckBoxList ID="cblLocation" runat="server" Width="100%" RepeatLayout="Table">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <br />


                                        <br />
                                        <asp:TextBox runat="server" ID="txtRemarks" Width="300px" Height="290px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right"><strong>
                                        <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="90px"></asp:Label>
                                    </strong></td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                            onkeyup="BlockStartDateKeyPress()" Width="150px"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" /></td>

                                </tr>
                                <tr>
                                    <td align="right"><strong>
                                        <asp:Label ID="Label1" runat="server" Height="13px" Text="To Date" Width="90px"></asp:Label>
                                    </strong></td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                            onkeyup="BlockEndDateKeyPress()" Width="150px"></asp:TextBox>
                                        <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                            TargetControlID="txtStartDate">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                            TargetControlID="txtEndDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%" align="right"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 35%">
                                        <br />
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="Button"
                                            OnClick="btnUpload_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvDocument" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                            OnRowCommand="gvDocument_RowCommand" OnRowDataBound="gvDocument_RowDataBound" Width="100%"
                                            DataKeyNames="DOCUMENT_ID" GridLines="Horizontal"
                                            EmptyDataText="<div style='padding:10px; margin:10px; background-color:#fbefef; border: Solid 1px #bb0000; color:#bb0000;'>There is no document to display.</div>">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Subject">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton AlternateText="download" ID="lnkViewDoc" runat="server" CommandName="download" CommandArgument='<%# Eval("DOCUMENT_ID")%>' ToolTip="Download Document">
                                                                         <%# Eval("SUBJECT")%>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Document Name">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%# Eval("DOCUMENT_NAME")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Uploaded By">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%# Eval("USER_NAME")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Upload DateTime">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%# Eval("TIME_STAMP")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="From Date">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%# Eval("FROM_DATE")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="To Date">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%# Eval("to_date")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton AlternateText="View" ToolTip="View Detail" ID="btnViewDoc" runat="server"
                                                                        ImageUrl="~/images/view.gif" CommandName="view" CommandArgument='<%# Eval("DOCUMENT_ID")%>' />
                                                                </td>
                                                                <td width="2%">&nbsp;</td>
                                                                <td>
                                                                    <asp:ImageButton AlternateText="Delete" ToolTip="Delete Document" ID="btnDel" runat="server"
                                                                        ImageUrl="~/images/delete.gif" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this document?');"
                                                                        CommandName="del" CommandArgument='<%# Eval("DOCUMENT_ID")%>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <HeaderStyle BackColor="#006699" ForeColor="White" />
                                        </asp:GridView>

                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

