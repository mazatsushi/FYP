<%@ Page AutoEventWireup="true" CodeFile="HashFailure.aspx.cs" Culture="en-SG" Inherits="Error.HashFailure"
    Language="C#" MasterPageFile="~/Site.master" Title="Hash Failure" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div id="errorMsg">
        <h1>
            Your attempt to hack into the system has been recorded and forwarded to the relevant authorities.
            Have a nice day.
        </h1>
    </div>
    <br />
    <div id="errorImg">
        <asp:Image ImageUrl="~/Images/niceday.jpg" runat="server" />
    </div>
</asp:Content>
