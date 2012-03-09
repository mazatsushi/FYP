<%@ Page AutoEventWireup="true" CodeFile="UpdateParticulars.aspx.cs" Culture="en-SG"
    Inherits="Account_UpdateParticulars" Language="C#" MasterPageFile="~/Site.master"
    Title="Update Particulars" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Update Particulars
    </h2>
    <p>
        Use the form below to update your particulars.
    </p>
    <p>
        Elements marked with * are required.
    </p>
    <div>
        <%-- Error Notifications --%>
        <span class="failureNotification">
            <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        </span>
        <asp:ValidationSummary CssClass="failureNotification" runat="server" ValidationGroup="UpdateParticularsValidationGroup" />
        <%-- / Error Notifications --%>
    </div>
    <%-- Personal Information --%>
    <asp:Panel CssClass="collapsePanelHeader" ID="PersonalInfoHeader" runat="server">
        <asp:Image ID="PersonalInfoPanelArrow" runat="server" />
        Personal Information (<asp:Label ID="PersonalInfoPanelLabel" runat="server" />)
    </asp:Panel>
    <asp:Panel CssClass="collapsePanel" ID="PersonalInfoContent" runat="server">
        <fieldset class="register">
            <%-- First Name --%>
            <div class="topPadding">
                <asp:Label AssociatedControlID="FirstName" runat="server" Text="* First Name: " />
                <asp:TextBox CssClass="textEntry" ID="FirstName" runat="server" />
                <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                    runat="server" TargetControlID="FirstName" />
                <asp:RequiredFieldValidator ControlToValidate="FirstName" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter your first name." runat="server"
                    ToolTip="Please enter your first name." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your first name.
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ControlToValidate="FirstName" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter a valid first name." OnServerValidate="IsFirstNameValid"
                    runat="server" ToolTip="Please enter a valid first name." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter a valid first name.
                </asp:CustomValidator>
            </div>
            <%-- / First Name --%>
            <%-- Middle Name --%>
            <div class="topPadding">
                <asp:Label AssociatedControlID="MiddleName" runat="server" Text="Middle Name: " />
                <asp:TextBox CssClass="textEntry" ID="MiddleName" runat="server" />
                <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                    runat="server" TargetControlID="MiddleName" />
                <asp:CustomValidator ControlToValidate="MiddleName" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter a valid middle name." OnServerValidate="IsMiddleNameValid"
                    runat="server" ToolTip="Please enter a valid middle name." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter a valid middle name.
                </asp:CustomValidator>
            </div>
            <%-- / Middle Name --%>
            <%-- Last Name --%>
            <div class="topPadding">
                <asp:Label AssociatedControlID="LastName" runat="server" Text="* Last Name: " />
                <asp:TextBox CssClass="textEntry" ID="LastName" runat="server" />
                <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                    runat="server" TargetControlID="LastName" />
                <asp:RequiredFieldValidator ControlToValidate="LastName" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter your last name." runat="server"
                    ToolTip="Please enter your last name." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your last name.
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ControlToValidate="LastName" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter a valid last name." OnServerValidate="IsLastNameValid"
                    runat="server" ToolTip="Please enter a valid last name." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter a valid last name.
                </asp:CustomValidator>
            </div>
            <%-- / Last Name --%>
            <%-- Prefix --%>
            <div class="clear">
                <asp:Label AssociatedControlID="Prefix" CssClass="topPadding" runat="server" Text="* Salutation: " />
                <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" DropDownStyle="DropDownList" ID="Prefix"
                    runat="server">
                    <asp:ListItem Text="Dr." Value="Dr." />
                    <asp:ListItem Text="Mdm." Value="Mdm." />
                    <asp:ListItem Text="Mr." Value="Mr." />
                    <asp:ListItem Text="Ms." Value="Ms." />
                    <asp:ListItem Text="Prof." Value="Prof." />
                </ajaxToolkit:ComboBox>
                <asp:RequiredFieldValidator ControlToValidate="Prefix" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please specify a salutation." runat="server"
                    ToolTip="Please specify a salutation." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify a salutation.
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ControlToValidate="Prefix" CssClass="failureNotification" Display="Dynamic"
                    ErrorMessage="Please specify a valid salutation." OnServerValidate="IsPrefixValid"
                    runat="server" ToolTip="Please specify a valid salutation." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify a valid salutation.
                </asp:CustomValidator>
            </div>
            <%-- / Prefix --%>
            <%-- Suffix --%>
            <div class="topPadding">
                <asp:Label AssociatedControlID="Suffix" runat="server" Text="Suffix: " />
                <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" DropDownStyle="DropDownList" ID="Suffix"
                    runat="server">
                    <asp:ListItem Text="" Value="" />
                    <asp:ListItem Text="Jr." Value="Jr." />
                    <asp:ListItem Text="Sr." Value="Sr." />
                </ajaxToolkit:ComboBox>
                <asp:CustomValidator ControlToValidate="Suffix" CssClass="failureNotification" Display="Dynamic"
                    ErrorMessage="Please specify a valid suffix." OnServerValidate="IsSuffixValid"
                    runat="server" ToolTip="Please specify a valid suffix." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify a valid suffix.
                </asp:CustomValidator>
            </div>
            <%-- / Suffix --%>
            <%-- Address --%>
            <div class="topPadding">
                <asp:Label AssociatedControlID="Address" runat="server" Text="* Address: " />
                <asp:TextBox CssClass="textEntry" ID="Address" runat="server" OnTextChanged="UpdateText" TextMode="MultiLine" />
                <asp:RequiredFieldValidator ControlToValidate="Address" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please specify an address." runat="server" ToolTip="Please specify an address."
                    ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify an address.
                </asp:RequiredFieldValidator>
            </div>
            <%-- / Address --%>
            <%-- Contact Number --%>
            <div class="topPadding">
                <asp:Label AssociatedControlID="ContactNumber" runat="server" Text="* Contact Number: " />
                <asp:TextBox CssClass="textEntry" ID="ContactNumber" runat="server" />
                <ajaxToolkit:MaskedEditExtender AutoComplete="False" ClearTextOnInvalid="False" ClipboardEnabled="False"
                    Enabled="True" ID="Contact_Input" Mask="99999999" MaskType="Number" runat="server"
                    TargetControlID="ContactNumber" />
                <ajaxToolkit:MaskedEditValidator ControlExtender="Contact_Input" ControlToValidate="ContactNumber"
                    CssClass="failureNotification" Display="Dynamic" EmptyValueBlurredText="<img src='../Images/icons/error.png'> Please specify your contact number."
                    EmptyValueMessage="Please specify your contact number." InvalidValueBlurredMessage="<img src='../Images/icons/error.png'> Please specify a valid contact number."
                    InvalidValueMessage="Please specify a valid contact number." IsValidEmpty="False"
                    runat="server" ValidationExpression="^[3689]\d{7}$" ValidationGroup="UpdateParticularsValidationGroup" />
            </div>
            <%-- / Contact Number --%>
            <%-- Postal Code --%>
            <div class="topPadding">
                <asp:Label AssociatedControlID="PostalCode" runat="server" Text="* Postal Code: " />
                <asp:TextBox CssClass="textEntry" ID="PostalCode" runat="server" />
                <ajaxToolkit:MaskedEditExtender AutoComplete="False" ClearTextOnInvalid="False" ClipboardEnabled="False"
                    Enabled="True" ID="PostalCode_Input" Mask="999999" MaskType="Number" runat="server"
                    TargetControlID="PostalCode" />
                <ajaxToolkit:MaskedEditValidator ControlExtender="PostalCode_Input" ControlToValidate="PostalCode"
                    CssClass="failureNotification" Display="Dynamic" EmptyValueBlurredText="<img src='../Images/icons/error.png'> Please specify your postal code."
                    EmptyValueMessage="Please specify your postal code." InvalidValueBlurredMessage="<img src='../Images/icons/error.png'> Please specify a valid postal."
                    InvalidValueMessage="Please specify a valid postal." IsValidEmpty="False" runat="server"
                    ValidationExpression="\d{6}$" ValidationGroup="UpdateParticularsValidationGroup" />
            </div>
            <%-- / Postal Code --%>
            <%-- Country --%>
            <div class="topPadding">
                <asp:Label AssociatedControlID="Country" runat="server" Text="* Country of Residence: " />
                <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" CssClass="float" DataSourceID="CountryListing"
                    DataTextField="CountryName" DataValueField="CountryName" DropDownStyle="DropDownList"
                    ID="Country" MaxLength="0" runat="server" />
                <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
                    ID="CountryListing" runat="server" SelectCommand="SELECT DISTINCT [CountryName] FROM [Countries] ORDER BY [CountryName]" />
                <asp:RequiredFieldValidator ControlToValidate="Country" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please specify your country of residence." runat="server"
                    ToolTip="Please specify your country of residence." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify your country of residence.
                </asp:RequiredFieldValidator>
            </div>
            <%-- / Country --%>
            <%-- Nationality --%>
            <div class="clear">
                <asp:Label AssociatedControlID="Nationality" CssClass="topPadding" runat="server" Text="* Nationality: " />
                <asp:TextBox CssClass="textEntry" ID="Nationality" runat="server" />
                <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                    runat="server" TargetControlID="Nationality" />
                <asp:RequiredFieldValidator ControlToValidate="Nationality" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please specify your nationality." runat="server"
                    ToolTip="Please specify your nationality." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify your nationality.
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ControlToValidate="Nationality" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please specify a valid nationality." OnServerValidate="IsNationalityValid"
                    runat="server" ToolTip="Please specify a valid nationality." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify a valid nationality.
                </asp:CustomValidator>
            </div>
            <%-- / Nationality --%>
        </fieldset>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender CollapseControlID="PersonalInfoHeader" CollapsedImage="~/Images/icons/bullet_arrow_down.png"
        CollapsedText="Show" Enabled="True" ExpandControlID="PersonalInfoHeader" ExpandedImage="~/Images/icons/bullet_arrow_up.png"
        ExpandedText="Hide" ImageControlID="PersonalInfoPanelArrow" runat="server" TargetControlID="PersonalInfoContent"
        TextLabelID="PersonalInfoPanelLabel">
    </ajaxToolkit:CollapsiblePanelExtender>
    <%-- / Personal Information --%>
    <%-- Account Information --%>
    <asp:Panel CssClass="collapsePanelHeader" ID="AccountInfoHeader" runat="server">
        <asp:Image ID="AccountInfoPanelArrow" runat="server" />
        Account Information (<asp:Label ID="AccountInfoPanelLabel" runat="server" />)
    </asp:Panel>
    <asp:Panel ID="AccountInfoContent" runat="server" CssClass="collapsePanel">
        <fieldset class="register">
            <%-- Email --%>
            <div>
                <asp:Label AssociatedControlID="Email" runat="server" Text="* E-mail:" />
                <asp:TextBox CssClass="textEntry" ID="Email" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="Email" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter your e-mail address." runat="server"
                    ToolTip="Please enter your e-mail address." ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your e-mail address.
                </asp:RequiredFieldValidator>
                <%--Matches a valid email address including ip's which are rarely used. Allows for a-z0-9_.- in the username,
                but not ending in a full stop i.e user.@domain.com is invalid and a-z0-9- as the optional sub domain(s)
                with domain name and a 2-7 char (a-z) tld allowing for short tld's like ca and new ones like museum.--%>
                <asp:RegularExpressionValidator ControlToValidate="Email" CssClass="failureNotification"
                    Display="Dynamic" ErrorMessage="Please enter a valid e-mail address." runat="server"
                    ToolTip="Please enter a valid e-mail address." ValidationExpression="^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\`d{1,3}\.){3}\d{1,3})(:\d{4})?$"
                    ValidationGroup="UpdateParticularsValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter a valid e-mail address.
                </asp:RegularExpressionValidator>
            </div>
            <%-- / Email --%>
        </fieldset>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender CollapseControlID="AccountInfoHeader" CollapsedImage="~/Images/icons/bullet_arrow_down.png"
        CollapsedText="Show" Enabled="True" ExpandControlID="AccountInfoHeader" ExpandedImage="~/Images/icons/bullet_arrow_up.png"
        ExpandedText="Hide" ID="AccountInfoPanel" ImageControlID="AccountInfoPanelArrow"
        runat="server" TargetControlID="AccountInfoContent" TextLabelID="AccountInfoPanelLabel" />
    <%-- / Account Information --%>
    <div>
        <asp:Button CssClass="buttons" runat="server" Text="Update" ValidationGroup="UpdateParticularsValidationGroup"
            OnClick="UpdateButton_Click" />
        <asp:Button CssClass="buttons" OnClick="CancelButtonClick" runat="server" Text="Cancel" CausesValidation="False" />
    </div>
</asp:Content>
