<%@ Page AutoEventWireup="true" CodeFile="ManageUser.aspx.cs" Culture="en-SG" Inherits="Admin.ManageUser"
    Language="C#" MasterPageFile="~/Site.master" Title="Manage User Roles" %>

<%@ Register Assembly="skmValidators" Namespace="skmValidators" TagPrefix="skm" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Manage User Roles
    </h2>
    <%-- Error Notifications --%>
    <div class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" />
        <asp:ValidationSummary runat="server" ValidationGroup="ValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <%-- Role --%>
    <div>
        <h3>
            <asp:Label AssociatedControlID="Roles" ID="Label" runat="server" />
        </h3>
    </div>
    <div>
        <asp:CheckBoxList CellPadding="5" CellSpacing="5" CssClass="float" ID="Roles" RepeatDirection="Horizontal"
            runat="server" TextAlign="Left" />
        <skm:CheckBoxListValidator ControlToValidate="Roles" CssClass="failureNotification"
            ErrorMessage="Please select at least one role." MinimumNumberOfSelectedCheckBoxes="1"
            runat="server" ValidationGroup="ValidationGroup">
            <asp:Image ImageAlign="AbsBottom" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please select at least one role for the user.
        </skm:CheckBoxListValidator>
    </div>
    <%-- / Role --%>
    <div class="clear topPadding">
        <asp:Button CssClass="buttons" runat="server" Text="Update User Role(s)" ValidationGroup="ValidationGroup"
            OnClick="UpdateButtonClick" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Admin/Default.aspx" runat="server"
            Text="Cancel" />
    </div>
</asp:Content>
