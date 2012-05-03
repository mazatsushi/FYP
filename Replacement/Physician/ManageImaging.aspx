﻿<%@ Page AutoEventWireup="true" CodeFile="ManageImaging.aspx.cs" Culture="en-SG"
    Inherits="Physician.ManageImaging" Language="C#" MasterPageFile="~/Site.master"
    Title="Manage Imaging" %>

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
                        <asp:TextBox CssClass="textEntry" ID="DatePicker" runat="server" />
                        <asp:ImageButton ID="DOB_Cal" ImageUrl="~/Images/icons/calendar.png" runat="server" />
                        <ajaxToolkit:CalendarExtender ClearTime="True" DaysModeTitleFormat="d MMM yyyy" Enabled="True"
                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy HH:mm" ID="CalendarExtender" PopupButtonID="DOB_Cal"
                            PopupPosition="TopLeft" runat="server" TargetControlID="DatePicker" TodaysDateFormat="d MMM yyyy" />
                        <ajaxToolkit:MaskedEditExtender AutoComplete="False" ClearTextOnInvalid="False" ClipboardEnabled="False"
                            CultureName="en-SG" Enabled="True" ID="DOB_Input" Mask="99/99/9999 99:99" MaskType="DateTime"
                            runat="server" TargetControlID="DatePicker" />
                        <asp:RequiredFieldValidator ControlToValidate="DatePicker" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify a date and time." runat="server"
                            ToolTip="Please specify a date and time." ValidationGroup="AddValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please specify a date and time.
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="DatePicker" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify a valid date and time." runat="server"
                            ToolTip="Please specify a valid date and time." ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](20\d\d)\s(([0-9])|([0-1][0-9])|([2][0-3])):(([0-9])|([0-5][0-9]))$"
                            ValidationGroup="AddValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please specify a valid date and time.
                        </asp:RegularExpressionValidator>
                        <asp:CustomValidator ControlToValidate="DatePicker" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify a valid date and time." OnServerValidate="DateRangeCheck"
                            runat="server" ToolTip="Please specify a valid date and time." ValidationGroup="AddValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please specify a valid date and time.
                        </asp:CustomValidator>
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
