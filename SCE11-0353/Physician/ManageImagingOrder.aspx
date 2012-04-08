<%@ Page AutoEventWireup="true" CodeFile="ManageImagingOrder.aspx.cs" Culture="en-SG"
    Inherits="Physician.ManageImagingOrder" Language="C#" MasterPageFile="~/Site.master"
    Title="Managing Imaging Studies" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <h2>
            Manage Imaging Order
        </h2>
        <p>
            Start by retrieving all imaging orders of a patient
        </p>
    </div>
    <%-- Error Notifications --%>
    <div class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        <asp:ValidationSummary runat="server" ValidationGroup="ImageValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <div>
        <%-- NRIC --%>
        <div>
            <asp:Label AssociatedControlID="NRIC" runat="server" Text="Patient NRIC: " />
            <asp:TextBox CssClass="textEntry" ID="NRIC" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="NRIC" CssClass="failureNotification"
                Display="Dynamic" ErrorMessage="Please enter patient NRIC." runat="server" ToolTip="Please enter patient NRIC."
                ValidationGroup="ImageValidationGroup">
                <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                Please enter patient NRIC.
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="NRIC" CssClass="failureNotification"
                Display="Dynamic" ErrorMessage="Please enter a valid NRIC." runat="server" ToolTip="Please enter a valid NRIC."
                ValidationExpression="^[SFTG]\d{7}[A-Z]$" ValidationGroup="ImageValidationGroup">
                <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                Please enter a valid NRIC.
            </asp:RegularExpressionValidator>
            <asp:CustomValidator ControlToValidate="NRIC" CssClass="failureNotification" Display="Dynamic"
                ErrorMessage="NRIC not found." OnServerValidate="NricExists" runat="server" ToolTip="NRIC not found."
                ValidationGroup="ImageValidationGroup">
                <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                NRIC not found.
            </asp:CustomValidator>
        </div>
        <%-- / NRIC --%>
        <div class="topPadding">
            <asp:Button CssClass="buttons" OnClick="RetrieveButtonClick" runat="server" Text="Retrieve Imaging Orders"
                ValidationGroup="ImagingValidationGroup" />
            <asp:Button CssClass="buttons" PostBackUrl="~/Physician/Default.aspx" runat="server"
                Text="Cancel" />
        </div>
    </div>
</asp:Content>
