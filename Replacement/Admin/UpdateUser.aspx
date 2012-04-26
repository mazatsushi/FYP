<%@ Page AutoEventWireup="true" CodeFile="UpdateUser.aspx.cs" Culture="en-SG" Inherits="Admin.UpdateUser"
    Language="C#" MasterPageFile="~/Site.master" Title="Manage User Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2>
                Manage User Roles
            </h2>
            <p>
                Update the roles of a user.
            </p>
            <%-- Error Notifications --%>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" CssClass="failureNotification" runat="server" ValidationGroup="UpdateUserValidationGroup" />
            </div>
            <%-- / Error Notifications --%>
            <h3>
                Please provide the username.
            </h3>
            <%-- Username --%>
            <div>
                <asp:Label ID="Label1" AssociatedControlID="UserName" runat="server" Text="User Name: " />
                <asp:TextBox CssClass="textEntry" ID="UserName" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter your user name." runat="server"
                    ToolTip="Please enter your user name." ValidationGroup="UpdateUserValidationGroup"
                    SetFocusOnError="True">
                    <asp:Image ID="Image1" ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your user name.
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" ControlToValidate="UserName" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="User name not found." OnServerValidate="UserNameExists"
                    runat="server" ToolTip="User name not found." ValidationGroup="UpdateUserValidationGroup">
                    <asp:Image ID="Image2" ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    User name not found.
                </asp:CustomValidator>
            </div>
            <%-- / Username --%>
            <div class="topPadding">
                <asp:Button ID="Button1" CssClass="buttons" runat="server" Text="Next" ValidationGroup="UpdateUserValidationGroup"
                    OnClick="NextButtonClick" />
                <asp:Button ID="Button2" CssClass="buttons" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
