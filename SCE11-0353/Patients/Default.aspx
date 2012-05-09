<%@ Page AutoEventWireup="true" CodeFile="Default.aspx.cs" Culture="en-SG" Inherits="Patients.Default"
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
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/Folder.png" PostBackUrl="~/Patients/ViewStudy.aspx"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <h3>
                    <asp:Label runat="server" Text="View Imaging History" />
                </h3>
            </td>
        </tr>
    </table>
</asp:Content>
