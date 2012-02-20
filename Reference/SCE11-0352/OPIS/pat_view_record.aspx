<%@ Page Title="" Language="C#" MasterPageFile="~/patient.master" AutoEventWireup="true" CodeFile="pat_view_record.aspx.cs" Inherits="pat_view_record" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="patientContentPlaceHolder" Runat="Server">

<center>
    <asp:SqlDataSource ID="viewMedSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>"                 
        
        SelectCommand="SELECT m.RecordID, d.DepartmentName, m.UpdatedTime FROM MedicalRecord AS m INNER JOIN Department AS d ON m.DepartmentID = d.DepartmentID WHERE (m.userID = @userParam)" DeleteCommand="DELETE FROM MedicalRecord WHERE (RecordID = @recordID)"
        >
        <SelectParameters>
            <asp:SessionParameter Name="userParam" SessionField="userID" />
        </SelectParameters>
        <%-- 
        DeleteCommand="DELETE FROM MedicalRecord WHERE RecordID = @recordIDParam"
        <DeleteParameters>
        <asp:ControlParameter ControlID="viewRecordGridView" Name="recordIDParam" PropertyName="SelectedDataKey" Type="Int16" />
        </DeleteParameters>
        --%><DeleteParameters>
            <asp:Parameter Name="recordID" />
        </DeleteParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="viewDescSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>" 
        SelectCommand="SELECT m.RecordID, m.SickDescription, m.DoctorComment, d.DepartmentName ,m.Image FROM MedicalRecord AS m INNER JOIN Department AS d ON m.DepartmentID = d.DepartmentID AND m.RecordID = @recordParam" 
        
        UpdateCommand="UPDATE MedicalRecord SET SickDescription = @sickParam WHERE (RecordID = @recordParam)">
        <SelectParameters>
            <asp:ControlParameter ControlID="viewRecordGridView" Name="recordParam" 
                PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:ControlParameter ControlID="viewSickDetailsView$sickDescTextBox" PropertyName="Text" Name="sickParam" Type="String" />           
            <asp:ControlParameter ControlID="viewRecordGridView" PropertyName="SelectedValue" Name="recordParam" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
    <asp:GridView ID="viewRecordGridView" runat="server" 
        AllowPaging="True" 
        AllowSorting="True" 
        AutoGenerateColumns="False"  
        DataSourceID="viewMedSqlDataSource"   
        DataKeyNames="RecordID"      
        EnableModelValidation="True"
        Width="500px" CellPadding="4">
        
        <RowStyle HorizontalAlign="Center" />
        <EmptyDataTemplate>
            <a href="javascript:__doPostBack('viewRecordGridView','Sort$RecordID')">Record 
            ID</a>
        </EmptyDataTemplate>
        <AlternatingRowStyle HorizontalAlign="Center" />
        <Columns> 
            <asp:BoundField DataField="RecordID" HeaderText="RecordID" 
                SortExpression="RecordID" InsertVisible="False" ReadOnly="True" />         
            <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" 
                SortExpression="DepartmentName" />
            <asp:BoundField DataField="UpdatedTime" HeaderText="UpdatedTime" 
                SortExpression="UpdatedTime" />
            <asp:CommandField ShowSelectButton="True"/> 
          <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="deleteButton" runat="server" CommandName="Delete" Text="Delete" OnClientClick="javascript:return confirm('Do you really want to \ndelete this record?');"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
         </Columns>
    </asp:GridView>
    <br />
    <asp:DetailsView ID="viewSickDetailsView" runat="server"                                            
        AutoGenerateRows="False"
        DataSourceID="viewDescSqlDataSource" 
        EnableModelValidation="True"  
        DefaultMode="Edit"
        DataKeyNames="RecordID" 
        Width="500px" Height="10px" 
        >
        <Fields>
            <asp:BoundField DataField="RecordID" HeaderText="RecordID" 
                InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
            <asp:TemplateField HeaderText="Department" HeaderStyle-Width="150px"  SortExpression="Department">
                <ItemTemplate>
                    <asp:Label ID="deptLabel" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                </ItemTemplate>  

<HeaderStyle Width="150px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sick Description" HeaderStyle-Width="150px"  SortExpression="SickDescription">
                <EditItemTemplate>
                    <asp:TextBox ID="sickDescTextBox" TextMode="SingleLine" Width="200px" Height="100px" runat="server" Text='<%# Bind("SickDescription") %>'></asp:TextBox>
                </EditItemTemplate>                

<HeaderStyle Width="150px"></HeaderStyle>
            </asp:TemplateField>
    
   <asp:TemplateField HeaderText="Image" >                                    
                                <ItemTemplate>                               
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# "HandlerRecord.ashx?ID=" + Eval("RecordID")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
            <asp:TemplateField HeaderText="Doctor Comment" HeaderStyle-Width="150px"  SortExpression="DoctorComment">
                <ItemTemplate>
                    <asp:Label ID="commentLabel" runat="server" Text='<%# Bind("DoctorComment") %>'></asp:Label>
                </ItemTemplate>  

<HeaderStyle Width="150px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="Edit" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                         OnClick ="update" CommandName="Update" Text="Update" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
</center>

</asp:Content>

