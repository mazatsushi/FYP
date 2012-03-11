<%@ Page Title="Appeal For Account Unlock" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="AppealUnlock.aspx.cs" Inherits="Guest_AppealUnlock"
    Culture="en-SG" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Appeal to Unlock Account
    </h2>
    <%-- Error Notifications --%>
    <div CssClass="failureNotification" runat="server">
        <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        <asp:ValidationSummary runat="server" ValidationGroup="AppealValidationGroup" />
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
            ToolTip="Please enter your user name." ValidationGroup="AppealValidationGroup"
            SetFocusOnError="True">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please enter your user name.
        </asp:RequiredFieldValidator>
        <asp:CustomValidator ControlToValidate="UserName" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="User name not found." OnServerValidate="UserNameExists"
            runat="server" ToolTip="User name not found." ValidationGroup="AppealValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            User name not found.
        </asp:CustomValidator>
    </div>
    <%-- / Username --%>
    <div class="topPadding">
        <asp:Button CssClass="buttons" runat="server" Text="Next" ValidationGroup="AppealValidationGroup"
            OnClick="NextButtonClick" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
    </div>
</asp:Content>
