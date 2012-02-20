<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Error.aspx.cs" Inherits="Error_Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="errorMsg">
        <h1>
            Oops! The page you just tried to access cannot be found.
        </h1>
    </div>
    <br />
    <div id="errorImg">
        <asp:Image ID="errorSign" runat="server" ImageUrl="~/Images/Error.png" />
    </div>
</asp:Content>
