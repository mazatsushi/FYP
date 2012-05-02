<%@ Page AutoEventWireup="true" CodeFile="ManageImaging.aspx.cs" Culture="en-SG"
    Inherits="Physician.ManageImaging" Language="C#" MasterPageFile="~/Site.master"
    Title="Manage Imaging" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Manage Patient Medical Imaging History
            </h2>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" />
                <asp:ValidationSummary runat="server" ValidationGroup="AddValidationGroup" />
                <asp:ValidationSummary runat="server" ValidationGroup="RemoveValidationGroup" />
            </div>
            <%-- / Error Notifications --%>
            <div>
                <h3>
                    Patient Name:
                    <asp:Label ID="PatientName" runat="server" />
                </h3>
            </div>
            <div class="topPadding">
                <fieldset>
                    <legend>Imaging History</legend>
                    <div>
                        <asp:GridView CellPadding="4" ForeColor="#333333" GridLines="None" ID="AllStudies"
                            runat="server">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:Label ID="Some" runat="server" Text="Replace with GridView" />
                    </div>
                    <asp:Label ID="None" runat="server" Text="None" />
                </fieldset>
            </div>
            <div class="topPadding">
                <fieldset>
                    <legend>Schedule a New Imaging Appointment</legend>
                    <div>
                        <asp:Label AssociatedControlID="DatePicker" runat="server" Text="* Date & Time (24H): " />
                        <eo:DatePicker ID="DatePicker" runat="server" ControlSkinID="None" DayCellHeight="15"
                            DayCellWidth="31" DayHeaderFormat="Short" FirstMonth="2012-01-01" MinValidDate="2012-01-01"
                            OtherMonthDayVisible="True" PickerFormat="MM/dd/yyyy HH:mm" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL"
                            TitleRightArrowImageUrl="DefaultSubMenuIcon" VisibleDate="2012-05-01">
                            <CalendarStyle CssText="background-color:white;border-bottom-color:Silver;border-bottom-style:solid;border-bottom-width:1px;border-left-color:Silver;border-left-style:solid;border-left-width:1px;border-right-color:Silver;border-right-style:solid;border-right-width:1px;border-top-color:Silver;border-top-style:solid;border-top-width:1px;color:#2C0B1E;padding-bottom:5px;padding-left:5px;padding-right:5px;padding-top:5px;" />
                            <TitleStyle CssText="font-family:Verdana;font-size:8.75pt;padding-bottom:5px;padding-left:5px;padding-right:5px;padding-top:5px;" />
                            <TitleArrowStyle CssText="cursor: hand" />
                            <MonthStyle CssText="cursor:hand;margin-bottom:0px;margin-left:4px;margin-right:4px;margin-top:0px;" />
                            <DayHeaderStyle CssText="font-family:Verdana;font-size:8pt;border-bottom: #f5f5f5 1px solid" />
                            <DayStyle CssText="font-family:Verdana;font-size:8pt;" />
                            <DayHoverStyle CssText="font-family:Verdana;font-size:8pt;background-image:url('00040402');color:#1c7cdc;" />
                            <TodayStyle CssText="font-family:Verdana;font-size:8pt;background-image:url('00040401');color:#1176db;" />
                            <SelectedDayStyle CssText="font-family:Verdana;font-size:8pt;background-image:url('00040403');color:Brown;" />
                            <DisabledDayStyle CssText="font-family:Verdana;font-size:8pt;color: gray" />
                            <FooterTemplate>
                                <table border="0" cellpadding="0" cellspacing="5" style="font-size: 11px; font-family: Verdana">
                                    <tr>
                                        <td width="30">
                                        </td>
                                        <td valign="center">
                                            <img src="{img:00040401}" />
                                        </td>
                                        <td valign="center">
                                            Today: {var:today:dd/MM/yyyy}
                                        </td>
                                    </tr>
                                </table>
                            </FooterTemplate>
                        </eo:DatePicker>
                    </div>
                    <div class="topPadding" id="newStudyDiv" runat="server">
                        <asp:Label AssociatedControlID="Description" runat="server" Text="* Description / Purpose of Study: " />
                        <div>
                            <asp:TextBox CssClass="textEntry" Height="180" ID="Description" runat="server" TextMode="MultiLine"
                                Width="250" />
                        </div>
                    </div>
                    <div class="topPadding" id="existingStudyDiv" runat="server">
                        <asp:Label AssociatedControlID="ExistingId" runat="server" Text="* ID of Current Open Study: " />
                        <asp:TextBox CssClass="textEntry" Enabled="False" ID="ExistingId" runat="server" />
                    </div>
                    <div class="topPadding">
                        <asp:Button CssClass="buttons" runat="server" Text="Schedule Appointment" OnClick="AddButtonClick"
                            ValidationGroup="AddValidationGroup" />
                    </div>
                </fieldset>
            </div>
            <div>
                <fieldset>
                    <legend>Close Study</legend>TODO
                </fieldset>
            </div>
            <div class="topPadding">
                <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Previous Page"
                    runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
                    runat="server" Text="Return to Physician Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
