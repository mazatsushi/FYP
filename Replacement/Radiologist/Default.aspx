<%@ Page AutoEventWireup="true" CodeFile="Default.aspx.cs" Culture="en-SG" Inherits="Radiologist.Default"
    Language="C#" MasterPageFile="~/Site.master" Title="Physician Home" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Radiologist Homepage
    </h2>
    <table class="table">
        <tr>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/placeholder.png" PostBackUrl="#"
                    runat="server" />
            </td>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/patient.png" PostBackUrl="~/Physician/ManagePatient.aspx"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <h3>
                    <asp:Label runat="server" Text="To Be Determined" />
                </h3>
            </td>
            <td>
                <h3>
                    <asp:Label runat="server" Text="Manage Patient" />
                </h3>
            </td>
        </tr>
    </table>
</asp:Content>
