<%@ Page AutoEventWireup="true" CodeFile="Error.aspx.cs" Culture="en-SG" Inherits="Error_Error"
    Language="C#" MasterPageFile="~/Site.master" Title="" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div id="errorMsg">
        <h1>
            Oops! An error just occured. Please contact the system administrator for troubleshooting.
        </h1>
    </div>
    <br />
    <div id="errorImg">
        <asp:Image runat="server" ImageUrl="~/Images/Error.png" />
    </div>
</asp:Content>
