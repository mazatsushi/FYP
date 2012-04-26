<%@ Page AutoEventWireup="true" CodeFile="UpdateUser2.aspx.cs" Culture="en-SG"
    Inherits="Admin.UpdateUser2" Language="C#" MasterPageFile="~/Site.master"
    Title="Manage User Roles" %>

<%@ Register Assembly="skmValidators" Namespace="skmValidators" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Manage User Roles
    </h2>
    <%-- Error Notifications --%>
    <div class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UpdateUserValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <%-- Role --%>
    <div>
        <h3>
            <asp:Label AssociatedControlID="Role" ID="Label" runat="server" />
        </h3>
    </div>
    <div>
        <asp:CheckBoxList CellPadding="5" CellSpacing="5" CssClass="float" ID="Role" RepeatDirection="Horizontal"
                          runat="server" TextAlign="Left" />
        <skm:CheckBoxListValidator ID="CheckBoxListValidator1" ControlToValidate="Role" CssClass="failureNotification"
                                   ErrorMessage="Please select at least one role." MinimumNumberOfSelectedCheckBoxes="1"
                                   runat="server" ValidationGroup="UpdateUserValidationGroup">
            <asp:Image ID="Image1" ImageAlign="AbsBottom" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please select at least one role.
        </skm:CheckBoxListValidator>
    </div>
    <%-- / Role --%>
    <div class="clear topPadding">
        <asp:Button ID="Button1" CssClass="buttons" runat="server" Text="Update User Role(s)" ValidationGroup="UpdateUserValidationGroup"
            OnClick="UpdateButtonClick" />
        <asp:Button ID="Button2" CssClass="buttons" PostBackUrl="~/Admin/Default.aspx" runat="server"
            Text="Cancel" />
    </div>
</asp:Content>
