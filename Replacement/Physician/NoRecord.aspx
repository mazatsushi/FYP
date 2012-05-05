<%@ Page Title="No Medical Record" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="NoRecord.aspx.cs" Inherits="Physician.NoRecord"
    Culture="en-SG" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h3>
                The patient does not have a recorded blood type yet. Please rectify the problem
                before proceeding.
            </h3>
            <div class="topPadding">
                <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Previous Page"
                    runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
                    runat="server" Text="Return to Physician Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
