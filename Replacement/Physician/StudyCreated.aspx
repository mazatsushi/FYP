<%@ Page AutoEventWireup="true" CodeFile="StudyCreated.aspx.cs" Culture="en-SG" Inherits="Physician.StudyCreated"
    Language="C#" MasterPageFile="~/Site.master" Title="Create Medical Study" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Create Medical Study
            </h2>
            <h3>
                A new study has been created for the patient.
            </h3>
            <div class="topPadding">
                <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Patient Management"
                    runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
                    runat="server" Text="Return to Physician Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
