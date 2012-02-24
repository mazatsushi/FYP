<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true"
    CodeFile="CascadingDropDown.aspx.cs"
    Inherits="CascadingDropDown_CascadingDropDown"
    Title="CascadingDropDown Sample"
    EnableEventValidation="false"
    Theme="SampleSiteTheme" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" runat="server" ID="ScriptManager1" />
    <div class="demoarea">
        <div class="demoheading">CascadingDropDown Demonstration</div>
        <table>
            <tr>
                <td>Make</td>
                <td><asp:DropDownList ID="DropDownList1" runat="server" Width="170" /></td>
            </tr>
            <tr>
                <td>Model</td>
                <td><asp:DropDownList ID="DropDownList2" runat="server" Width="170" /></td>
            </tr>
            <tr>
                <td>Color</td>
                <td><asp:DropDownList ID="DropDownList3" runat="server" Width="170" AutoPostBack="true"
                    OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" /></td>
            </tr>
        </table>
        <br />
        
        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="DropDownList1"
            Category="Make"  PromptText="Please select a make"  LoadingText="[Loading makes...]"
            ServicePath="CarsService.asmx" ServiceMethod="GetDropDownContents" />
        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="DropDownList2"
            Category="Model" PromptText="Please select a model" LoadingText="[Loading models...]"
            ServiceMethod="GetDropDownContentsPageMethod" ParentControlID="DropDownList1" />
        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="DropDownList3"
            Category="Color" PromptText="Please select a color" LoadingText="[Loading colors...]"
            ServicePath="~/CascadingDropDown/CarsService.asmx" ServiceMethod="GetDropDownContents"
            ParentControlID="DropDownList2" />
      
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="inline">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="[No response provided yet]" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList3" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="demobottom"></div>
    
    <asp:Panel ID="Description_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
            CascadingDropDown Description
        </div>
    </asp:Panel>
    <asp:Panel id="Description_ContentPanel" runat="server" style="overflow:hidden;">
        <p>
            CascadingDropDown is an ASP.NET AJAX extender that can be attached to an ASP.NET DropDownList
            control to get automatic population of a set of DropDownList controls.  Each time the selection
            of one the DropDownList controls changes, the CascadingDropDown makes a call to a specified web
            service to retrieve the list of values for the next DropDownList in the set.
        </p>
        <br />
        <p>
            CascadingDropDown enables a common scenario in which the contents of one list depends on the
            selection of another list and does so without having to embed the entire data set in the page or
            transfer it to the client at all.  All the logic about the contents of the set of DropDownList
            controls lives on the server in a web service.  This web service can use any suitable method for
            storing and looking up the relevant data.  The sample web service used here reads the data set
            from a simple hierarchical XML data file.  The sample data file shows that the DropDownList items
            can have distinct names and value (values are optional in the sample).  It also demonstrates that
            items can have an optional optionTitle attribute which will be displayed as a tooltip over the
            item in some modern browsers.  There is also a walkthrough showing how to use a 
            <a href="../Walkthrough/CCDWithDB.aspx">CascadingDropDown with a database</a>.        
        </p>
    </asp:Panel>

    <asp:Panel ID="Properties_HeaderPanel" runat="server" style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand"   />
            CascadingDropDown Properties
        </div>
    </asp:Panel>
    <asp:Panel id="Properties_ContentPanel" runat="server" style="overflow:hidden;" Height="0px">
        <p>
            The control above is initialized with this code. The <em>italic</em> properties are optional:
        </p>
<pre>&lt;ajaxToolkit:CascadingDropDown ID="CDD1" runat="server"
    TargetControlID="DropDownList2"
    Category="Model"
    <em>PromptText</em>="Please select a model"
    <em>LoadingText</em>="[Loading models...]"
    <em>ServicePath</em>="CarsService.asmx"
    ServiceMethod="GetDropDownContents"
    <em>ParentControlID</em>="DropDownList1"
    <em>SelectedValue</em>="SomeValue" /&gt;</pre>
        <ul>
            <li><strong>TargetControlID</strong> - The ID of the DropDownList to populate.</li>
            <li><strong>Category</strong> - The name of the category this DropDownList represents.</li>
            <li><strong>PromptText</strong> - Optional text to display before the user has selected a value from the DropDownList.</li>
            <li><strong>PromptValue</strong> - Optional value set when PromptText is displayed.</li>
            <li><strong>EmptyText</strong> - Optional text to display when the DropDownList has no data to display.</li>
            <li><strong>EmptyValue</strong> - Optional value set when EmptyText is displayed.</li>
            <li><strong>LoadingText</strong> - Optional text to display while the data for the DropDownList
                is being loaded.</li>
            <li><strong>ServicePath</strong> - Path to a web service that returns the data used to populate
                the DropDownList. This property should be left null if ServiceMethod refers to a page method.
                The web service should be decorated with the System.Web.Script.Services.ScriptService
                attribute.</li>
            <li><strong>ServiceMethod</strong> - Web service method that returns the data used to populate
                the DropDownList.  The signature of this method must match the following:
<pre>
[System.Web.Services.WebMethod]
[System.Web.Script.Services.ScriptMethod]
public CascadingDropDownNameValue[] GetDropDownContents(
       string knownCategoryValues, string category) { ... }</pre>
                Note that you can replace "GetDropDownContents" with a naming of your choice, but the return
                type and parameter name and type must exactly match, including case.</li>
            <li><strong>ContextKey</strong> - User/page specific context provided to an optional overload of the
                web method described by ServiceMethod/ServicePath.  If the context key is used, it should have the
                same signature with an additional parameter named contextKey of type string:
                <pre>
[System.Web.Services.WebMethod]
[System.Web.Script.Services.ScriptMethod]
public CascadingDropDownNameValue[] GetDropDownContents(
       string knownCategoryValues, string category, string contextKey) { ... }</pre>
                Note that you can replace "GetDropDownContents" with a name of your choice, but the return type
                and parameter name and type must exactly match, including case.</li>
            <li><strong>UseContextKey</strong> - Whether or not the ContextKey property should be used.  This
                will be automatically enabled if the ContextKey property is ever set (on either the client or
                the server).  If the context key is used, it should have the same signature with an additional
                parameter named contextKey of type string (as described above).</li>
            <li><strong>ParentControlID</strong> - Optional ID of the parent DropDownList that controls the
                contents of this DropDownList.</li>
            <li><strong>SelectedValue</strong> - Optional value to select by default.  This needs to exactly
                match the string representation of a value in the DropDownList.</li>
        </ul>
    </asp:Panel>
    
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server" 
        TargetControlID="Description_ContentPanel"
        ExpandControlID="Description_HeaderPanel"
        CollapseControlID="Description_HeaderPanel"
        Collapsed="False"        
        ImageControlID="Description_ToggleImage" />
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" 
        TargetControlID="Properties_ContentPanel"
        ExpandControlID="Properties_HeaderPanel"
        CollapseControlID="Properties_HeaderPanel"
        Collapsed="True"        
        ImageControlID="Properties_ToggleImage" />
</asp:Content>