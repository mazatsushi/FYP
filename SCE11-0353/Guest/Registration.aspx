<%@ Page AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Guest_Registration"
    Language="C#" MasterPageFile="~/Site.master" Title="Register" %>

<asp:Content ContentPlaceHolderID="HeadContent" ID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContent" runat="Server">
    <asp:CreateUserWizard BackColor="#F7F6F3" Font-Names="Verdana" Font-Size="1em" ID="CreateUserWizard1"
        runat="server">
        <ContinueButtonStyle BackColor="#FFFBFF" Font-Names="Verdana" ForeColor="#284775" />
        <CreateUserButtonStyle BackColor="#FFFBFF" Font-Names="Verdana" ForeColor="#284775" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <WizardSteps>
            <asp:WizardStep ID="CreateUserWizardStep1" runat="server" StepType="Start">
                <asp:Panel runat="server">
                    <asp:Label runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </asp:Panel>
            </asp:WizardStep>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep2" runat="server" >
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep" runat="server" >
            </asp:CompleteWizardStep>
        </WizardSteps>
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="1em"
            ForeColor="White" HorizontalAlign="Center" />
        <NavigationButtonStyle BackColor="#FFFBFF" Font-Names="Verdana" ForeColor="#284775" />
        <SideBarButtonStyle Font-Names="Verdana" ForeColor="White" />
        <SideBarStyle BackColor="#5D7B9D" Font-Size="1em" VerticalAlign="Top" />
    </asp:CreateUserWizard>
</asp:Content>
