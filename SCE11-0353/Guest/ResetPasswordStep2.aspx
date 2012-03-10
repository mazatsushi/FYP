<%@ Page AutoEventWireup="true" CodeFile="ResetPasswordStep2.aspx.cs" Culture="en-SG"
    Inherits="Guest_ResetPasswordStep2" Language="C#" MasterPageFile="~/Site.master"
    Title="Reset Password" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Reset Password
    </h2>
    <%-- Error Notifications --%>
    <asp:Panel CssClass="failureNotification" runat="server">
        <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        <asp:ValidationSummary runat="server" ValidationGroup="ResetPasswordValidationGroup" />
    </asp:Panel>
    <%-- / Error Notifications --%>
    <h3>
        Please answer the following security question to reset your password.
    </h3>
    <asp:Panel runat="server">
        <%-- Security Question --%>
        <asp:Panel runat="server">
            <asp:Label ID="Question" runat="server" />
        </asp:Panel>
        <%-- / Security Question --%>
        <%-- Security Answer --%>
        <asp:Panel class="topPadding" runat="server">
            <asp:Label AssociatedControlID="Answer" runat="server" Text="Security Answer:" />
            <asp:TextBox CssClass="textEntry" ID="Answer" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="Answer" CssClass="failureNotification"
                Display="Dynamic" ErrorMessage="Security answer is required." runat="server"
                ToolTip="Security answer is required." ValidationGroup="ResetPasswordValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Security answer is required.
            </asp:RequiredFieldValidator>
        </asp:Panel>
        <%-- / Security Answer --%>
    </asp:Panel>
    <asp:Panel CssClass="topPadding" runat="server">
        <asp:Button CssClass="buttons" runat="server" Text="Reset Password" ValidationGroup="ResetPasswordValidationGroup"
            OnClick="ResetButtonClick" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
    </asp:Panel>
</asp:Content>
