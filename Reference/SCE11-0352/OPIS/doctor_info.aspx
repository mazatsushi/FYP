<%@ Page Title="" Language="C#" MasterPageFile="~/doctor.master" AutoEventWireup="true" CodeFile="doctor_info.aspx.cs" Inherits="doctor_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="doctorContentPlaceHolder" ContentPlaceHolderID="doctorContentPlaceHolder" Runat="Server">
    <center>
    <table style="text-align:left" width="100%">
        <tr>
            <td align="left"><b><u>Doctor Particular</u></b></td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="docSqlDataSource" runat="server"
                    ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>" 
                    SelectCommand="SELECT d.DepartmentName, p.* FROM Doctorinfo p, userinfo u, Department d WHERE p.userId = u.userId AND d.DepartmentID = p.DepartmentID AND u.userId = @UserID" 
                    >
                    <SelectParameters>
                        <asp:SessionParameter Name="userID" SessionField="UserId" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <table width="400px" cellpadding="5">
                <tr>
                    <td>
                        <asp:DetailsView ID="docDetailsView" runat="server" 
                        Height="90px" Width="400px"
                        DataSourceID="docSqlDataSource"
                        DataKeyNames="userId" AutoGenerateRows="False" 
                        EnableModelValidation="True" >
                        <Fields>
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" 
                                SortExpression="DepartmentName"  />
                            <asp:TemplateField HeaderText="Image" >                                    
                                <ItemTemplate>                               
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# "Handler.ashx?ID=" + Eval("userId")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            <asp:BoundField DataField="Phone_number" HeaderText="Phone Number" SortExpression="Phone_number" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />                            
                        </Fields>
                    </asp:DetailsView>
                    </td>
                </tr>                  
                </table>
            </td>            
        </tr>
        <tr>
            <td align="center">
                <asp:FileUpload ID="picFileUpload" runat="server"/>                
                <asp:Button ID="uploadButton" runat="server" OnClick="uploadButton_Click" Text="Upload"/>
                <br />
                <asp:Label ID="messageLabel" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</center>

</asp:Content>

