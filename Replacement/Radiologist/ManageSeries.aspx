<%@ Page AutoEventWireup="true" CodeFile="ManageSeries.aspx.cs" Culture="en-SG" Inherits="Radiologist.ManageSeries"
    Language="C#" MasterPageFile="~/Site.master" Title="Manage Study" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function error() {
            alert("Please upload only DICOM files.");
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <h3>
                    Series ID:
                    <asp:Label ID="SeriesID" runat="server" />
                </h3>
            </div>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" />
                <asp:ValidationSummary runat="server" ValidationGroup="Group" />
            </div>
            <%-- / Error Notifications --%>
            <div>
                <fieldset>
                    <legend>Images</legend>
                    <div>
                        This div is for displaying all the thumbnails
                    </div>
                    <asp:Label ID="None" runat="server" Text="No Images Yet" />
                </fieldset>
            </div>
            <div class="topPadding">
                <eo:AJAXUploader AllowedExtension=".dcm" AutoPostBack="True" ClientSideOnError="error"
                    ID="Uploader" OnFileUploaded="FileUploaded" ProgressBarSkin="Windows_Vista" ProgressDialogID="ProgressDialog"
                    runat="server" TempFileLocation="E:\Temp\Projects\FYP\Replacement\TempUploads"
                    Width="250px">
                    <LayoutTemplate>
                        <table border="0" cellpadding="2" cellspacing="0" width="250px">
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="InputPlaceHolder" runat="server">Input Box Place Holder </asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button CssClass="buttons" ID="UploadButton" runat="server" Text="Upload" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <eo:ProgressBar ID="ProgressBar" runat="server" ControlSkinID="Windows_Vista" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="ProgressTextPlaceHolder" runat="server">Progress Text Place Holder</asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </eo:AJAXUploader>
                <eo:AJAXUploaderProgressDialog ID="ProgressDialog" runat="server" Height="216px"
                    Width="320px" AllowResize="True" CloseButtonUrl="00020440" ControlSkinID="None"
                    HeaderHtml="Dialog Title" HeaderHtmlFormat="&lt;div style=&quot;padding-top:4px&quot;&gt;{0}&lt;/div&gt;"
                    HeaderImageHeight="27" HeaderImageUrl="00020441" MinHeight="100" MinWidth="150">
                    <HeaderStyleActive CssText="background-image:url(00020442);color:#444444;font-family:'trebuchet ms';font-size:10pt;font-weight:bold;padding-bottom:7px;padding-left:8px;padding-right:0px;padding-top:0px;" />
                    <ContentStyleActive CssText="background-color:#f0f0f0;font-family:tahoma;font-size:8pt;padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px" />
                    <ContentTemplate>
                        <p>
                            &nbsp;
                        </p>
                        <eo:ProgressBar ID="ProgressBar" runat="server" ControlSkinID="Windows_Vista" />
                        <p>
                            <asp:PlaceHolder ID="ProgressTextPlaceHolder" runat="server" />
                        </p>
                        <div style="text-align: center">
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                        </div>
                    </ContentTemplate>
                    <FooterStyleActive CssText="background-color:#f0f0f0; padding-right: 4px; padding-left: 4px; font-size: 8pt; padding-bottom: 4px; padding-top: 4px; font-family: tahoma" />
                    <BorderImages BottomBorder="00020409,00020429" BottomLeftCorner="00020408,00020428"
                        BottomRightCorner="00020410,00020430" LeftBorder="00020406,00020426" RightBorder="00020407,00020427"
                        TopBorder="00020402,00020422" TopLeftCorner="00020401,00020421" TopLeftCornerBottom="00020404,00020424"
                        TopRightCorner="00020403,00020423" TopRightCornerBottom="00020405,00020425" />
                </eo:AJAXUploaderProgressDialog>
            </div>
            <div class="topPadding">
                <asp:Button CssClass="buttons" ID="NewButton" Text="New Patient" runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Radiologist/Default.aspx"
                    runat="server" Text="Return to Radiologist Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
