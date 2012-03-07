<%@ Page AutoEventWireup="true" CodeFile="ResetPasswordStep2.aspx.cs" Culture="en-SG"
    Inherits="Guest_ResetPasswordStep2" Language="C#" MasterPageFile="~/Site.master"
    Title="Reset Password" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Reset Password
    </h2>
    <div>
        <%-- Error Notifications --%>
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        </span>
        <asp:ValidationSummary CssClass="failureNotification" runat="server" ValidationGroup="RegisterUserValidationGroup" />
        <%-- / Error Notifications --%>
    </div>
    <h3>
        Please answer the following security question to reset your password.
    </h3>
    <div>
        <%-- Security Question --%>
        <div>
            <asp:Label ID="Question" runat="server" />
        </div>
        <%-- / Security Question --%>
        <%-- Security Answer --%>
        <div class="padding">
            <asp:Label AssociatedControlID="Answer" runat="server" Text="Security Answer:" />
            <asp:TextBox CssClass="textEntry" ID="Answer" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="Answer" CssClass="failureNotification"
                Display="Dynamic" ErrorMessage="Security answer is required." runat="server"
                ToolTip="Security answer is required." ValidationGroup="RegisterUserValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Security answer is required.
            </asp:RequiredFieldValidator>
        </div>
        <%-- / Security Answer --%>
    </div>
    <br />
    <div>
        <asp:Button CssClass="buttons" runat="server" Text="Reset Password" ValidationGroup="RegisterUserValidationGroup"
            OnClick="ResetButtonClick" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
    </div>
</asp:Content>
