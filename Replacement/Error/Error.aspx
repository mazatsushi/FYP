<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Error.aspx.cs" Inherits="Error.Error" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div id="errorMsg">
        <h1>
            Oops! An error just occured. Please contact the system administrator for assistance.
        </h1>
    </div>
    <br />
    <div id="errorImg">
        <asp:Image ImageUrl="~/Images/Error.png" runat="server" />
    </div>
</asp:Content>
