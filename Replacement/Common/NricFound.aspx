<%@ Page AutoEventWireup="true" CodeFile="NricFound.aspx.cs" Culture="en-SG" Inherits="Common.NricFound"
    Language="C#" MasterPageFile="~/Site.master" Title="Search By NRIC Found" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Search for User via NRIC
    </h2>
    <asp:DetailsView CellPadding="4" ForeColor="#333333" GridLines="None" Height="100px"
        ID="NricDetails" runat="server" Width="250px" AutoGenerateRows="False" DataSourceID="Particulars">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="NRIC" HeaderText="NRIC" ReadOnly="True" SortExpression="NRIC" />
            <asp:BoundField DataField="Prefix" HeaderText="Prefix" ReadOnly="True" SortExpression="Prefix" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" ReadOnly="True" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True" SortExpression="LastName" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <asp:LinqDataSource ContextTypeName="RIS_DB_Entities" EntityTypeName="" ID="Particulars"
        runat="server" Select="new (NRIC, FirstName, LastName, Prefix)" TableName="UserParticulars"
        Where="NRIC == @NRIC">
        <WhereParameters>
            <asp:QueryStringParameter Name="NRIC" QueryStringField="Nric" Type="String" />
        </WhereParameters>
    </asp:LinqDataSource>
    <div class="submitButton, topPadding">
        <asp:Button CssClass="buttons" OnClick="ProceedButtonClick" runat="server" Text="Proceed" />
        <asp:Button CssClass="buttons" OnClick="CancelButtonClick" runat="server" Text="Search Again" />
    </div>
</asp:Content>
