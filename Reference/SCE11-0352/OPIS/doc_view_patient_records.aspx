<%@ Page Title="" Language="C#" MasterPageFile="~/doctor.master" AutoEventWireup="true" CodeFile="doc_view_patient_records.aspx.cs" Inherits="doc_view_patient_records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="doctorContentPlaceHolder" Runat="Server">

    <center>
    <asp:SqlDataSource ID="viewPatSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>" 
        SelectCommand="SELECT DISTINCT p.userID, p.Name, p.Gender, p.Age, p.NRIC, p.Nationality, p.Phone_number, p.Email, p.Address FROM Patientinfo AS p INNER JOIN MedicalRecord AS m ON p.userID = m.userID WHERE (m.DepartmentID = @deptIDParam)">
        <SelectParameters>
            <asp:SessionParameter Name="deptIDParam" SessionField="DeptID" Type="Int16" DefaultValue="" />
        </SelectParameters>
    </asp:SqlDataSource> 
    
    <asp:GridView ID="viewPatGridView" runat="server" 
        AllowPaging="True" 
        AllowSorting="True" 
        AutoGenerateColumns="False" 
        DataSourceID="viewPatSqlDataSource" 
        EnableModelValidation="True" 
        DataKeyNames="userID" 
        Width="500px">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
            <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
            <asp:BoundField DataField="NRIC" HeaderText="NRIC" SortExpression="NRIC" ReadOnly="True" />
            <asp:BoundField DataField="Nationality" HeaderText="Nationality" SortExpression="Nationality" />
            <asp:BoundField DataField="Phone_number" HeaderText="Phone Number" SortExpression="Phone_number" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="selectionLabel" runat="server" Text="View Medical"  Font-Size="XX-Small">
                    </asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="selectMedbutton" runat="server" PostBackUrl='<%# "view_medical_records.aspx?NRIC=" + Eval("NRIC")%>'>Select Me</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>      
        </Columns>
    </asp:GridView>
    <br />
    
</center>
</asp:Content>

