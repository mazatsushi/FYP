<%@ Page AutoEventWireup="true" CodeFile="UploadDicom2.aspx.cs" Culture="en-SG" Inherits="Radiologist.UploadDicom2"
    Language="C#" MasterPageFile="~/Site.master" Title="Upload DICOM" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <h2>
            Upload DICOM
        </h2>
        <h3>
            Please provide the DICOM file for upload
        </h3>
    </div>
    <%-- Error Notifications --%>
    <div class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server" Text="" />
        <asp:ValidationSummary runat="server" ValidationGroup="ImageValidationGroup" />
    </div>
    <%-- / Error Notifications --%>
    <div>
        <asp:FileUpload ID="FileUpload" runat="server" />
    </div>
    <div class="topPadding">
        <asp:Button CssClass="buttons" OnClick="UploadButtonClick" runat="server" Text="Upload File"
            ValidationGroup="ImageValidationGroup" />
        <asp:Button CssClass="buttons" PostBackUrl="~/Radiologist/Default.aspx" runat="server"
            Text="Cancel" />
    </div>
</asp:Content>
