<%@ Page AutoEventWireup="true" CodeFile="UpdateUser.aspx.cs" Culture="en-SG" Inherits="Admin_UpdateUser"
    Language="C#" MasterPageFile="~/Site.master" Title="Manage User Roles" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Manage User Roles
    </h2>
    <p>
        Update the roles of a user. Please note that only users are allowed to update their
        own particulars after account creation.
    </p>
    <%-- Error Notifications --%>
    <asp:Panel runat="server">
        <asp:ValidationSummary CssClass="failureNotification" runat="server"
            ValidationGroup="UpdateUserValidationGroup" />
    </asp:Panel>
    <%-- / Error Notifications --%>
    <h3>
        Please provide your username
    </h3>
    <%-- Username --%>
    <asp:Panel runat="server">
        <asp:Label AssociatedControlID="UserName" runat="server" Text="User Name: " />
        <asp:TextBox CssClass="textEntry" ID="UserName" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="UserName" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="Please enter your user name." runat="server"
            ToolTip="Please enter your user name." ValidationGroup="UpdateUserValidationGroup"
            SetFocusOnError="True">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please enter your user name.
        </asp:RequiredFieldValidator>
        <asp:CustomValidator ControlToValidate="UserName" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="User name not found." OnServerValidate="UserNameExists"
            runat="server" ToolTip="User name not found." ValidationGroup="UpdateUserValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            User name not found.
        </asp:CustomValidator>
    </asp:Panel>
    <%-- / Username --%>
    <asp:Panel class="topPadding" runat="server">
        <asp:Button CssClass="buttons" runat="server" Text="Next" ValidationGroup="UpdateUserValidationGroup"
            OnClick="NextButtonClick" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
    </asp:Panel>
</asp:Content>
