<%@ Page AutoEventWireup="true" CodeFile="ManagePatient.aspx.cs" Culture="en-SG"
    Inherits="Physician.ManagePatient" Language="C#" MasterPageFile="~/Site.master"
    Title="Manage Patient" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Manage Patient
    </h2>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" />
                <asp:ValidationSummary runat="server" ValidationGroup="ValidationGroup" />
            </div>
            <%-- / Error Notifications --%>
            <div>
                <h3>
                    Patient Name:
                    <asp:Label ID="PatientName" runat="server" />
                </h3>
            </div>
            <%-- Blood Type --%>
            <fieldset class="topPadding">
                <legend>Blood Type</legend>
                <div>
                    <asp:Label AssociatedControlID="BloodType" runat="server" Text="* Blood Type: " />
                    <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" DropDownStyle="DropDownList" ID="BloodType"
                        runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="BloodType" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Please specify the patient's blood type." runat="server"
                        ToolTip="Please specify the patient's blood type." ValidationGroup="ValidationGroup">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please specify the patient's blood type.
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ControlToValidate="BloodType" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Blood type does not exist in the system." OnServerValidate="BloodTypeExists"
                        runat="server" ToolTip="Blood type does not exist in the system." ValidationGroup="ValidationGroup">
                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Blood type does not exist in the system.
                    </asp:CustomValidator>
                    <div class="topPadding">
                        <asp:Button CssClass="buttons" runat="server" Text="Update Blood Type" ValidationGroup="ValidationGroup"
                            OnClick="BloodButtonClick" />
                    </div>
                </div>
            </fieldset>
            <%-- / Blood Type --%>
            <%-- Drug Allergies --%>
            <fieldset class="topPadding">
                <legend>Drug Allergies</legend>
                <div>
                    <asp:Label ID="None" runat="server" Text="No Known Drug Allergies" Visible="False" />
                    <asp:ListBox Enabled="False" Height="100px" ID="AllergyList" runat="server" Width="140px" />
                </div>
                <div class="topPadding">
                    <asp:Button CssClass="buttons" runat="server" Text="Update Allergies" PostBackUrl="~/Physician/UpdateAllergy.aspx" />
                </div>
            </fieldset>
            <%-- / Drug Allergies --%>
            <%-- Medical Imaging History --%>
            <fieldset class="topPadding">
                <legend>Medical Imaging History</legend>
                <div>
                </div>
                <div>
                    <asp:Button CssClass="buttons" runat="server" Text="View Imaging History" PostBackUrl="~/Physician/ManageImagingHistory.aspx" />
                </div>
            </fieldset>
            <%-- / Medical Imaging History --%>
            <div class="topPadding">
                <asp:Button CssClass="buttons" ID="NewPatient" runat="server" Text="New Patient" />
                <asp:Button CssClass="buttons" OnClick="ReturnButtonClick" runat="server" Text="Return to Physician Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
