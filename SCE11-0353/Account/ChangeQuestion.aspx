<%@ Page AutoEventWireup="true" CodeFile="ChangeQuestion.aspx.cs" Culture="en-SG"
    Inherits="Account.ChangeQuestion" Language="C#" MasterPageFile="~/Site.master"
    Title="Change Security Question and Answer" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Change Security Question & Answer
            </h2>
            <p>
                Use the form below to change your security question and answer.
            </p>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:ValidationSummary runat="server" ValidationGroup="ValidationGroup" />
            </div>
            <%-- / Error Notifications --%>
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>Security Question & Answer</legend>
                    <%-- Password --%>
                    <div>
                        <asp:Label AssociatedControlID="Password" runat="server" Text="Password: " />
                        <asp:TextBox CssClass="passwordEntry" ID="Password" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ControlToValidate="Password" CssClass="failureNotification"
                            ErrorMessage="Password is required." runat="server" ToolTip="Password is required."
                            ValidationGroup="ValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Password is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <%-- / Password --%>
                    <%-- Security Question --%>
                    <div class="topPadding">
                        <asp:Label AssociatedControlID="Question" runat="server" Text="Security Question:" />
                        <asp:TextBox CssClass="textEntry" ID="Question" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="Question" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Security question is required." runat="server"
                            ToolTip="Security question is required." ValidationGroup="ValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Security question is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <%-- / Security Question --%>
                    <%-- Security Answer --%>
                    <div class="topPadding">
                        <asp:Label AssociatedControlID="Answer" runat="server" Text="Security Answer:" />
                        <asp:TextBox CssClass="textEntry" ID="Answer" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="Answer" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Security answer is required." runat="server"
                            ToolTip="Security answer is required." ValidationGroup="ValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Security answer is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <%-- / Security Answer --%>
                </fieldset>
            </div>
            <div>
                <asp:Button CssClass="buttons" runat="server" Text="Update" ValidationGroup="ValidationGroup"
                    OnClick="UpdateButtonClick" />
                <asp:Button CausesValidation="False" CssClass="buttons" OnClick="CancelButtonClick"
                    runat="server" Text="Cancel" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
