<%@ Page AutoEventWireup="true" CodeFile="CreateSeries.aspx.cs" Culture="en-SG"
    Inherits="Radiologist.CreateSeries" Language="C#" MasterPageFile="~/Site.master"
    Title="Start New Series" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <h2>
            Start New Series
        </h2>
        <p>
            Enter the patient NRIC to begin
        </p>
    </div>
    <%-- Error Notifications --%>
    <div class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        <asp:ValidationSummary runat="server" ValidationGroup="ImagingValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <div>
        <%-- NRIC --%>
        <div>
            <asp:Label AssociatedControlID="NRIC" runat="server" Text="Patient NRIC: " />
            <asp:TextBox CssClass="textEntry" ID="NRIC" runat="server" ValidationGroup="ImagingValidationGroup" />
            <asp:RequiredFieldValidator ControlToValidate="NRIC" CssClass="failureNotification"
                Display="Dynamic" ErrorMessage="Please enter patient NRIC." runat="server" ToolTip="Please enter patient NRIC."
                ValidationGroup="ImageValidationGroup">
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
                ErrorMessage="NRIC not found." OnServerValidate="NricExists" runat="server" ToolTip="NRIC not found."
                ValidationGroup="ImagingValidationGroup">
                <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                NRIC not found.
            </asp:CustomValidator>
        </div>
        <%-- / NRIC --%>
    </div>
    <div class="topPadding">
        <asp:Button CssClass="buttons" OnClick="CreateButtonClick" runat="server" Text="Retrieve Imaging Orders"
            ValidationGroup="ImagingValidationGroup" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Radiologist/Default.aspx" runat="server"
            Text="Cancel" />
    </div>
</asp:Content>
