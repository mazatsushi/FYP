<%@ Page AutoEventWireup="true" CodeFile="UpdateAllergy.aspx.cs" Culture="en-SG"
    Inherits="Physician.UpdateAllergy" Language="C#" MasterPageFile="~/Site.master"
    Title="Update Patient Allergies" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Update Patient Allergies
            </h2>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" />
                <asp:ValidationSummary runat="server" ValidationGroup="AddValidationGroup" />
                <asp:ValidationSummary runat="server" ValidationGroup="RemoveValidationGroup" />
            </div>
            <%-- / Error Notifications --%>
            <div>
                <h3>
                    Patient Name:
                    <asp:Label ID="PatientName" runat="server" />
                </h3>
            </div>
            <%-- Add Allergies --%>
            <fieldset class="topPadding">
                <legend>Add Drug Allergy</legend>
                <div>
                    <asp:Label ID="NoneLabel" runat="server" Text="No Known Drug Allergies" />
                    <asp:Label ID="SomeLabel" runat="server" Text="Current Drug Allergies" />
                    <div class="topPadding">
                        <asp:ListBox Enabled="False" Height="100px" ID="AllergyList" runat="server" SelectionMode="Multiple"
                            Width="140px" />
                    </div>
                </div>
                <div class="topPadding">
                    <asp:Label AssociatedControlID="Addable" runat="server" Text="* Drug name: " />
                    <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" DropDownStyle="DropDownList" ID="Addable"
                        runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="Addable" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Please specify the drug name." runat="server"
                        ToolTip="Please specify the drug name." ValidationGroup="AddValidationGroup">
                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Please specify the drug name.
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ControlToValidate="Addable" CssClass="failureNotification" Display="Dynamic"
                        ErrorMessage="Drug name not found in the system." OnServerValidate="DrugExists"
                        runat="server" ToolTip="Drug name not found in the system." ValidationGroup="AddValidationGroup">
                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Drug name not found in the system.
                    </asp:CustomValidator>
                    <asp:CustomValidator ControlToValidate="Addable" CssClass="failureNotification" Display="Dynamic"
                        ErrorMessage="Patient already has that drug allergy." OnServerValidate="HasNoAllergy"
                        runat="server" ToolTip="Patient already has that drug allergy." ValidationGroup="AddValidationGroup">
                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Patient already has that drug allergy.
                    </asp:CustomValidator>
                </div>
                <div class="topPadding">
                    <asp:Button CssClass="buttons" runat="server" Text="Add Allergy" OnClick="AddButtonClick"
                        ValidationGroup="AddValidationGroup" />
                </div>
            </fieldset>
            <%-- / Add Allergies --%>
            <%-- Remove Allergies --%>
            <fieldset class="topPadding" id="Removal" runat="server">
                <legend>Remove Drug Allergy</legend>
                <div class="topPadding">
                    <asp:Label AssociatedControlID="Removable" runat="server" Text="* Drug name: " />
                    <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" DropDownStyle="DropDownList" ID="Removable"
                        runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="Removable" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Please select the drug allergy to remove." runat="server"
                        ToolTip="Please select the drug allergy to remove." ValidationGroup="RemoveValidationGroup">
                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Please select the drug allergy to remove.
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ControlToValidate="Removable" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Drug name not found in the system." OnServerValidate="DrugExists"
                        runat="server" ToolTip="Drug name not found in the system." ValidationGroup="RemoveValidationGroup">
                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Drug name not found in the system.
                    </asp:CustomValidator>
                    <asp:CustomValidator ControlToValidate="Removable" CssClass="failureNotification" Display="Dynamic"
                        ErrorMessage="Patient does not have that drug allergy." OnServerValidate="HasAllergy"
                        runat="server" ToolTip="Patient does not have that drug allergy." ValidationGroup="RemoveValidationGroup">
                        <asp:Image ID="Image1" ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Patient does not have that drug allergy.
                    </asp:CustomValidator>
                </div>
                <div class="topPadding">
                    <asp:Button CssClass="buttons" runat="server" Text="Remove Allergy" OnClick="RemoveButtonClick"
                        ValidationGroup="RemoveValidationGroup" />
                </div>
            </fieldset>
            <%-- / Remove Allergies --%>
            <div class="topPadding">
                <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Patient Management"
                    runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
                    runat="server" Text="Return to Physician Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
