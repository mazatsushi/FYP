<%@ Page AutoEventWireup="true" CodeFile="AddDrug.aspx.cs" Culture="en-SG" Inherits="Admin.AddDrug"
    Language="C#" MasterPageFile="~/Site.master" Title="Add Drug Allergy" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Add Medical Allergy
    </h2>
    <p>
        Add a new medical allergy to the system.
    </p>
    <%-- Error Notifications --%>
    <div class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        <asp:ValidationSummary runat="server" ValidationGroup="ValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <%-- Drug name --%>
    <div>
        <asp:Label AssociatedControlID="DrugName" runat="server" Text="Drug Name: " />
        <asp:TextBox CssClass="textEntry" ID="DrugName" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="DrugName" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="Please enter a drug name." runat="server" ToolTip="Please enter a drug name."
            ValidationGroup="ValidationGroup" SetFocusOnError="True">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Please enter a drug name.
        </asp:RequiredFieldValidator>
        <asp:CustomValidator ControlToValidate="DrugName" CssClass="failureNotification"
            Display="Dynamic" ErrorMessage="Drug allergy is already in system." OnServerValidate="DrugNotExists"
            runat="server" ToolTip="Drug allergy is already in system." ValidationGroup="ValidationGroup">
            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
            Drug allergy is already in system.
        </asp:CustomValidator>
    </div>
    <%-- / Drug name --%>
    <div class="topPadding">
        <asp:Button CssClass="buttons" OnClick="AddButtonClick" runat="server" Text="Add Allergy"
            ValidationGroup="ValidationGroup" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Physician/Default.aspx" runat="server"
            Text="Cancel" />
    </div>
</asp:Content>
