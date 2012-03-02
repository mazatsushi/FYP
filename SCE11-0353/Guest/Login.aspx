<%@ Page AutoEventWireup="true" CodeFile="Login.aspx.cs" Culture="en-SG" Inherits="Account_Login"
    Language="C#" MasterPageFile="~/Site.master" Title="Login" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <asp:HyperLink EnableViewState="false" NavigateUrl="~/Guest/Register.aspx" runat="server"
            Text="Register" />
        if you don't have an account.
    </p>
    <asp:Login BackColor="#F5F5F5" EnableViewState="True" Font-Names="Verdana" Font-Size="1em"
        ForeColor="#333333" ID="LoginUser" OnLoggedIn="OnLoggedIn" runat="server">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server" />
            </span>
            <asp:ValidationSummary CssClass="failureNotification" ID="LoginUserValidationSummary"
                runat="server" ValidationGroup="LoginUserValidationGroup" />
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Account Information</legend>
                    <div>
                        <asp:Label AssociatedControlID="UserName" runat="server" Text="Username: " />
                        <asp:TextBox CssClass="textEntry" ID="UserName" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="UserName" CssClass="failureNotification"
                            ErrorMessage="Username is required." runat="server" ToolTip="Username is required."
                            ValidationGroup="LoginUserValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> Username is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:Label AssociatedControlID="Password" runat="server" Text="Password: " />
                        <asp:TextBox CssClass="passwordEntry" ID="Password" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ControlToValidate="Password" CssClass="failureNotification"
                            ErrorMessage="Password is required." runat="server" ToolTip="Password is required."
                            ValidationGroup="LoginUserValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> Password is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:CheckBox ID="RememberMe" runat="server" />
                        <asp:Label AssociatedControlID="RememberMe" CssClass="inline" ID="RememberMeLabel"
                            runat="server">Keep me logged in</asp:Label>
                    </div>
                </fieldset>
                <div class="submitButton">
                    <asp:Button CommandName="Login" ID="LoginButton" runat="server" Text="Log In" ValidationGroup="LoginUserValidationGroup" />
                </div>
            </div>
        </LayoutTemplate>
        <LoginButtonStyle BackColor="#FFFBFF" Font-Names="Verdana" ForeColor="#284775" />
        <TextBoxStyle Font-Size="1em" />
    </asp:Login>
</asp:Content>
