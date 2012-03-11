<%@ Page AutoEventWireup="true" CodeFile="CreateImagingOrder.aspx.cs" Culture="en-SG"
    Inherits="Physician_CreateImagingOrder" Language="C#" MasterPageFile="~/Site.master"
    Title="Create Imaging Order" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel runat="server">
        <h2>
            Create a New Imaging Order
        </h2>
        <p>
            Use the form below to create a new imaging order for a patient.
        </p>
    </asp:Panel>
    <%-- Error Notifications --%>
    <asp:Panel CssClass="failureNotification" runat="server">
        <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        <asp:ValidationSummary runat="server" ValidationGroup="ImagingValidationGroup" />
    </asp:Panel>
    <%-- / Error Notifications --%>
    <%-- Study Information --%>
    <fieldset>
        <legend>Imaging Order Information</legend>
        <asp:Panel runat="server">
            <%-- NRIC --%>
            <asp:Panel runat="server">
                <asp:Label AssociatedControlID="NRIC" runat="server" Text="Patient NRIC: " />
                <asp:TextBox CssClass="textEntry" ID="NRIC" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="NRIC" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter patient NRIC." runat="server" ToolTip="Please enter patient NRIC."
                    ValidationGroup="ImagingValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter patient NRIC.
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="NRIC" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter a valid NRIC." runat="server" ToolTip="Please enter a valid NRIC."
                    ValidationExpression="^[SFTG]\d{7}[A-Z]$" ValidationGroup="ImagingValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter a valid NRIC.
                </asp:RegularExpressionValidator>
                <asp:CustomValidator ControlToValidate="NRIC" CssClass="failureNotification" Display="Dynamic"
                    ErrorMessage="NRIC not found." OnServerValidate="NricExists" runat="server"
                    ToolTip="NRIC not found." ValidationGroup="ImagingValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    NRIC not found.
                </asp:CustomValidator>
                <asp:CustomValidator ControlToValidate="NRIC" CssClass="failureNotification" Display="Dynamic"
                    ErrorMessage="Patient still has uncompleted studies." OnServerValidate="HasOpenStudies" runat="server"
                    ToolTip="Patient still has uncompleted studies." ValidationGroup="ImagingValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Patient still has uncompleted studies.
                </asp:CustomValidator>
            </asp:Panel>
            <%-- / NRIC --%>
            <%-- Description --%>
            <asp:Panel CssClass="topPadding" runat="server">
                <asp:Label AssociatedControlID="Description" runat="server" Text="Description:" />
                <asp:Panel runat="server">
                    <asp:TextBox CssClass="float textEntry" ID="Description" runat="server" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator ControlToValidate="Description" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Please enter a description." runat="server" ToolTip="Please enter a description."
                        ValidationGroup="ImagingValidationGroup">
                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Please enter a description.
                    </asp:RequiredFieldValidator>
                </asp:Panel>
            </asp:Panel>
            <%-- / Description --%>
        </asp:Panel>
    </fieldset>
    <%-- / Study Information --%>
    <%-- Appointment Information --%>
    <fieldset>
        <legend>Appointment Information</legend>
        <asp:Panel runat="server">
            <asp:Panel runat="server">
                <asp:Panel runat="server">
                    <asp:Label AssociatedControlID="Date" runat="server" Text="Appointment Date (dd/mm/yyyy): " />
                    <asp:TextBox CssClass="textEntry" ID="Date" runat="server" />
                    <asp:ImageButton ID="Cal" ImageUrl="~/Images/icons/calendar.png" runat="server" />
                    <ajaxToolkit:CalendarExtender ClearTime="True" DaysModeTitleFormat="dd/MM/yyyy" Enabled="True"
                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="Cal" runat="server"
                        StartDate="1/1/1940" TargetControlID="Date" TodaysDateFormat="dd/MM/yyyy" PopupPosition="TopLeft" />
                    <ajaxToolkit:MaskedEditExtender AutoComplete="False" ClearTextOnInvalid="False" ClipboardEnabled="False"
                        CultureName="en-SG" Enabled="True" ID="Date_Input" Mask="99/99/9999" MaskType="Date"
                        runat="server" TargetControlID="Date" />
                    <ajaxToolkit:MaskedEditValidator ControlExtender="Date_Input" ControlToValidate="Date"
                        CssClass="failureNotification" Display="Dynamic" EmptyValueBlurredText="<img src='../Images/icons/error.png'> Please specify an appointment date."
                        EmptyValueMessage="Please specify an appointment date." InvalidValueBlurredMessage="<img src='../Images/icons/error.png'> Please specify a valid date."
                        InvalidValueMessage="Please specify a valid date." IsValidEmpty="False" runat="server"
                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                        ValidationGroup="ImagingValidationGroup" />
                    <asp:RangeValidator ControlToValidate="Date" CssClass="failureNotification" ErrorMessage="Date is too far in the future."
                        ID="DateRangeCheck" runat="server" Type="Date" ValidationGroup="ImagingValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Date is too far in the future.
                    </asp:RangeValidator>
                </asp:Panel>
                <asp:Panel CssClass="topPadding" runat="server">
                    <asp:Label runat="server" Text="Appointment Time: " />
                    <asp:DropDownList ID="Hour" runat="server" />
                    <asp:DropDownList ID="Minute" runat="server" />
                    <asp:DropDownList ID="AMPM" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </fieldset>
    <%-- / Appointment Information --%>
    <asp:Panel runat="server">
        <asp:Button CssClass="buttons" OnClick="CreateButtonClick" runat="server" Text="Create Imaging Order"
            ValidationGroup="ImagingValidationGroup" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/Default.aspx" runat="server"
            Text="Cancel" />
    </asp:Panel>
</asp:Content>
