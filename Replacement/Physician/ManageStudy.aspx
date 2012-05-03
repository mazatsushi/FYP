<%@ Page AutoEventWireup="true" CodeFile="ManageStudy.aspx.cs" Culture="en-SG" Inherits="Physician.ManageStudy"
    Language="C#" MasterPageFile="~/Site.master" Title="Manage Study" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Manage Study
    </h2>
    <div>
        <h3>
            Study ID:
            <asp:Label ID="Study" runat="server" />
        </h3>
    </div>
    <div>
        This here to display data about the study
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px">
        </asp:DetailsView>
    </div>
    <div>
        This here to use grid view to display the DICOM images. Or to link to yet another page to view the images.
    </div>
    <div>
        This here to allow user to mark study as complete. Maybe we can use the details view instead?
    </div>
    <div class="topPadding">
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Previous Page"
            runat="server" />
        <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
            runat="server" Text="Return to Physician Homepage" />
    </div>
</asp:Content>
