<%@ Page AutoEventWireup="true" CodeFile="ManageImagingOrder2.aspx.cs" Culture="en-SG"
    Inherits="Physician_ManageImagingOrder2" Language="C#" MasterPageFile="~/Site.master"
    Title="Manage Imaging Studies" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <h2>
            Manage Imaging Order
        </h2>
        <h3>
            <asp:Label ID="PatientNameLabel" runat="server" Text="Displaying medical study history of " />
        </h3>
    </div>
    <div class="topPadding">
        <%-- Error Notifications --%>
        <div class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server" Text="" />
            <asp:ValidationSummary runat="server" ValidationGroup="ImageValidationGroup" />
        </div>
        <%-- / Error Notifications --%>
        <div class="topPadding">
            <asp:Label AssociatedControlID="Study" runat="server" Text="Study ID to display: " />
            <asp:TextBox CssClass="textEntry" ID="Study" runat="server" />
            <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="Numbers" runat="server"
                TargetControlID="Study" />
            <asp:RequiredFieldValidator ControlToValidate="Study" CssClass="failureNotification"
                Display="Dynamic" ErrorMessage="Please enter the study ID." runat="server" ToolTip="Please enter the study ID."
                ValidationGroup="ImageValidationGroup">
                <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                Please enter the study ID.
            </asp:RequiredFieldValidator>
            <asp:CustomValidator runat="server" ErrorMessage="CustomValidator">
                <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                Please enter the study ID.
            </asp:CustomValidator>
        </div>
        <div class="topPadding">
            <asp:Button CssClass="buttons" OnClick="RetrieveButtonClick" runat="server" Text="Display Study Details"
                ValidationGroup="ImageValidationGroup" />
        </div>
    </div>
    <div class="topPadding">
        <asp:GridView AllowPaging="True" AllowSorting="True" CellPadding="4" EmptyDataText="Null"
            ForeColor="#333333" GridLines="None" ID="StudyGrid" runat="server">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" HorizontalAlign="Center" VerticalAlign="Middle" />
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
