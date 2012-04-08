<%@ Page AutoEventWireup="true" CodeFile="Default.aspx.cs" Culture="en-SG" Inherits="Account.Default"
    Language="C#" MasterPageFile="~/Site.master" Title="Account Management" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Account Management
    </h2>
    <table class="table">
        <tr>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/Keys.png" PostBackUrl="~/Account/ChangePassword.aspx"
                    runat="server" />
            </td>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/Lock.png" PostBackUrl="~/Account/ChangeQuestion.aspx"
                    runat="server" />
            </td>
            <td>
                <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/Folder.png" PostBackUrl="~/Account/UpdateParticulars.aspx"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <h3>
                    <asp:Label runat="server" Text="Change Password" />
                </h3>
            </td>
            <td>
                <h3>
                    <asp:Label runat="server" Text="Change Security Question & Answer" />
                </h3>
            </td>
            <td>
                <h3>
                    <asp:Label runat="server" Text="Update Particulars" />
                </h3>
            </td>
        </tr>
    </table>
</asp:Content>
