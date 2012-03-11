<%@ Page AutoEventWireup="true" CodeFile="UpdatePatientBloodType.aspx.cs" Culture="en-SG"
    Inherits="Physician_UpdatePatientBloodType" Language="C#" MasterPageFile="~/Site.master"
    Title="Update Patient Blood Type" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Update Patient Blood Type
    </h2>
    <%-- Error Notifications --%>
    <div class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" />
        <asp:ValidationSummary runat="server" ValidationGroup="PatientValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <%-- NRIC --%>
    <div>
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
        <asp:CustomValidator ControlToValidate="NRIC" CssClass="failureNotification" Display="Dynamic"
            ErrorMessage="User specified is not a patient." OnServerValidate="IsPatient"
            runat="server" ToolTip="User specified is not a patient." ValidationGroup="PatientValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            User specified is not a patient.
        </asp:CustomValidator>
    </div>
    <%-- NRIC --%>
    <div class="topPadding">
        <asp:Button CssClass="buttons" OnClick="RetrieveButtonClick" runat="server" Text="Retrieve Patient"
            ValidationGroup="PatientValidationGroup" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/Default.aspx" runat="server"
            Text="Cancel" />
    </div>
</asp:Content>
