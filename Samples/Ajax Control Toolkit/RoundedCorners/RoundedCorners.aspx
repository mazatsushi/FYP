<%@ Page
    Language="C#"
    MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true"
    Inherits="CommonPage"
    Title="RoundedCorners Sample" 
    Theme="SampleSiteTheme" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="SampleContent" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <div class="demoarea">
        <div class="demoheading">RoundedCorners Demonstration</div>
        
        <asp:Panel ID="Panel1" runat="server" Width="330px" CssClass="roundedPanel">
            <div style="padding: 10px; text-align: center">
                <div style="padding: 5px; border: solid black thin; background-color: #B4B4B4;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/AJAX.gif" AlternateText="ASP.NET AJAX" /><br />
                    ASP.NET AJAX
                </div>
            </div>
        </asp:Panel>
        
        <ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server"
            BehaviorID="RoundedCornersBehavior1"
            TargetControlID="Panel1"
            Radius="6"
            Corners="All" />
        
        <div style="padding-top: 3px;">CornerRadius:</div>
        <div style="padding: 10px;">
            <input type="radio" id="radius0" name="radiusValues" value="0"
                onclick="$find('RoundedCornersBehavior1').set_Radius(this.value);" />
            <label for="radius0">None</label>
            
            <input type="radio" id="radius2" name="radiusValues" value="2"
                onclick="$find('RoundedCornersBehavior1').set_Radius(this.value);" />
            <label for="radius2">2px</label>
            
            <input type="radio" id="radius4" name="radiusValues" value="4"
                onclick="$find('RoundedCornersBehavior1').set_Radius(this.value);" />
            <label for="radius4">4px</label>
            
            <input type="radio" id="radius6" name="radiusValues" value="6"
                onclick="$find('RoundedCornersBehavior1').set_Radius(this.value);" checked="checked" />
            <label for="radius6">6px</label>
            
            <input type="radio" id="radius10" name="radiusValues" value="10"
                onclick="$find('RoundedCornersBehavior1').set_Radius(this.value);" />
            <label for="radius10">10px</label>
        </div>
        
        <div style="padding-top: 3px;">Corners:</div>
        <div style="padding: 10px;">
            <input type="checkbox" id="corner1" name="cornerValues" value="1"
                onclick="$find('RoundedCornersBehavior1').setCorner(this.value, this.checked);" checked="checked" />
            <label for="corner1">Top Left</label>
            
            <input type="checkbox" id="corner2" name="cornerValues" value="2"
                onclick="$find('RoundedCornersBehavior1').setCorner(this.value, this.checked);" checked="checked" />
            <label for="corner2">Top Right</label><br />
            
            <input type="checkbox" id="corner8" name="cornerValues" value="8"
                onclick="$find('RoundedCornersBehavior1').setCorner(this.value, this.checked);" checked="checked" />
            <label for="corner8" >Bottom Left</label>
            
            <input type="checkbox" id="corner4" name="cornerValues" value="4"
                onclick="$find('RoundedCornersBehavior1').setCorner(this.value, this.checked);" checked="checked" />
            <label for="corner4">Bottom Right</label>
        </div>
        
        <div style="padding-top: 3px;">Border Color:</div>
        <div style="padding: 10px;">
            <input type="radio" id="color0" name="colorValues" value=""
                onclick="$find('RoundedCornersBehavior1').set_BorderColor(this.value);" checked="checked" />
            <label for="color0">None</label>
            
            <input type="radio" id="color1" name="colorValues" value="Black"
                onclick="$find('RoundedCornersBehavior1').set_BorderColor(this.value);" />
            <label for="color1">Black</label>
            
            <input type="radio" id="color2" name="colorValues" value="Red"
                onclick="$find('RoundedCornersBehavior1').set_BorderColor(this.value);" />
            <label for="color2">Red</label>
            
            <input type="radio" id="color3" name="colorValues" value="Aqua"
                onclick="$find('RoundedCornersBehavior1').set_BorderColor(this.value);" />
            <label for="color3">Aqua</label>
        </div>
    </div>
    <div class="demobottom"></div>
    
    <asp:Panel ID="Description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
            RoundedCorners Description
        </div>
    </asp:Panel>
    <asp:Panel ID="Description_ContentPanel" runat="server" Style="overflow: hidden;">
        <p>
            The RoundedCorners extender applies rounded corners to existing elements. To accomplish
            this it inserts elements before and after the element that is selected, so the overall
            height of the element will change slightly. You can choose the corners of the target
            panel that should be rounded by setting the Corners property on the extender to None, 
            TopLeft, TopRight, BottomRight, BottomLeft, Top, Right, Bottom, Left, or All.
        </p>
    </asp:Panel>
    
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="expand" />
            RoundedCorners Properties
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
        <p>The control above is initialized with this code.  The <em>italic</em> properties are optional.</p>
<pre>&lt;ajaxToolkit:RoundedCornersExtender ID="rce" runat="server"
    TargetControlID="Panel1"
    <em>Radius</em>="6"
    <em>Corners</em>="All" /&gt;</pre>
        <ul>
            <li><strong>TargetControlID</strong> - The ID of the button or link for this extender
                to operate on </li>
            <li><strong>Radius</strong> - The radius of the corners (and height
                of the added area). Default is 5.</li>
            <li><strong>Corners</strong> - The corners of the target panel that will be rounded (can be
                None, TopLeft, TopRight, BottomRight, BottomLeft, Top, Right, Bottom, Left, or All)</li>
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