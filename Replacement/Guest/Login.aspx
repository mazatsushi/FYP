<%@ Page AutoEventWireup="true" CodeFile="Login.aspx.cs" Culture="en-SG" Inherits="Guest.Login"
    Language="C#" MasterPageFile="~/Site.master" Title="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
    </p>
    <p>
        Please
        <asp:HyperLink ID="HyperLink1" EnableViewState="false" NavigateUrl="~/Guest/Register.aspx" runat="server"
            Text="register" />
        if you don't have an account.
    </p>
    <asp:Login BackColor="#F5F5F5" EnableViewState="True" Font-Names="Verdana" Font-Size="1em"
        ForeColor="#333333" ID="LoginUser" OnLoggedIn="OnLoggedIn" runat="server">
        <LayoutTemplate>
            <div class="failureNotification">
                <asp:Literal ID="FailureText" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValidationGroup" />
            </div>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Account Information</legend>
                    <div>
                        <asp:Label ID="Label1" AssociatedControlID="UserName" runat="server" Text="Username: " />
                        <asp:TextBox CssClass="textEntry" ID="UserName" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" CssClass="failureNotification"
                            ErrorMessage="Username is required." runat="server" ToolTip="Username is required."
                            ValidationGroup="ValidationGroup">
                            <asp:Image ID="Image1" ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> Username is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:Label ID="Label2" AssociatedControlID="Password" runat="server" Text="Password: " />
                        <asp:TextBox CssClass="passwordEntry" ID="Password" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Password" CssClass="failureNotification"
                            ErrorMessage="Password is required." runat="server" ToolTip="Password is required."
                            ValidationGroup="ValidationGroup">
                            <asp:Image ID="Image2" ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> Password is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:CheckBox ID="RememberMe" runat="server" />
                        <asp:Label AssociatedControlID="RememberMe" CssClass="inline" ID="RememberMeLabel"
                            runat="server">Keep me logged in</asp:Label>
                    </div>
                    <div id="Div1" class="submitButton, topPadding" runat="server">
                        <asp:Button ID="Button1" CommandName="Login" CssClass="buttons" runat="server" Text="Log In" ValidationGroup="ValidationGroup" />
                    </div>
                </fieldset>
                <div>
                    <asp:LinkButton ID="LinkButton1" CausesValidation="False" PostBackUrl="~/Guest/ResetPassword.aspx"
                        runat="server" Text="Forgot Your Password?" />
                </div>
            </div>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
