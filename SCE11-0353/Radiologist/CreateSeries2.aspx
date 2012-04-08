<%@ Page Title="Start New Series" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CreateSeries2.aspx.cs" Inherits="Radiologist.CreateSeries2" Culture="en-SG" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <h2>
            Start New Series
        </h2>
        <h3>
            <asp:Label ID="PatientNameLabel" runat="server" Text="Displaying imaging orders of " />
        </h3>
    </div>
    <div class="topPadding">
        <%-- Error Notifications --%>
        <div class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server" Text="" />
            <asp:ValidationSummary runat="server" ValidationGroup="ImageValidationGroup" />
        </div>
        <%-- / Error Notifications --%>
        <%-- Study --%>
        <div class="topPadding">
            <asp:Label AssociatedControlID="Study" runat="server" Text="Study ID to start a new series: " />
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
        <%-- / Study --%>
        <%-- Modality --%>
        <div class="topPadding">
            <asp:Label AssociatedControlID="Modality" CssClass="float" runat="server" Text="Modality: " />
            <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" ID="Modality" MaxLength="0" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="Modality" CssClass="failureNotification"
                Display="Dynamic" ErrorMessage="Please specify the modality." runat="server"
                ToolTip="Please specify the modality." ValidationGroup="ImageValidationGroup">
                <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                Please specify the modality.
            </asp:RequiredFieldValidator>
        </div>
        <%-- / Modality --%>
        <div class="topPadding">
            <asp:Button CssClass="buttons" OnClick="CreateButtonClick" runat="server" Text="Create New Series"
                ValidationGroup="ImageValidationGroup" />
            <asp:Button CssClass="buttons" PostBackUrl="~/Radiologist/Default.aspx" runat="server"
                Text="Cancel" />
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
