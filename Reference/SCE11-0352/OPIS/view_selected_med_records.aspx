<%@ Page Title="" Language="C#" MasterPageFile="~/doctor.master" AutoEventWireup="true" CodeFile="view_selected_med_records.aspx.cs" Inherits="view_selected_med_records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="doctorContentPlaceHolder" Runat="Server">


<center>

    
    
    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            Medical Records Selection
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                                     <asp:GridView ID="GridView1" runat="server" 
                                                            AllowSorting="True" AutoGenerateColumns="False" 
                                                            EnableModelValidation="True" 
                                                            Width="500px" DataKeyNames="RecordID" >
                                                         <Columns>
                                                             <asp:BoundField DataField="UpdatedTime" HeaderText="UpdatedTime" 
                                                                 SortExpression="UpdatedTime" />
                                                                <asp:TemplateField HeaderText="Image" SortExpression="Image">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="medImage" runat="server" ImageUrl='<%# "handlerrecord.ashx?ID=" + Eval("RecordID")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="SickDescription" SortExpression="SickDescription">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("SickDescription") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                                 <EditItemTemplate>
                                                                     <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SickDescription") %>'></asp:TextBox>
                                                                 </EditItemTemplate>
                                                             </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="DoctorComment" SortExpression="DoctorComment">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("DoctorComment") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                                 <EditItemTemplate>
                                                                     <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("DoctorComment") %>'></asp:TextBox>
                                                                 </EditItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="DepartmentName" SortExpression="DepartmentName">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                                 <EditItemTemplate>
                                                                     <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:TextBox>
                                                                 </EditItemTemplate>
                                                             </asp:TemplateField>
                                                         </Columns>
                                                
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
</center>
</asp:Content>

