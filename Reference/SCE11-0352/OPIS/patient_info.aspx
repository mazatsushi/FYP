<%@ Page Title="" Language="C#" MasterPageFile="~/patient.master" AutoEventWireup="true" CodeFile="patient_info.aspx.cs" Inherits="patient_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="patientContentPlaceHolder" ContentPlaceHolderID="patientContentPlaceHolder" Runat="Server">
    <center>

        <asp:SqlDataSource ID="patSqlDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>" 
            
            SelectCommand="SELECT p.userID, p.Name, p.Gender, p.Age, p.NRIC, p.Nationality, p.Phone_number, p.Email, p.Address, p.Image FROM Patientinfo AS p INNER JOIN userinfo AS u ON p.userID = u.userId WHERE (u.userId = @UserID)">
            <SelectParameters>
                <asp:SessionParameter Name="UserID" SessionField="UserId" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:DetailsView ID="patDetailsView" runat="server" AutoGenerateRows="False" 
            DataKeyNames="NRIC" DataSourceID="patSqlDataSource" Height="50px" Width="125px">
            <Fields>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:TemplateField HeaderText="Image" SortExpression="Image">
                    <ItemTemplate>                               
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# "Handlerpat.ashx?ID=" + Eval("userId")%>'/>
                                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                <asp:BoundField DataField="NRIC" HeaderText="NRIC" ReadOnly="True" SortExpression="NRIC" />
                <asp:BoundField DataField="Nationality" HeaderText="Nationality" SortExpression="Nationality" />
                <asp:BoundField DataField="Phone_number" HeaderText="Phone_number" SortExpression="Phone_number" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
            </Fields>
        </asp:DetailsView>
<td align="center">
                <asp:FileUpload ID="picFileUpload" runat="server"/>                
                <asp:Button ID="uploadButton" runat="server" OnClick="uploadButton_Click" Text="Upload"/>
                <br />
                <asp:Label ID="messageLabel" runat="server" ForeColor="Red"></asp:Label>
            </td>
</center>
</asp:Content>

