<%@ Page AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Culture="en-SG"
    Inherits="Account_ChangePassword" Language="C#" MasterPageFile="~/Site.master"
    Title="Change Password" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
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
                    <div class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server" />
                        <asp:ValidationSummary runat="server" ValidationGroup="ValidationGroup" />
                    </div>
                    <div class="accountInfo">
                        <fieldset class="changePassword">
                            <legend>Account Information</legend>
                            <%-- Current Password --%>
                            <div>
                                <asp:Label AssociatedControlID="CurrentPassword" runat="server" Text="Old Password: " />
                                <asp:TextBox CssClass="passwordEntry" ID="CurrentPassword" runat="server" TextMode="Password" />
                                <asp:RequiredFieldValidator ControlToValidate="CurrentPassword" CssClass="failureNotification"
                                    ErrorMessage="Current password is required." runat="server" ToolTip="Current Password is required."
                                    ValidationGroup="ValidationGroup">
                                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                    Current password is required.
                                </asp:RequiredFieldValidator>
                            </div>
                            <%-- / Current Password --%>
                            <%-- New Password --%>
                            <div class="topPadding">
                                <asp:Label AssociatedControlID="NewPassword" runat="server" Text="New Password: " />
                                <asp:TextBox CssClass="passwordEntry" ID="NewPassword" runat="server" TextMode="Password" />
                                <asp:RequiredFieldValidator ControlToValidate="NewPassword" CssClass="failureNotification"
                                    ErrorMessage="New password is required." runat="server" ToolTip="New password is required."
                                    ValidationGroup="ValidationGroup">
                                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                    New password is required.
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:PasswordStrength BarBorderCssClass="BarBorder" BarIndicatorCssClass="BarIndicator"
                                    DisplayPosition="RightSide" Enabled="True" HelpStatusLabelID="PasswordStrengthLabel"
                                    MinimumNumericCharacters="1" MinimumSymbolCharacters="1" PreferredPasswordLength="6"
                                    runat="server" StrengthIndicatorType="BarIndicator" TargetControlID="NewPassword" />
                                <br />
                                <asp:Label ID="PasswordStrengthLabel" runat="server" Text="Label" />
                            </div>
                            <%-- / New Password --%>
                            <%-- Confirm New Password --%>
                            <div class="topPadding">
                                <asp:Label AssociatedControlID="ConfirmNewPassword" runat="server" Text="Confirm New Password: " />
                                <asp:TextBox CssClass="passwordEntry" ID="ConfirmNewPassword" runat="server" TextMode="Password" />
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmNewPassword" CssClass="failureNotification"
                                    Display="Dynamic" ErrorMessage="Please enter new password again." runat="server"
                                    ToolTip="Please enter new password again." ValidationGroup="ValidationGroup">
                                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                    Please enter new password again.
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="Both new passwords must match."
                                    runat="server" ValidationGroup="ValidationGroup">
                                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                    Both new passwords must match.
                                </asp:CompareValidator>
                            </div>
                            <%-- / Confirm New Password --%>
                        </fieldset>
                        <div class="submitButton">
                            <asp:Button CommandName="ChangePassword" CssClass="buttons" ID="ChangePasswordPushButton"
                                runat="server" Text="Change Password" ValidationGroup="ValidationGroup" />
                            <asp:Button CausesValidation="False" CommandName="Cancel" CssClass="buttons" ID="CancelPushButton"
                                OnClick="CancelPushButton_Click" runat="server" Text="Cancel" />
                        </div>
                    </div>
                </ChangePasswordTemplate>
            </asp:ChangePassword>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
