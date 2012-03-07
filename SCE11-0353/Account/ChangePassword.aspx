<%@ Page AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Culture="en-SG"
    Inherits="Account_ChangePassword" Language="C#" MasterPageFile="~/Site.master"
    Title="Change Password" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Change Password
    </h2>
    <p>
        Use the form below to change your password.
    </p>
    <p>
        New passwords are required to be a minimum of
        <%= Membership.MinRequiredPasswordLength %>
        characters in length, with at least
        <%= Membership.MinRequiredNonAlphanumericCharacters %>
        non-alphanumeric characters.
    </p>
    <asp:ChangePassword EnableViewState="false" RenderOuterTable="false" runat="server"
        SuccessPageUrl="~/Account/ChangePasswordSuccess.aspx">
        <ChangePasswordTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server" />
            </span>
            <asp:ValidationSummary CssClass="failureNotification" ID="ChangeUserPasswordValidationSummary"
                runat="server" ValidationGroup="ChangeUserPasswordValidationGroup" />
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label AssociatedControlID="CurrentPassword" runat="server" Text="Old Password: " />
                        <asp:TextBox CssClass="passwordEntry" ID="CurrentPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ControlToValidate="CurrentPassword" CssClass="failureNotification"
                            ErrorMessage="Current password is required." runat="server" ToolTip="Current Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> Current password is required.
                        </asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label AssociatedControlID="NewPassword" runat="server" Text="New Password: " />
                        <asp:TextBox CssClass="passwordEntry" ID="NewPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ControlToValidate="NewPassword" CssClass="failureNotification"
                            ErrorMessage="New password is required." runat="server" ToolTip="New password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> New password is required.
                        </asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label AssociatedControlID="ConfirmNewPassword" runat="server" Text="Confirm New Password: " />
                        <asp:TextBox CssClass="passwordEntry" ID="ConfirmNewPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ControlToValidate="ConfirmNewPassword" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter new password again." runat="server"
                            ToolTip="Please enter new password again." ValidationGroup="ChangeUserPasswordValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please enter new password again.
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Both new passwords must match."
                            runat="server" ValidationGroup="ChangeUserPasswordValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> Both new passwords must match.
                        </asp:CompareValidator>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button CommandName="ChangePassword" CssClass="buttons" ID="ChangePasswordPushButton"
                        runat="server" Text="Change Password" ValidationGroup="ChangeUserPasswordValidationGroup" />
                    <asp:Button CausesValidation="False" CommandName="Cancel" CssClass="buttons" ID="CancelPushButton"
                        OnClick="CancelPushButton_Click" runat="server" Text="Cancel" />
                </p>
            </div>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
</asp:Content>
