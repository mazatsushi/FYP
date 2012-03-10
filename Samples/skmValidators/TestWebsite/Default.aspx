<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="skmValidators" Namespace="skmValidators" TagPrefix="skm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            CheckBoxValidator &amp; CheckBoxListValidator Demos</h1>
        <p>
            This demo illustrates using the CheckBoxValidator and CheckBoxListValidator controls.</p>
        <hr />
        <h3>Demo 1: CheckBoxValidator with MustBeChecked = True (the default)</h3>
        <p>Please enter your email address:
            <asp:TextBox ID="EmailAddress" runat="server" Columns="40">scott@example.com</asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="EmailAddress"
                Display="Dynamic" ErrorMessage="You must enter an email address." ValidationGroup="Demo1">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailAddress"
                Display="Dynamic" ErrorMessage="You must enter a properly formatted email address."
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Demo1">*</asp:RegularExpressionValidator><br />
            <br />
            <em>Blah blah blah... boring legal disclaimer and text... blah blah blah<br />
            </em>
            <br />
            <asp:CheckBox ID="AgreeToLegalDisclaimer" runat="server" Text="I agree to the terms of service" />
                <skm:CheckBoxValidator runat="server" id="CheckBoxValidator1" ControlToValidate="AgreeToLegalDisclaimer" Display="Dynamic" ErrorMessage="You must agree to the terms of service before you can continue." ValidationGroup="Demo1" AssociatedButtonControlId="">*</skm:CheckBoxValidator>
            <br />
            <br />
            <asp:Button ID="SubmitFirstDemo" runat="server" Text="Continue -->" ValidationGroup="Demo1" /><br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Demo1" />
        </p>
        <hr />
        <h3>Demo 2: CheckBoxValidator with AssociatedButtonControlId Set and MustBeChecked =
            False</h3>
        <p>Please enter your email address:
            <asp:TextBox ID="TextBox1" runat="server" Columns="40">scott@example.com</asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EmailAddress"
                Display="Dynamic" ErrorMessage="You must enter an email address." ValidationGroup="Demo1">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="EmailAddress"
                Display="Dynamic" ErrorMessage="You must enter a properly formatted email address."
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Demo1">*</asp:RegularExpressionValidator><br />
            <br />
            <em>Blah blah blah... boring legal disclaimer and text... blah blah blah</em></p>
        <p>
            <strong>NOTE:</strong> For this example, you must <em>uncheck</em> the CheckBox
            in order to continue...<br />
            <br />
            <asp:CheckBox ID="IAgreeIAgree" runat="server" Text="I DISagree to the terms of service" Checked="True" />
                <skm:CheckBoxValidator runat="server" id="CheckBoxValidator2" ControlToValidate="IAgreeIAgree" Display="Dynamic" ErrorMessage='You must agree to the terms of service before you can continue. (In other words, you must uncheck the "I DISagree to the terms of service" checkbox.)' ValidationGroup="Demo2" AssociatedButtonControlId="SubmitSecondDemo" MustBeChecked="False">*</skm:CheckBoxValidator>
        </p>
        <p>
            <asp:Button ID="SubmitSecondDemo" runat="server" Text="Continue -->" ValidationGroup="Demo2" /><br />
            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Demo2" />
        </p>
        <hr />
        <h3>
            Demo 3: CheckBoxListValidator with MinimumNumberOfSelectedCheckBoxes = 1 (the default)</h3>
        <p>
            What is your name:
            <asp:TextBox ID="YourName" runat="server">Scott Mitchell</asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="Demo2" ID="RequiredFieldValidator2" runat="server" ControlToValidate="YourName"
                Display="Dynamic" ErrorMessage="Please enter your name.">*</asp:RequiredFieldValidator></p>
        <p>
            Please select the programming languages you've used:</p>
        <p>
            <asp:CheckBoxList ID="LanguagesList" runat="server">
                <asp:ListItem>C#</asp:ListItem>
                <asp:ListItem>Visual Basic</asp:ListItem>
                <asp:ListItem>C++</asp:ListItem>
                <asp:ListItem>Pascal</asp:ListItem>
                <asp:ListItem>LISP</asp:ListItem>
                <asp:ListItem>SmallTalk</asp:ListItem>
                <asp:ListItem>ML</asp:ListItem>
                <asp:ListItem>COBOL</asp:ListItem>
                <asp:ListItem>Fortran</asp:ListItem>
                <asp:ListItem>Prolog</asp:ListItem>
            </asp:CheckBoxList>&nbsp;
                <skm:CheckBoxListValidator runat="server" id="CheckBoxListValidator1" ControlToValidate="LanguagesList" Display="Dynamic" ErrorMessage="You must select one or more programming languages." ValidationGroup="Demo3"></skm:CheckBoxListValidator>
            </p>
        <p>
            <asp:Button ID="SubmitThirdDemo" runat="server" Text="Continue -->" ValidationGroup="Demo3" />
        </p>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Demo3" />
            &nbsp;
                <hr />
        <h3>
            Demo 4: CheckBoxListValidator with MinimumNumberOfSelectedCheckBoxes = 2</h3>
        <p>
            Please select two or more items you're interested in:</p>
        <p>
            <asp:CheckBoxList ID="ItemsOfInterest" runat="server">
                <asp:ListItem>Stocks</asp:ListItem>
                <asp:ListItem>Bonds</asp:ListItem>
                <asp:ListItem>ETFs</asp:ListItem>
                <asp:ListItem>Annuities</asp:ListItem>
                <asp:ListItem>T-Bills</asp:ListItem>
                <asp:ListItem>REITs</asp:ListItem>
            </asp:CheckBoxList>
                <skm:CheckBoxListValidator runat="server" id="CheckBoxListValidator2" ControlToValidate="ItemsOfInterest" Display="Dynamic" ErrorMessage="You must select -two- or more items of interest." ValidationGroup="Demo4" MinimumNumberOfSelectedCheckBoxes="2"></skm:CheckBoxListValidator>
            </p>
        <p>
            <asp:Button ID="SubmitFourthDemo" runat="server" Text="Continue -->" ValidationGroup="Demo4" />
        </p>
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="Demo4" />
            &nbsp;
    </div>
    </form>
</body>
</html>
