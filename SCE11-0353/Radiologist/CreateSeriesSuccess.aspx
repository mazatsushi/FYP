<%@ Page AutoEventWireup="true" CodeFile="CreateSeriesSuccess.aspx.cs" Culture="en-SG"
    Inherits="Radiologist.CreateSeriesSuccess" Language="C#" MasterPageFile="~/Site.master"
    Title="Start New Series" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Start New Series
    </h2>
    <h3>
        <asp:Label ID="Status" Text="A new series has been created with the ID of " runat="server" />
    </h3>
</asp:Content>
