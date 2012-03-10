<%@ Page AutoEventWireup="true" CodeFile="CreateAppointment.aspx.cs" Culture="en-SG"
    Inherits="Staff_ScheduleAppointment" Language="C#" MasterPageFile="~/Site.master"
    Title="Schedule Appointment" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <h2>
            Schedule New Appointment
        </h2>
        <p>
            Schedule a new appointment on behalf of a patient.
        </p>
        <p>
            Elements marked with * are required.
        </p>
    </div>
    <%-- Error Notifications --%>
    <div>
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        </span>
        <asp:ValidationSummary CssClass="failureNotification" runat="server" ValidationGroup="AppointmentValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <%-- Personal Information --%>
    <asp:Panel CssClass="collapsePanelHeader" ID="PersonalInfoHeader" runat="server">
        <asp:Image ID="PersonalInfoPanelArrow" runat="server" />
        Personal Information (<asp:Label ID="PersonalInfoPanelLabel" runat="server" />)
    </asp:Panel>
    <asp:Panel CssClass="collapsePanel" ID="PersonalInfoContent" runat="server">
        <fieldset class="register">
        </fieldset>
    </asp:Panel>
    <%-- / Personal Information --%>
</asp:Content>
