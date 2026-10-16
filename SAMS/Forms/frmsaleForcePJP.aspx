<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmsaleForcePJP.aspx.cs" Inherits="Forms_frmsaleForcePJP"
    Title="SAMS: Permanent Journy Program" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $find("CEStartDate")._popupDiv.style.zIndex = 100000000;
            $("select").searchable();
          
          
        }

        function ChPrincipalListSelect() {
            var chkBoxList = document.getElementById('<%= cblDate.ClientID %>');
            var chkBox = document.getElementById('<%= ChPrincipal.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }

        function UnCheckPrincipalAll() {
            var chkBox = document.getElementById('<%= ChPrincipal.ClientID %>');
            var chkBoxList = document.getElementById('<%= cblDate.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
        }

    </script>
    <div id="right_data">

        <div style="z-index: 101; left: 712px; width: 100px; position: absolute; top: 129px; height: 100px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                        Wait Update
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <table width="100%">
            <tr>
                <td align="left" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%" cellspacing="5">
                                <tr>
                                    <td style="width: 10%">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Text="Location"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10%">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="Order Booker"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:DropDownList ID="DrpSaleForce" runat="server" Width="200px"
                                            AutoPostBack="True" OnSelectedIndexChanged="DrpSaleForce_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Text="Date From"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10"
                                            onkeyup="BlockStartDateKeyPress()" Width="178px" OnTextChanged="txtStartDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                            TargetControlID="txtStartDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 10%">
                                        <strong>
                                            <asp:Label ID="Label7" runat="server" Width="110px" Text="Date To"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10"
                                            onkeyup="BlockEndDateKeyPress()" Width="178px" OnTextChanged="txtEndDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                            TargetControlID="txtEndDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">
                                        <strong>
                                            <asp:CheckBox ID="cbAlternate" runat="server" Text="Alternate" AutoPostBack="true"
                                                OnCheckedChanged="cbAlternate_CheckedChanged" Checked="true" />
                                        </strong>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:RadioButtonList ID="rblAlternate" runat="server" RepeatDirection="Horizontal" Width="200" Visible="false"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblAlternate_SelectedIndexChanged">
                                            <asp:ListItem Text="Weekly" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Monthly" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width: 10%"></td>
                                    <td style="width: 50%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" valign="top">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" Text="Section"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 30%" valign="top">
                                        <asp:DropDownList ID="ddlSection" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10%" valign="top">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" Text="Date List"></asp:Label></strong>
                                    </td>
                                    <td style="width: 50%" valign="top">
                                        <asp:CheckBox ID="ChPrincipal" runat="server" Font-Size="8pt" onclick="ChPrincipalListSelect()"
                                            Text="Select All" Width="75px" />
                                        <br />
                                        <asp:Panel ID="Panel2" runat="server" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px"
                                            Height="160px" ScrollBars="Vertical" Width="200px" BackColor="White" CssClass="pnlCheckboxlist">
                                            <asp:CheckBoxList ID="cblDate" CssClass="chkboxlist" runat="server" onclick="UnCheckPrincipalAll();" Width="100%">
                                                <asp:ListItem Text="27-May-19, Monday" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="28-May-19, Tuesday" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="29-May-19, Wednesday" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="30-May-19, Thrusday" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="31-May-19, Friday" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="01-June-19, Saturday" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="02-June-19, Sunday" Value="7"></asp:ListItem>
                                            </asp:CheckBoxList>
                                            <div style="z-index: 101; left: -97px; width: 100px; position: absolute; top: 216px; height: 100px">
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Save"
                                            ValidationGroup="vg" Width="120px" CssClass="Button" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel3" runat="server" Height="230px" ScrollBars="Vertical" Width="100%" BorderWidth="1px">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdPJP" runat="server" Width="100%" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" BorderColor="White" BackColor="White" HorizontalAlign="Center"
                                                        AutoGenerateColumns="False" OnRowEditing="grdPJP_RowEditing" OnRowDeleting="grdPJP_RowDeleting">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                                        <Columns>
                                                            <asp:BoundField DataField="PJP_MASTER_ID" HeaderText="PJP_MASTER_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ORDER_BOOKER_ID" HeaderText="ORDER_BOOKER_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DATE_TYPE" HeaderText="DATE_TYPE">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ORDER_BOOKER_NAME" HeaderText="Order Booker">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AREA_NAME" HeaderText="Section">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="VISIT_FREQUENCY" HeaderText="Visit frequency">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="START_DATE" HeaderText="Effective Start Date" DataFormatString="{0:dd-MM-yyyy}">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="END_DATE" HeaderText="Effective End Date" DataFormatString="{0:dd-MM-yyyy}">
                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:BoundField>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
