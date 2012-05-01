<%@ Page Title="Create Medical Record First" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="NoBlood.aspx.cs" Inherits="Physician.NoBlood"
    Culture="en-SG" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Update Patient Allergies
    </h2>
    <h3>
        The patient does not have a recorded blood type yet. Please rectify the problem
        before updating his/her drug allergies.
    </h3>
    <div class="topPadding">
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Previous Page"
            runat="server" />
        <asp:Button CssClass="buttons" OnClick="ResetButtonClick" runat="server" Text="Return to Physician Homepage" />
    </div>
</asp:Content>
