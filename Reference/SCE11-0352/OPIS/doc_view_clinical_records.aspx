<%@ Page Title="" Language="C#" MasterPageFile="~/doctor.master" AutoEventWireup="true" CodeFile="doc_view_patient_records.aspx.cs" Inherits="doc_view_patient_records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="doctorContentPlaceHolder" Runat="Server">

<center>
 <asp:SqlDataSource ID="viewPatSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>" 
        SelectCommand="SELECT p.Name, p.Gender, p.Age, m.RecordID, m.SickDescription FROM Patientinfo AS p INNER JOIN MedicalRecord AS m ON p.userID = m.userID WHERE (m.DepartmentID = @deptIDParam)">
        <SelectParameters>
            <asp:SessionParameter Name="deptIDParam" SessionField="DeptID" Type="Int16" 
                DefaultValue="" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="viewMedSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>"         
        SelectCommand="SELECT RecordID, DepartmentID, SickDescription, DoctorComment, userID,Image
 FROM MedicalRecord WHERE (RecordID = @recordIDParam)" 
        UpdateCommand="UPDATE MedicalRecord SET DoctorComment = @commentParam WHERE (RecordID = @recordIDParam)"
        >
        <SelectParameters>
            <asp:ControlParameter ControlID="viewPatGridView" Name="recordIDParam" 
                PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:ControlParameter ControlID="viewPatDetailsView$commentTextBox" PropertyName="Text" Name="commentParam" Type="String" />       
            <asp:ControlParameter ControlID="viewPatGridView" PropertyName="SelectedValue" Name="recordIDParam" />
        </UpdateParameters>
        
    </asp:SqlDataSource>
    
    <asp:GridView ID="viewPatGridView" runat="server" 
        AllowPaging="True" 
        AllowSorting="True" 
        AutoGenerateColumns="False" 
        DataSourceID="viewPatSqlDataSource" 
        EnableModelValidation="True" 
        DataKeyNames="RecordID" 
        Width="500px">
        <Columns>
            <asp:BoundField DataField="RecordID" HeaderText="RecordID" 
                SortExpression="RecordID" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" 
                SortExpression="Gender" />
            <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
            <asp:BoundField DataField="SickDescription" HeaderText="SickDescription" 
                SortExpression="SickDescription" />
            <asp:CommandField ShowSelectButton="true" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:DetailsView ID="viewPatDetailsView" runat="server"  
        AutoGenerateRows="False"
        DataSourceID="viewMedSqlDataSource" 
        EnableModelValidation="True"  
        DefaultMode="Edit"
        DataKeyNames="RecordID" 
        Width="500px" Height="10px">
        <Fields>
            <asp:BoundField DataField="RecordID" HeaderText="RecordID" 
                InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
            <asp:TemplateField HeaderText="Sick Description" HeaderStyle-Width="150px"  SortExpression="SickDescription">
                <ItemTemplate>
                    <asp:Label ID="sickDescLabel" runat="server" Text='<%# Bind("SickDescription") %>'></asp:Label>
                </ItemTemplate>

<HeaderStyle Width="150px"></HeaderStyle>
            </asp:TemplateField>
           
     <asp:TemplateField HeaderText="Image" >                                    
                                <ItemTemplate>                               
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# "HandlerRecord.ashx?ID=" + Eval("RecordID")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
            <asp:TemplateField HeaderText="Comment" HeaderStyle-Width="150px"  SortExpression="DoctorComment">
                <EditItemTemplate>
                    <asp:TextBox ID="commentTextBox" TextMode="SingleLine" Width="200px" Height="100px" runat="server" Text='<%# Bind("DoctorComment") %>'></asp:TextBox>
                </EditItemTemplate>

<HeaderStyle Width="150px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="Edit" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                      OnClick ="update"   CommandName="Update" Text="Update" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
</center>
</asp:Content>

