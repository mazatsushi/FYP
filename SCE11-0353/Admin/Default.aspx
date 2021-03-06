﻿<%@ Page AutoEventWireup="true" CodeFile="Default.aspx.cs" Culture="en-SG" Inherits="Admin.Default"
    Language="C#" MasterPageFile="~/Site.master" Title="Admin Homepage" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Admin Homepage
    </h2>
    <table class="table">
        <tr>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/drug.png" PostBackUrl="~/Admin/AddDrug.aspx"
                    runat="server" />
            </td>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/user.png" PostBackUrl="~/Admin/AddUser.aspx"
                    runat="server" />
            </td>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/cog.png" PostBackUrl="~/Admin/ManageUser.aspx"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <h3>
                    <asp:Label runat="server" Text="Add Drug" />
                </h3>
            </td>
            <td>
                <h3>
                    <asp:Label runat="server" Text="Add User" />
                </h3>
            </td>
            <td>
                <h3>
                    <asp:Label runat="server" Text="Manage User Role(s)" />
                </h3>
            </td>
        </tr>
    </table>
</asp:Content>
