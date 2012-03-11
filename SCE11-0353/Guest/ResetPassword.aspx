<%@ Page AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Culture="en-SG"
    Inherits="Account_ResetPassword" Language="C#" MasterPageFile="~/Site.master"
    Title="Reset Password" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Reset Password
    </h2>
    <%-- Error Notifications --%>
    <div>
        <asp:ValidationSummary CssClass="failureNotification" runat="server" ValidationGroup="ResetPasswordValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <h3>
        Please provide your username
    </h3>
    <%-- Username --%>
    <div>
        <asp:Label AssociatedControlID="UserName" runat="server" Text="User Name: " />
        <asp:TextBox CssClass="textEntry" ID="UserName" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="UserName" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="Please enter your user name." runat="server"
            ToolTip="Please enter your user name." ValidationGroup="ResetPasswordValidationGroup"
            SetFocusOnError="True">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please enter your user name.
        </asp:RequiredFieldValidator>
        <asp:CustomValidator ControlToValidate="UserName" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="User name not found." OnServerValidate="UserNameExists"
            runat="server" ToolTip="User name not found." ValidationGroup="ResetPasswordValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            User name not found.
        </asp:CustomValidator>
    </div>
    <%-- / Username --%>
    <div class="topPadding">
        <asp:Button CssClass="buttons" runat="server" Text="Next" ValidationGroup="ResetPasswordValidationGroup"
            OnClick="NextButtonClick" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
    </div>
</asp:Content>
