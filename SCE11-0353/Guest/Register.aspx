﻿<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8"
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em">
        <%--<LayoutTemplate>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server" />
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server" />
        </LayoutTemplate>--%>
        <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <CreateUserButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <WizardSteps>
            <asp:WizardStep ID="CreateUserWizardStep1" runat="server">
                <%--Prompt user for information and save it in SQL database--%>
                <div>
                    <h2>
                        Create a New Account (Patient)
                    </h2>
                    <p>
                        Please provide your personal information.
                    </p>
                    <p>
                        If you are not a patient, please approach the system administrator to have your
                        account created.
                    </p>
                    <p>
                        Elements marked with * are required.
                    </p>
                </div>
                <div>
                    <%--Start of error notifications--%>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server" />
                    </span>
                    <asp:ValidationSummary CssClass="failureNotification" runat="server" ValidationGroup="PersonalInfoValidationGroup" />
                </div>
                <%--End of error notifications--%>
                <%--Start of personal information entry--%>
                <div class="formDivs">
                    <asp:Panel CssClass="collapsePanelHeader" ID="PersonalInfoHeader" runat="server"
                        Wrap="False">
                        <asp:Image ID="PersonalInfoPanelArrow" runat="server" />
                        Personal Information (<asp:Label ID="PersonalInfoPanelLabel" runat="server" />)
                    </asp:Panel>
                    <asp:Panel CssClass="collapsePanel" ID="PersonalInfoContent" runat="server" Wrap="False">
                        <fieldset class="register">
                            <%--Start of NRIC--%>
                            <div>
                                <asp:Label AssociatedControlID="NRIC" runat="server" Text="* NRIC: " />
                                <asp:TextBox CssClass="textEntry" ID="NRIC" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="NRIC" CssClass="failureNotification"
                                    Display="Dynamic" ErrorMessage="Please enter your NRIC." runat="server" ToolTip="Please enter your NRIC."
                                    ValidationGroup="PersonalInfoValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> 

 Please enter your NRIC.
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="NRIC" CssClass="failureNotification"
                                    Display="Dynamic" ErrorMessage="Please enter a valid NRIC." runat="server" ToolTip="Please enter a valid NRIC."
                                    ValidationExpression="^[SFTG]\d{7}[A-Z]$" ValidationGroup="PersonalInfoValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> 

 Please enter a valid NRIC.
                                </asp:RegularExpressionValidator>
                                <%--TODO: Make sure that the NRIC is unique--%>
                                <%--<asp:CustomValidator ControlToValidate="UserName" CssClass="failureNotification"
                                        Display="Dynamic" ErrorMessage="User name is already taken." ID="CustomValidator1"
                                        OnServerValidate="UniqueUserName_Validate" runat="server" ToolTip="User name is not unique."
                                        ValidationGroup="RegisterUserValidationGroup">
                                        <asp:Image ID="Image2" ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png"
                                            runat="server" />
                                        User name is not unique.
                                    </asp:CustomValidator>--%>
                            </div>
                            <%--End of NRIC--%>
                            <%--Start of FirstName--%>
                            <div>
                                <asp:Label AssociatedControlID="FirstName" runat="server" Text="* First Name: " />
                                <asp:TextBox CssClass="textEntry" ID="FirstName" runat="server" />
                                <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                                    runat="server" TargetControlID="FirstName" />
                                <asp:RequiredFieldValidator ControlToValidate="FirstName" CssClass="failureNotification"
                                    Display="Dynamic" ErrorMessage="Please enter your first name." runat="server"
                                    ToolTip="Please enter your first name." ValidationGroup="PersonalInfoValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> 

 Please enter your first name.
                                </asp:RequiredFieldValidator>
                                <asp:CustomValidator ControlToValidate="FirstName" CssClass="failureNotification"
                                    Display="Dynamic" ErrorMessage="Please enter a valid first name." OnServerValidate="FirstName_Validate"
                                    runat="server" ToolTip="Please enter a valid first name." ValidationGroup="PersonalInfoValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> 

 Please enter a valid first name.
                                </asp:CustomValidator>
                            </div>
                            <%--End of FirstName--%>
                            <%--Start of MiddleName--%>
                            <div>
                                <asp:Label AssociatedControlID="MiddleName" runat="server" Text="Middle Name: " />
                                <asp:TextBox CssClass="textEntry" ID="MiddleName" runat="server" />
                                <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                                    runat="server" TargetControlID="MiddleName" />
                                <asp:CustomValidator ControlToValidate="MiddleName" CssClass="failureNotification"
                                    Display="Dynamic" ErrorMessage="Please enter a valid middle name." OnServerValidate="MiddleName_Validate"
                                    runat="server" ToolTip="Please enter a valid middle name." ValidationGroup="PersonalInfoValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> 

 User name is not unique.
                                </asp:CustomValidator>
                            </div>
                            <%--End of MiddleName--%>
                            <%--Start of LastName--%>
                            <div>
                                <asp:Label AssociatedControlID="LastName" runat="server" Text="* Last Name: " />
                                <asp:TextBox CssClass="textEntry" ID="LastName" runat="server" />
                                <ajaxToolkit:FilteredTextBoxExtender Enabled="True" FilterType="LowercaseLetters, UppercaseLetters"
                                    runat="server" TargetControlID="LastName" />
                                <asp:RequiredFieldValidator ControlToValidate="LastName" CssClass="failureNotification"
                                    Display="Dynamic" ErrorMessage="Please enter your last name." runat="server"
                                    ToolTip="Please enter your last name." ValidationGroup="PersonalInfoValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> 

 Please enter your last name.
                                </asp:RequiredFieldValidator>
                            </div>
                            <%--End of LastName--%>
                            <%--Start of Gender--%>
                            <div>
                                <asp:Label AssociatedControlID="Gender" runat="server" Text="* Gender: " />
                                <asp:RadioButtonList ID="Gender" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Text="Male" Value="male" />
                                    <asp:ListItem Text="Female" Value="female" />
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ControlToValidate="Gender" CssClass="failureNotification"
                                    Display="Dynamic" ErrorMessage="Please specify your gender." runat="server" ToolTip="Please specify your gender."
                                    ValidationGroup="PersonalInfoValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> 

 Please specify your gender.
                                </asp:RequiredFieldValidator>
                            </div>
                            <%--End of Gender--%>
                            <%--Start of Prefix--%>
                            <div>
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
                                    ToolTip="Please specify a salutation." ValidationGroup="PersonalInfoValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" /> 

 Please specify a salutation.
                                </asp:RequiredFieldValidator>
                            </div>
                            <%--End of Prefix--%>
                            <%--Start of Suffix--%>
                            <div>
                                <asp:Label AssociatedControlID="Suffix" runat="server" Text="Suffix: " />
                                <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" DropDownStyle="DropDownList" ID="Suffix"
                                    runat="server">
                                    <asp:ListItem Text="" Value="" />
                                    <asp:ListItem Text="Jr." Value="Jr." />
                                    <asp:ListItem Text="Sr." Value="Sr." />
                                </ajaxToolkit:ComboBox>
                            </div>
                            <%--End of Suffix--%>
                            <%--Start of DOB--%>
                            <div>
                                <asp:Label AssociatedControlID="Prefix" runat="server" Text="* Date of Birth (dd/mm/yyyy): " />
                                <asp:TextBox CssClass="textEntry" ID="DateOfBirth" runat="server" />
                                <asp:ImageButton ID="DOB_Cal" ImageUrl="~/Images/icons/calendar.png" runat="server" />
                                <ajaxToolkit:CalendarExtender ClearTime="True" DaysModeTitleFormat="dd/MM/yyyy" Enabled="True"
                                    FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="DOB_Cal" runat="server"
                                    StartDate="1/1/1940" TargetControlID="DateOfBirth" TodaysDateFormat="dd/MM/yyyy" />
                                <ajaxToolkit:MaskedEditExtender AutoComplete="False" ClearTextOnInvalid="False" ClipboardEnabled="False"
                                    Enabled="True" ID="DOB_Input" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="DateOfBirth" />
                                <ajaxToolkit:MaskedEditValidator ControlExtender="DOB_Input" ControlToValidate="DateOfBirth"
                                    CssClass="failureNotification" Display="Dynamic" EmptyValueBlurredText="Please specify your birth date."
                                    EmptyValueMessage="Please specify your birth date." InvalidValueBlurredMessage="Please specify a valid birth date."
                                    InvalidValueMessage="Please specify a valid birth date." IsValidEmpty="False"
                                    runat="server" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"
                                    ValidationGroup="PersonalInfoValidationGroup" />
                                <%--<asp:RangeValidator runat="server" ErrorMessage="RangeValidator" />--%>
                            </div>
                            <%--End of DOB--%>
                        </fieldset>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender CollapseControlID="PersonalInfoHeader" CollapsedImage="~/Images/icons/bullet_arrow_down.png"
                        CollapsedText="Show" Enabled="True" ExpandControlID="PersonalInfoHeader" ExpandedImage="~/Images/icons/bullet_arrow_up.png"
                        ExpandedText="Hide" ImageControlID="PersonalInfoPanelArrow" runat="server" TargetControlID="PersonalInfoContent"
                        TextLabelID="PersonalInfoPanelLabel">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </div>
                <%--End of personal information entry--%>
                <div>
                    <asp:Button CommandName="MoveNext" ID="Step1Button" runat="server" Text="Continue"
                        ValidationGroup="PersonalInfoValidationGroup" />
                </div>
            </asp:WizardStep>
            <%--Start of account information entry--%>
            <asp:CreateUserWizardStep runat="server" ID="CreateUserWizardStep2" Title="">
                <ContentTemplate>
                    <div class="formDivs">
                        <asp:Panel CssClass="collapsePanelHeader" ID="AccountInfoHeader" runat="server">
                            <asp:Image ID="AccountInfoPanelArrow" runat="server" />
                            Account Information (<asp:Label ID="AccountInfoPanelLabel" runat="server" />)
                        </asp:Panel>
                        <asp:Panel ID="AccountInfoContent" runat="server" CssClass="collapsePanel">
                            <fieldset class="register">
                                <%--Start of username--%>
                                <div>
                                    <asp:Label AssociatedControlID="UserName" runat="server" Text="* User Name: " />
                                    <asp:TextBox CssClass="textEntry" ID="UserName" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="UserName" CssClass="failureNotification"
                                        Display="Dynamic" ErrorMessage="Please enter your user name." runat="server"
                                        ToolTip="Please enter your desired user name." ValidationGroup="RegisterUserValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                        Please enter your desired user name.
                                    </asp:RequiredFieldValidator>
                                    <asp:CustomValidator ControlToValidate="UserName" CssClass="failureNotification"
                                        Display="Dynamic" ErrorMessage="User name is already taken." OnServerValidate="UniqueUserName_Validate"
                                        runat="server" ToolTip="User name is not unique." ValidationGroup="RegisterUserValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                        User name is not unique.
                                    </asp:CustomValidator>
                                </div>
                                <%--End of username--%>
                                <%--Start of email--%>
                                <div>
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
                                        Display="Dynamic" ErrorMessage="Please enter a valid email address." runat="server"
                                        ToolTip="Please enter a valid email address." ValidationExpression="^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\`d{1,3}\.){3}\d{1,3})(:\d{4})?$"
                                        ValidationGroup="RegisterUserValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                        Please enter a valid email address.
                                    </asp:RegularExpressionValidator>
                                    <asp:CustomValidator ControlToValidate="Email" CssClass="failureNotification" Display="Dynamic"
                                        ErrorMessage="Email address is not unique." OnServerValidate="UniqueEmail_Validate"
                                        runat="server" ToolTip="Email address is already in use." ValidationGroup="RegisterUserValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                        Email address is not unique.
                                    </asp:CustomValidator>
                                </div>
                                <%--End of email--%>
                                <%--Start of password--%>
                                <div>
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
                                <%--End of password--%>
                                <%--Start of confirm password--%>
                                <div>
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
                                        runat="server" ValidationGroup="RegisterUserValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                        Both passwords must match.
                                    </asp:CompareValidator>
                                </div>
                                <%--End of confirm password--%>
                                <%--Start of security question--%>
                                <div>
                                    <asp:Label AssociatedControlID="Question" runat="server" Text="* Security Question:" />
                                    <asp:TextBox CssClass="textEntry" ID="Question" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="Question" CssClass="failureNotification"
                                        Display="Dynamic" ErrorMessage="Security question is required." runat="server"
                                        ToolTip="Please provide a secret question." ValidationGroup="RegisterUserValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                                        Please provide a secret question.
                                    </asp:RequiredFieldValidator>
                                </div>
                                <%--End of security question--%>
                                <%--Start of security answer--%>
                                <div>
                                    <asp:Label AssociatedControlID="Answer" runat="server" Text="* Security Answer:" />
                                    <asp:TextBox CssClass="textEntry" ID="Answer" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="Answer" CssClass="failureNotification"
                                        Display="Dynamic" ErrorMessage="Security answer is required." runat="server"
                                        ToolTip="Please enter the answer to your secret question." ValidationGroup="RegisterUserValidationGroup">
                                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png"
                                            runat="server" />
                                        Please enter the answer to your secret question.
                                    </asp:RequiredFieldValidator>
                                </div>
                                <%--End of security answer--%>
                            </fieldset>
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender CollapseControlID="AccountInfoHeader" CollapsedImage="~/Images/icons/bullet_arrow_down.png"
                            CollapsedText="Show" Enabled="True" ExpandControlID="AccountInfoHeader" ExpandedImage="~/Images/icons/bullet_arrow_up.png"
                            ExpandedText="Hide" ID="AccountInfoPanel" ImageControlID="AccountInfoPanelArrow"
                            runat="server" TargetControlID="AccountInfoContent" TextLabelID="AccountInfoPanelLabel" />
                    </div>
                    <%--End of account information entry--%>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <%--<div class="formDivs">
                <asp:Button CommandName="MoveNext" ID="RegisterButton" runat="server" Text="Continue"
                    ValidationGroup="RegisterUserValidationGroup" />
                <asp:Button ID="CancelButton" PostBackUrl="~/Default.aspx" runat="server" Text="Cancel" />
            </div>--%>
            <asp:CompleteWizardStep runat="server" Title="">
                <ContentTemplate>
                    <div>
                        <h2>
                            Create a New Account (Patient)
                        </h2>
                    </div>
                    <div>
                        <h3>
                            Account successfully created.
                        </h3>
                        <p>
                            Please proceed to update your personal information.
                        </p>
                    </div>
                    <div>
                        <asp:Button ID="FinishButton" PostBackUrl="~/Account/UpdateParticulars.aspx" runat="server"
                            Text="Finish" />
                        <asp:Button ID="CancelButton2" runat="server" Text="Cancel" />
                    </div>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
        <HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True" Font-Size="0.9em"
            ForeColor="White" HorizontalAlign="Center" />
        <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
        <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
        <SideBarStyle BackColor="#5D7B9D" BorderWidth="0px" Font-Size="0.9em" VerticalAlign="Top" />
        <StepStyle BorderWidth="0px" />
    </asp:CreateUserWizard>
</asp:Content>
