<%@ Page AutoEventWireup="true" CodeFile="UpdateAllergySuccess.aspx.cs" Culture="en-SG"
    Inherits="Physician.UpdateAllergySuccess" Language="C#" MasterPageFile="~/Site.master"
    Title="Update Patient Allergy" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Update Patient Allergies
    </h2>
    <h3>
        Patient's drug allergy updated.
    </h3>
    <div class="topPadding">
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Patient Management"
            runat="server" />
        <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
            runat="server" Text="Return to Physician Homepage" />
    </div>
</asp:Content>
