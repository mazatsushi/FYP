<%@ Page AutoEventWireup="true" CodeFile="Default.aspx.cs" Culture="en-SG" Inherits="Radiologist.Default"
    Language="C#" MasterPageFile="~/Site.master" Title="Physician Home" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Radiologist Homepage
            </h2>
            <table class="table">
                <tr>
                    <td>
                        <asp:ImageButton CausesValidation="False" ImageUrl="~/Images/patient.png" PostBackUrl="~/Radiologist/ManagePatient.aspx"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
