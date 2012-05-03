<%@ Page AutoEventWireup="true" CodeFile="SearchStudies.aspx.cs" Culture="en-SG"
    Inherits="Common.SearchStudies" Language="C#" MasterPageFile="~/Site.master"
    Title="Search for Studies" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Select a Study
    </h2>
    <div>
        <asp:Label ID="None" Text="System has no recorded studies yet." runat="server" />
        <asp:GridView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            ForeColor="#333333" GridLines="None" ID="AllStudies" runat="server" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60" HeaderText="Study ID"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("StudyId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="180" HeaderText="Description"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180" ItemStyle-Wrap="True">
                    <ItemTemplate>
                        <asp:TextBox Enabled="False" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="160" HeaderText="Date Started"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("DateStarted") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80" HeaderText="Completed"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:CheckBox Checked='<%# Eval("IsCompleted") %>' Enabled="False" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="160" HeaderText="Date Finished"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("DateCompleted") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="180" HeaderText="Diagnosis"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180">
                    <ItemTemplate>
                        <asp:TextBox Enabled="False" runat="server" Text='<%# Eval("Diagnosis") %>' TextMode="MultiLine" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:HyperLink NavigateUrl='<%# ResolveUrl(HttpUtility.HtmlEncode(Request.QueryString["ReturnUrl"])) + "?StudyId=" + Eval("StudyId") + "&Checksum=" + CryptoHandler.GetHash(Eval("StudyId").ToString()) %>'
                            runat="server">Select</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>
</asp:Content>
