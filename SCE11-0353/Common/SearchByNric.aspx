<%@ Page AutoEventWireup="true" CodeFile="SearchByNric.aspx.cs" Culture="en-SG" Inherits="Common_SearchByNric"
    Language="C#" MasterPageFile="~/Site.master" Title="Search By NRIC" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Search for user via NRIC
            </h2>
            <p>
                Use the form below to search for a user using their NRIC.
            </p>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:Literal runat="server" Text="" />
                <asp:ValidationSummary runat="server" ValidationGroup="ValidationGroup" />
            </div>
            <%-- / Error Notifications --%>
            <div>
                <fieldset class="login">
                    <legend>Search By NRIC</legend>
                    <div>
                        <asp:Label AssociatedControlID="Nric" runat="server" Text="User's NRIC: " />
                        <asp:TextBox CssClass="textEntry" ID="Nric" runat="server" />
                        <asp:RegularExpressionValidator ControlToValidate="Nric" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter a valid NRIC." runat="server" ToolTip="Please enter a valid NRIC."
                            ValidationExpression="^[SFTG]\d{7}[A-Z]$" ValidationGroup="ValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please enter a valid NRIC.
                        </asp:RegularExpressionValidator>
                        <asp:CustomValidator ControlToValidate="Nric" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="NRIC not found." OnServerValidate="NricExists" runat="server" ToolTip="NRIC not found."
                            ValidationGroup="ValidationGroup"> <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> NRIC not found.
                        </asp:CustomValidator>
                        <asp:RequiredFieldValidator ControlToValidate="Nric" CssClass="failureNotification"
                            ErrorMessage="User's NRIC is required." runat="server" ToolTip="User's NRIC is required."
                            ValidationGroup="ValidationGroup">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            User's NRIC is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="submitButton, topPadding">
                        <asp:Button CommandName="Search" CssClass="buttons" runat="server" Text="Search"
                            ValidationGroup="ValidationGroup" OnClick="SearchButton_Click" />
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
