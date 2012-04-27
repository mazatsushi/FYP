<%@ Page AutoEventWireup="true" CodeFile="HashFailure.aspx.cs" Culture="en-SG" Inherits="Error.HashFailure"
    Language="C#" MasterPageFile="~/Site.master" Title="Hash Failure" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div id="errorMsg">
        <h1>
            The NRIC provided is suspected to have been compromised. Please contact the system
            administrator for assistance.
        </h1>
    </div>
    <br />
    <div id="errorImg">
        <asp:Image ImageUrl="~/Images/Error.png" runat="server" />
    </div>
</asp:Content>
