<%@ Page AutoEventWireup="true" CodeFile="AppointmentAdded.aspx.cs" Culture="en-SG"
    Inherits="Physician.AppointmentAdded" Language="C#" MasterPageFile="~/Site.master"
    Title="Appointment Added" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Manage Medical Imaging History
    </h2>
    <h3>
        New imaging appointment added on behalf of patient.
    </h3>
    <div class="topPadding">
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Patient Management"
            runat="server" />
        <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
            runat="server" Text="Return to Physician Homepage" />
    </div>
</asp:Content>
