<%@ Page Title="" Language="C#" MasterPageFile="~/doctor.master" AutoEventWireup="true" CodeFile="pat_update_record.aspx.cs" Inherits="pat_update_record" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">

    function SelectAllCheckboxes(spanChk) {

        // Added as ASPX uses SPAN for checkbox

        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                //elm[i].click();

                if (elm[i].checked != xState)
                    elm[i].click();
                //elm[i].checked=xState;

            }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="doctorContentPlaceHolder" Runat="Server">



<center>
    <asp:SqlDataSource ID="viewPatSqlDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:OPISWebConnectionString %>" 
        
        SelectCommand="SELECT d.DepartmentName, m.Image, m.UpdatedTime FROM Department AS d INNER JOIN MedicalRecord AS m ON d.DepartmentID = m.DepartmentID CROSS JOIN Patientinfo AS p WHERE (p.userID = @userIDParam)">
        <SelectParameters>
            <asp:Parameter Name="userIDParam" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:GridView ID="CompareGD" 
        runat="server" 
        AllowPaging="true" 
        PageSize="6"        
        AllowSorting="true" 
        AutoGenerateColumns="false" 
        DataKeyNames="userID" 
        DataSourceID="viewPatSqlDS" 
        GridLines="None"
        onselectedindexchanged="CompareGD_SelectedIndexChanged">
    <Columns>
        <asp:TemplateField HeaderText="Select">
            <HeaderTemplate>
                <input id="checkAll" runat="server" type="checkbox" onclick="javascript:SelectAllCheckboxes(this);" />
            </HeaderTemplate>
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "userID") %>
                <asp:CheckBox ID="selectCB" runat="server" />                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="userID" HeaderText="userID" InsertVisible="false" ReadOnly="true" SortExpression="userID" />
        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
        <asp:BoundField DataField="Image" HeaderText="Image" SortExpression="Image" />      
    </Columns>
    
    </asp:GridView>
</center>


</asp:Content>

