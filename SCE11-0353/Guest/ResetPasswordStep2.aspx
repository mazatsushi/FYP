<%@ Page AutoEventWireup="true" CodeFile="ResetPasswordStep2.aspx.cs" Culture="en-SG"
    Inherits="Guest_ResetPasswordStep2" Language="C#" MasterPageFile="~/Site.master"
    Title="Reset Password" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Reset Password
            </h2>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" Text="" />
                <asp:ValidationSummary runat="server" ValidationGroup="ValidationGroup" />
            </div>
            <%-- / Error Notifications --%>
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
            </div>
            <div class="topPadding">
                <asp:Button CssClass="buttons" runat="server" Text="Reset Password" ValidationGroup="ValidationGroup"
                    OnClick="ResetButtonClick" />
                <asp:Button CssClass="buttons" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
