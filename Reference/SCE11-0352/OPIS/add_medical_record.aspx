<%@ Page Title="" Language="C#" 
         MasterPageFile="~/patient.master" 
         AutoEventWireup="true" 
         CodeFile="add_medical_record.aspx.cs" 
         Inherits="add_medical_record" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="patientContentPlaceHolder" Runat="Server">
<center>
    <table style="text-align:left" width="100%">
        <tr>
            <td align="left"><b><u>Add Medical Record</u></b></td>
        </tr>
        <tr>
            <td>           
                <asp:SqlDataSource ID="DeptSqlDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>" 
                    SelectCommand="SELECT * FROM [Department]">
                </asp:SqlDataSource>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />                
                <table width="400px" cellpadding="5" cellspacing="4">
                    <tr>
                        <td>Department:</td>
                        <td>
                            <asp:DropDownList ID="deptDDL" runat="server" Width="150px" 
                                DataSourceID="DeptSqlDataSource" DataTextField="DepartmentName" 
                                DataValueField="DepartmentID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Disease Descriptions:</td>
                    </tr>
                    <tr>
                        <td colspan="2">                            
                            <asp:TextBox ID="sickDescTextBox" runat="server" 
                                Width="400px" Height="150px" 
                                TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                     <tr>
            <td align="center">
                <asp:FileUpload ID="picFileUpload" runat="server"/>                
                       <br />
                <asp:Label ID="messageLabel" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>&nbsp;</td>
                        <td align="right">
                            <asp:Button runat="server" ID="submitButton" Text="Submit" 
                                onclick="submitButton_Click" /> &nbsp;&nbsp;
                            <asp:Button runat="server" ID="resetButton" Text="Reset" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</center>
</asp:Content>