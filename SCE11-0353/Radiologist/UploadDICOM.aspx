<%@ Page AutoEventWireup="true" CodeFile="UploadDICOM.aspx.cs" Culture="en-SG" Inherits="Radiologist_UploadDICOM"
    Language="C#" MasterPageFile="~/Site.master" Title="Upload DICOM" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <h2>
            Upload DICOM
        </h2>
        <h3>
            Please provide the Series ID before starting upload
        </h3>
    </div>
    <%-- Error Notifications --%>
    <div class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        <asp:ValidationSummary runat="server" ValidationGroup="ImageValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <div>
        <asp:Label AssociatedControlID="SeriesID" runat="server" Text="Series ID: " />
        <asp:TextBox CssClass="textEntry" ID="SeriesID" runat="server" />
    </div>
    <div class="topPadding">
        <asp:Button CssClass="buttons" OnClick="RetrieveButtonClick" runat="server" Text="Retrieve Series"
            ValidationGroup="ImagingValidationGroup" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Radiologist/Default.aspx" runat="server"
            Text="Cancel" />
    </div>
</asp:Content>
