<%@ Page AutoEventWireup="true" CodeFile="Register.aspx.cs" Culture="en-SG" Inherits="Account_Register"
    Language="C#" MasterPageFile="~/Site.master" Title="Register" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <h2>
                    Create a New Account (Patient)
                </h2>
                <p>
                    If you are not a patient, please approach the system administrator to have your
                    account created.
                </p>
                <p>
                    Passwords must have at least
                    <%= Membership.MinRequiredPasswordLength %>
                    characters with at least
                    <%= Membership.MinRequiredNonAlphanumericCharacters %>
                    non-alphanumeric characters.
                </p>
                <p>
                    Elements marked with * are required.
                </p>
            </div>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" Text="" />
                <asp:ValidationSummary runat="server" ValidationGroup="RegisterUserValidationGroup" />
            </div>
            <%-- / Error Notifications --%>
            <%-- Personal Information --%>
            <asp:Panel CssClass="collapsePanelHeader" ID="PersonalInfoHeader" runat="server">
                <asp:Image ID="PersonalInfoPanelArrow" runat="server" />
                Personal Information (<asp:Label ID="PersonalInfoPanelLabel" runat="server" />)
            </asp:Panel>
            <asp:Panel CssClass="collapsePanel" ID="PersonalInfoContent" runat="server">
                <fieldset class="register">
                    <%-- NRIC --%>
                    <div>
                        <asp:Label AssociatedControlID="NRIC" runat="server" Text="* NRIC: " />
                        <asp:TextBox CssClass="textEntry" ID="NRIC" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="NRIC" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter your NRIC." runat="server" ToolTip="Please enter your NRIC."
                            ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your NRIC.
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="NRIC" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter a valid NRIC." runat="server" ToolTip="Please enter a valid NRIC."
                            ValidationExpression="^[SFTG]\d{7}[A-Z]$" ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter a valid NRIC.
                        </asp:RegularExpressionValidator>
                        <asp:CustomValidator ControlToValidate="NRIC" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="NRIC already in use." OnServerValidate="NricNotExists" runat="server"
                            ToolTip="NRIC already in use." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    NRIC already in use.
                        </asp:CustomValidator>
                    </div>
                    <%-- / NRIC --%>
                    <%-- / First Name --%>
                    <div class="topPadding">
                        <asp:Label AssociatedControlID="FirstName" runat="server" Text="* First Name: " />
                        <asp:TextBox CssClass="textEntry" ID="FirstName" runat="server" />
                        <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                            runat="server" TargetControlID="FirstName" />
                        <asp:RequiredFieldValidator ControlToValidate="FirstName" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter your first name." runat="server"
                            ToolTip="Please enter your first name." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your first name.
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ControlToValidate="FirstName" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter a valid first name." OnServerValidate="IsFirstNameValid"
                            runat="server" ToolTip="Please enter a valid first name." ValidationGroup="RegisterUserValidationGroup">
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
                            runat="server" ToolTip="Please enter a valid middle name." ValidationGroup="RegisterUserValidationGroup">
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
                            ToolTip="Please enter your last name." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your last name.
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ControlToValidate="LastName" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter a valid last name." OnServerValidate="IsLastNameValid"
                            runat="server" ToolTip="Please enter a valid last name." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter a valid last name.
                        </asp:CustomValidator>
                    </div>
                    <%-- / Last Name --%>
                    <%-- Gender --%>
                    <div class="topPadding">
                        <asp:Label AssociatedControlID="Gender" runat="server" Text="* Gender: " />
                        <asp:RadioButtonList CssClass="float" ID="Gender" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="Male" Value="m" />
                            <asp:ListItem Text="Female" Value="f" />
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ControlToValidate="Gender" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify your gender." runat="server" ToolTip="Please specify your gender."
                            ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify your gender.
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ControlToValidate="Gender" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="Please specify a valid gender." OnServerValidate="IsGenderValid"
                            runat="server" ToolTip="Please specify a valid gender." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify a valid gender.
                        </asp:CustomValidator>
                    </div>
                    <%-- / Gender --%>
                    <%-- Prefix --%>
                    <div class="clear topPadding">
                        <asp:Label AssociatedControlID="Prefix" runat="server" Text="* Salutation: " />
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
                            ToolTip="Please specify a salutation." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify a salutation.
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ControlToValidate="Prefix" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="Please specify a valid salutation." OnServerValidate="IsPrefixValid"
                            runat="server" ToolTip="Please specify a valid salutation." ValidationGroup="RegisterUserValidationGroup">
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
                            runat="server" ToolTip="Please specify a valid suffix." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify a valid suffix.
                        </asp:CustomValidator>
                    </div>
                    <%-- / Suffix --%>
                    <%-- DOB --%>
                    <div class="topPadding">
                        <asp:Label AssociatedControlID="DateOfBirth" runat="server" Text="* Date of Birth (dd/mm/yyyy): " />
                        <asp:TextBox CssClass="textEntry" ID="DateOfBirth" runat="server" />
                        <asp:ImageButton ID="DOB_Cal" ImageUrl="~/Images/icons/calendar.png" runat="server" />
                        <ajaxToolkit:CalendarExtender ClearTime="True" DaysModeTitleFormat="dd/MM/yyyy" Enabled="True"
                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="DOB_Cal" runat="server"
                            StartDate="1/1/1940" TargetControlID="DateOfBirth" TodaysDateFormat="dd/MM/yyyy"
                            PopupPosition="TopLeft" />
                        <ajaxToolkit:MaskedEditExtender AutoComplete="False" ClearTextOnInvalid="False" ClipboardEnabled="False"
                            CultureName="en-SG" Enabled="True" ID="DOB_Input" Mask="99/99/9999" MaskType="Date"
                            runat="server" TargetControlID="DateOfBirth" />
                        <ajaxToolkit:MaskedEditValidator ControlExtender="DOB_Input" ControlToValidate="DateOfBirth"
                            CssClass="failureNotification" Display="Dynamic" EmptyValueBlurredText="<img src='../Images/icons/error.png'> Please specify your date of birth."
                            EmptyValueMessage="Please specify your date of birth." InvalidValueBlurredMessage="<img src='../Images/icons/error.png'> Please specify a valid date of birth."
                            InvalidValueMessage="Please specify a valid date of birth." IsValidEmpty="False"
                            runat="server" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                            ValidationGroup="RegisterUserValidationGroup" />
                        <asp:RangeValidator ControlToValidate="DateOfBirth" CssClass="failureNotification"
                            ErrorMessage="Please specify a valid date of birth." ID="DateRangeCheck" runat="server"
                            Type="Date" ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify a valid date of birth.
                        </asp:RangeValidator>
                    </div>
                    <%-- / DOB --%>
                    <%-- Address --%>
                    <div class="topPadding">
                        <asp:Label AssociatedControlID="Address" runat="server" Text="* Address: " />
                        <asp:TextBox CssClass="textEntry" ID="Address" runat="server" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ControlToValidate="Address" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify an address." runat="server" ToolTip="Please specify an address."
                            ValidationGroup="RegisterUserValidationGroup">
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
                            runat="server" ValidationExpression="^[3689]\d{7}$" ValidationGroup="RegisterUserValidationGroup" />
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
                            ValidationExpression="\d{6}$" ValidationGroup="RegisterUserValidationGroup" />
                    </div>
                    <%-- / Postal Code --%>
                    <%-- Country --%>
                    <div class="topPadding">
                        <asp:Label AssociatedControlID="Country" runat="server" Text="* Country of Residence: " />
                        <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" CssClass="float" ID="Country" MaxLength="0"
                            runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="Country" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify your country of residence." runat="server"
                            ToolTip="Please specify your country of residence." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify your country of residence.
                        </asp:RequiredFieldValidator>
                    </div>
                    <%-- / Country --%>
                    <%-- Nationality --%>
                    <div class="clear topPadding">
                        <asp:Label AssociatedControlID="Nationality" runat="server" Text="* Nationality: " />
                        <asp:TextBox CssClass="textEntry" ID="Nationality" runat="server" />
                        <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                            runat="server" TargetControlID="Nationality" />
                        <asp:RequiredFieldValidator ControlToValidate="Nationality" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify your nationality." runat="server"
                            ToolTip="Please specify your nationality." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify your nationality.
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ControlToValidate="Nationality" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify a valid nationality." OnServerValidate="IsNationalityValid"
                            runat="server" ToolTip="Please specify a valid nationality." ValidationGroup="RegisterUserValidationGroup">
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
            <asp:Panel CssClass="collapsePanel" ID="AccountInfoContent" runat="server">
                <fieldset class="register">
                    <%-- Username --%>
                    <div runat="server">
                        <asp:Label AssociatedControlID="UserName" runat="server" Text="* User Name: " />
                        <asp:TextBox CssClass="textEntry" ID="UserName" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="UserName" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter your user name." runat="server"
                            ToolTip="Please enter your user name." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your user name.
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ControlToValidate="UserName" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="User name is already in use." OnServerValidate="UserNameNotExists"
                            runat="server" ToolTip="User name is already in use." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    User name is already in use.
                        </asp:CustomValidator>
                    </div>
                    <%-- / Username --%>
                    <%-- Email --%>
                    <div cssclass="topPadding" runat="server">
                        <asp:Label AssociatedControlID="Email" runat="server" Text="* E-mail:" />
                        <asp:TextBox CssClass="textEntry" ID="Email" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="Email" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter your e-mail address." runat="server"
                            ToolTip="Please enter your e-mail address." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your e-mail address.
                        </asp:RequiredFieldValidator>
                        <%--Matches a valid email address including ip's which are rarely used. Allows for a-z0-9_.- in the username,
                but not ending in a full stop i.e user.@domain.com is invalid and a-z0-9- as the optional sub domain(s)
                with domain name and a 2-7 char (a-z) tld allowing for short tld's like ca and new ones like museum.--%>
                        <asp:RegularExpressionValidator ControlToValidate="Email" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter a valid e-mail address." runat="server"
                            ToolTip="Please enter a valid e-mail address." ValidationExpression="^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\`d{1,3}\.){3}\d{1,3})(:\d{4})?$"
                            ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter a valid e-mail address.
                        </asp:RegularExpressionValidator>
                        <asp:CustomValidator ControlToValidate="Email" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="E-mail address is already in use." OnServerValidate="EmailNotInUse"
                            runat="server" ToolTip="Em-ail address is already in use." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    E-mail address is already in use.
                        </asp:CustomValidator>
                    </div>
                    <%-- / Email --%>
                    <%-- Password --%>
                    <div cssclass="topPadding" runat="server">
                        <asp:Label AssociatedControlID="Password" runat="server" Text="* Password:" />
                        <asp:TextBox CssClass="passwordEntry" ID="Password" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ControlToValidate="Password" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please enter your password." runat="server" ToolTip="Please enter your password."
                            ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please enter your password.
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:PasswordStrength BarBorderCssClass="BarBorder" BarIndicatorCssClass="BarIndicator"
                            DisplayPosition="RightSide" Enabled="True" HelpStatusLabelID="PasswordStrengthLabel"
                            MinimumNumericCharacters="1" MinimumSymbolCharacters="1" PreferredPasswordLength="6"
                            runat="server" StrengthIndicatorType="BarIndicator" TargetControlID="Password" />
                        <br />
                        <asp:Label ID="PasswordStrengthLabel" runat="server" Text="Label" />
                    </div>
                    <%-- / Password --%>
                    <%-- Confirm Password --%>
                    <div cssclass="topPadding" runat="server">
                        <asp:Label AssociatedControlID="ConfirmPassword" runat="server" Text="* Confirm Password:" />
                        <asp:TextBox CssClass="passwordEntry" ID="ConfirmPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please re-enter password." runat="server" ToolTip="Please enter re-password."
                            ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please re-enter password.
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Both passwords must match."
                            runat="server" ToolTip="Both passwords must match." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Both passwords must match.
                        </asp:CompareValidator>
                    </div>
                    <%-- / Confirm Password --%>
                    <%-- Security Question --%>
                    <div cssclass="topPadding" runat="server">
                        <asp:Label AssociatedControlID="Question" runat="server" Text="* Security Question:" />
                        <asp:TextBox CssClass="textEntry" ID="Question" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="Question" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Security question is required." runat="server"
                            ToolTip="Security question is required." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Security question is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <%-- / Security Question --%>
                    <%-- Security Answer --%>
                    <div cssclass="topPadding" runat="server">
                        <asp:Label AssociatedControlID="Answer" runat="server" Text="* Security Answer:" />
                        <asp:TextBox CssClass="textEntry" ID="Answer" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="Answer" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Security answer is required." runat="server"
                            ToolTip="Security answer is required." ValidationGroup="RegisterUserValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Security answer is required.
                        </asp:RequiredFieldValidator>
                    </div>
                    <%-- / Security Answer --%>
                </fieldset>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender CollapseControlID="AccountInfoHeader" CollapsedImage="~/Images/icons/bullet_arrow_down.png"
                CollapsedText="Show" Enabled="True" ExpandControlID="AccountInfoHeader" ExpandedImage="~/Images/icons/bullet_arrow_up.png"
                ExpandedText="Hide" ID="AccountInfoPanel" ImageControlID="AccountInfoPanelArrow"
                runat="server" TargetControlID="AccountInfoContent" TextLabelID="AccountInfoPanelLabel" />
            <%-- / Account Information --%>
            <div runat="server">
                <asp:Button CssClass="buttons" OnClick="RegisterButton_Click" runat="server" Text="Create User"
                    ValidationGroup="RegisterUserValidationGroup" />
                <asp:Button CssClass="buttons" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
