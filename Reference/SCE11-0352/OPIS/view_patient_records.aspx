<%@ Page Title="" Language="C#" MasterPageFile="~/doctor.master" AutoEventWireup="true" CodeFile="view_patient_records.aspx.cs" Inherits="view_patient_records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="doctorContentPlaceHolder" Runat="Server">

    <center>        
    <asp:GridView ID="viewPatGridView" runat="server" 
        AllowPaging="True" 
        AllowSorting="True" 
        AutoGenerateColumns="False" 
        EnableModelValidation="True" 
        DataKeyNames="NRIC"
        Width="500px" 
        >
        <Columns>
            <asp:CommandField ShowSelectButton="true" />
            <asp:BoundField DataField="p.userID" HeaderText="userID" SortExpression="userID" Visible="false" />
            <asp:BoundField DataField="p.Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="p.Gender" HeaderText="Gender" SortExpression="Gender" />
            <asp:BoundField DataField="p.Age" HeaderText="Age" SortExpression="Age" />
            <asp:BoundField DataField="p.NRIC" HeaderText="NRIC" SortExpression="NRIC" ReadOnly="True" />
            <asp:BoundField DataField="p.Nationality" HeaderText="Nationality" SortExpression="Nationality" />
            <asp:BoundField DataField="p.Phone_number" HeaderText="Phone Number" SortExpression="Phone_number" />
            <asp:BoundField DataField="p.Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="p.Address" HeaderText="Address" SortExpression="Address" />
                <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="selectionLabel" runat="server" Text="View Medical"  Font-Size="XX-Small">
                    </asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Button ID="selectionButton" runat="server" Text="Select" PostBackUrl='<%# "view_patient_records.aspx?NRIC=" + Eval("NRIC")%>'  />
                </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="viewMedSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>"         
        SelectCommand="SELECT RecordID, DepartmentID, SickDescription, DoctorComment, userID,Image FROM MedicalRecord WHERE (RecordID = @recordIDParam)" 
        UpdateCommand="UPDATE MedicalRecord SET DoctorComment = @commentParam WHERE (RecordID = @recordIDParam)">
        <SelectParameters>
            <asp:ControlParameter ControlID="viewPatGridView" Name="recordIDParam" 
                PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:ControlParameter ControlID="viewPatDetailsView$commentTextBox" PropertyName="Text" Name="commentParam" Type="String" />       
            <asp:ControlParameter ControlID="viewPatGridView" PropertyName="SelectedValue" Name="recordIDParam" />
        </UpdateParameters>
        
    </asp:SqlDataSource>
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

