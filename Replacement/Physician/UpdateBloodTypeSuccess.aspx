<%@ Page AutoEventWireup="true" CodeFile="UpdateBloodTypeSuccess.aspx.cs" Culture="en-SG"
    Inherits="Physician.UpdateBloodTypeSuccess" Language="C#" MasterPageFile="~/Site.master"
    Title="Manage Patient" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Update Patient's Blood Type</h2>
            <h3>
                Patient's blood type has been updated.
            </h3>
            <div class="topPadding">
                <asp:Button CssClass="buttons" PostBackUrl="ManagePatient.aspx" Text="Return to Previous Page"
                    runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" runat="server" Text="Return to Physician Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
