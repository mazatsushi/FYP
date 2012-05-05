<%@ Page AutoEventWireup="true" CodeFile="StudyClosed.aspx.cs" Inherits="Physician.StudyClosed"
    Language="C#" MasterPageFile="~/Site.master" Title="Study Closed" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Manage Study
    </h2>
    <h3>
    </h3>
    <div class="topPadding">
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Patient Management"
            runat="server" />
        <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
            runat="server" Text="Return to Physician Homepage" />
    </div>
</asp:Content>
