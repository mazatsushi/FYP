<%@ Page Title="" Language="C#" MasterPageFile="~/doctor.master" AutoEventWireup="true"
    CodeFile="view_medical_records.aspx.cs" Inherits="view_medical_records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="doctorContentPlaceHolder" runat="Server">
    <center>
        <table style="text-align: left" width="100%">
            <tr>
                <td align="left">
                    <b><u>Medical Record</u></b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="MedRecSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>"
                        SelectCommand="SELECT p.NRIC, p.userID, m.RecordID, m.Image, m.UpdatedTime, d.DepartmentID, d.DepartmentName FROM MedicalRecord AS m INNER JOIN Patientinfo AS p ON m.userID = p.userID INNER JOIN Department AS d ON m.DepartmentID = d.DepartmentID WHERE (p.NRIC = @NRICParam) ORDER BY m.UpdatedTime desc">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="NRICParam" QueryStringField="NRIC" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
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
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="selectMedGridView" runat="server" AllowPaging="True" PageSize="8"
                                                            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="MedRecSqlDataSource"
                                                            OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True" DataKeyNames="RecordID"
                                                            Width="500px">
                                                            <Columns>
                                                                <asp:BoundField DataField="RecordID" HeaderText="Order" ReadOnly="True" />
                                                                <asp:TemplateField HeaderText="Selection" SortExpression="Selection">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="selrecord" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:BoundField DataField="UpdatedTime" HeaderText="UpdatedTime" 
                                                                 SortExpression="UpdatedTime" />
                                                                <asp:TemplateField HeaderText="Image" SortExpression="Image">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="medImage" runat="server" ImageUrl='<%# "handlerrecord.ashx?ID=" + Eval("RecordID")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" SortExpression="DepartmentName" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="selectedbutton" runat="server" Text="Select" OnClick="selectedbutton_Click" />
                                                    </td>
                                                </tr>
                                            </table>
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
