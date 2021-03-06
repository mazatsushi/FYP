﻿<%@ Page AutoEventWireup="true" CodeFile="Default.aspx.cs" Culture="en-SG" Inherits="Physician.Default"
    Language="C#" MasterPageFile="~/Site.master" Title="Physician Home" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Physician Homepage
    </h2>
    <table class="table">
        <tr>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/patient.png" PostBackUrl="~/Physician/ManagePatient.aspx"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <h3>
                    <asp:Label runat="server" Text="Manage Patient" />
                </h3>
            </td>
        </tr>
    </table>
</asp:Content>
