<%@ Page AutoEventWireup="true" CodeFile="ImageAdded.aspx.cs" Culture="en-SG" Inherits="Radiologist.ImageAdded"
    Language="C#" MasterPageFile="~/Site.master" Title="Manage Series" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Manage Series
            </h2>
            <h3>
                Image has been successfully uploaded into the system.
            </h3>
            <div class="topPadding">
                <asp:Button CssClass="buttons" ID="BackButton" runat="server" Text="Return to Previous Page" />
                <asp:Button CssClass="buttons" PostBackUrl="~/Radiologist/ManagePatient.aspx" Text="Return to Patient Management"
                    runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Radiologist/Default.aspx"
                    runat="server" Text="Return to Radiologist Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
