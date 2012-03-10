<%@ Page AutoEventWireup="true" CodeFile="UpdatePatientBlood.aspx.cs" Culture="en-SG"
    Inherits="Physician_UpdatePatientBlood" Language="C#" MasterPageFile="~/Site.master"
    Title="Update Patient Blood Type" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Update Patient Blood Type
    </h2>
    <%-- Error Notifications --%>
    <asp:Panel CssClass="failureNotification" runat="server">
        <asp:Literal ID="ErrorMessage" runat="server" />
        <asp:ValidationSummary runat="server" ValidationGroup="PatientValidationGroup" />
    </asp:Panel>
    <%-- / Error Notifications --%>
    <asp:Panel runat="server">
        <asp:Label AssociatedControlID="NRIC" runat="server" Text="Patient NRIC: " />
        <asp:TextBox CssClass="textEntry" ID="NRIC" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="NRIC" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="Please enter patient's NRIC." runat="server"
            ToolTip="Please enter patient's NRIC." ValidationGroup="PatientValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please enter patient's NRIC.
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="NRIC" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="Please enter a valid NRIC." runat="server" ToolTip="Please enter a valid NRIC."
            ValidationExpression="^[SFTG]\d{7}[A-Z]$" ValidationGroup="PatientValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please enter a valid NRIC.
        </asp:RegularExpressionValidator>
        <asp:CustomValidator ControlToValidate="NRIC" CssClass="failureNotification" Display="Dynamic"
            ErrorMessage="NRIC cannot be found." OnServerValidate="NricExists" runat="server"
            ToolTip="NRIC cannot be found." ValidationGroup="PatientValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            NRIC cannot be found.
        </asp:CustomValidator>
    </asp:Panel>
    <asp:Panel CssClass="topPadding" runat="server">
        <asp:Button CssClass="buttons" OnClick="RetrieveButtonClick" runat="server" Text="Retrieve Patient"
            ValidationGroup="PatientValidationGroup" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/Default.aspx" runat="server"
            Text="Cancel" />
    </asp:Panel>
</asp:Content>
