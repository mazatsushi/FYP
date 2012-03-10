<%@ Page AutoEventWireup="true" CodeFile="UpdatePatientBlood2.aspx.cs" Culture="en-SG"
    Inherits="Physician_UpdatePatientBlood2" Language="C#" MasterPageFile="~/Site.master"
    Title="Update Patient Blood" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Update Patient Blood Type
    </h2>
    <%-- Error Notifications --%>
    <asp:Panel runat="server">
        <asp:Literal ID="ErrorMessage" runat="server" />
        <asp:ValidationSummary CssClass="failureNotification" runat="server" ValidationGroup="PatientValidationGroup" />
    </asp:Panel>
    <%-- / Error Notifications --%>
    <%-- Blood Type --%>
    <asp:Panel runat="server">
        <asp:Label AssociatedControlID="BloodType" runat="server" Text="Blood Type: " />
        <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" DropDownStyle="DropDownList" ID="BloodType"
            MaxLength="0" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="BloodType" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="Please enter patient's blood type." runat="server"
            ToolTip="Please enter patient's blood type." ValidationGroup="PatientValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please enter patient's blood type.
        </asp:RequiredFieldValidator>
    </asp:Panel>
    <%-- / Blood Type --%>
    <asp:Panel Enabled="False" runat="server" Visible="False">
        <asp:Panel CssClass="topPadding" runat="server">
            <asp:Button CssClass="buttons" OnClick="UpdateButtonClick" runat="server" Text="Update Blood Type"
                ValidationGroup="PatientValidationGroup" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
