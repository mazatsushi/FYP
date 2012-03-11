<%@ Page AutoEventWireup="true" CodeFile="UpdatePatientBloodType2.aspx.cs" Culture="en-SG"
    Inherits="Physician_UpdatePatientBloodType2" Language="C#" MasterPageFile="~/Site.master"
    Title="Update Patient Blood Type" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Update Patient Blood Type
    </h2>
    <%-- Error Notifications --%>
    <div>
        <asp:Literal ID="ErrorMessage" runat="server" />
        <asp:ValidationSummary CssClass="failureNotification" runat="server" ValidationGroup="PatientValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <%-- Patient Allergies --%>
    <div>
        <h3>
            <asp:Label ID="PatientNameLabel" runat="server" Text="Patient Name: " />
        </h3>
        <asp:Label AssociatedControlID="BloodType" runat="server" Text="Patient Blood Type: " />
        <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" ID="BloodType" MaxLength="0" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="BloodType" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="Please specify the patient's blood type." runat="server"
            ToolTip="Please specify the patient's blood type." ValidationGroup="PatientValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please specify the patient's blood type.
        </asp:RequiredFieldValidator>
    </div>
    <%-- / Patient Allergies --%>
    <div>
        <div class="topPadding">
            <asp:Button CssClass="buttons" OnClick="UpdateButtonClick" runat="server" Text="Update Patient's Blood Type"
                ValidationGroup="PatientValidationGroup" />
            <asp:Button CssClass="buttons" PostBackUrl="~/Physician/Default.aspx" runat="server"
                Text="Cancel" />
        </div>
    </div>
</asp:Content>
