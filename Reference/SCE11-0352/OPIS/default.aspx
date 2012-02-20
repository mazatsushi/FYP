<%@ Page Language="C#" AutoEventWireup="true"  
         CodeFile="default.aspx.cs" Inherits="_default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
                      "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Outpatient Information System</title>
</head>

<body>
    <center><h1>Outpatient Information System</h1></center>
    <br />
    <form id="form1" runat="server">
    <div>
    <center>
        <table width="400px" style="text-align:left" >
            <tr>
                <td style="text-align:center; border:1;background-color:#FFC0CB" colspan="2">Login Page</td>
            </tr>
            <tr>
                <td style="width:100px">User Name:</td>
                <td>
                    <asp:TextBox runat="server" ID="userTextBox" Width="200px" TextMode="SingleLine"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td style="width:100px">Password:</td>
                <td>
                    <asp:TextBox runat="server" ID="pwdTextBox" Width="200px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td style="width:100px">User Role:</td>
                <td>
                    <asp:DropDownList runat="server" ID="roleDDL" Width="100px">
                        <asp:ListItem Value="1" Text="Doctor"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Patient"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="color:Red">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="false"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" id="loginButton" Text="Login" 
                        onclick="loginButton_Click" />
                </td>
            </tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
