<%@ Page AutoEventWireup="true" CodeFile="ManageStudy.aspx.cs" Culture="en-SG" Inherits="Radiologist.ManageStudy"
    Language="C#" MasterPageFile="~/Site.master" Title="Manage Study" %>

<%@ Import Namespace="DB_Handlers" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Manage Study
            </h2>
            <%-- Error Notifications --%>
            <div class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server" />
                <asp:ValidationSummary runat="server" ValidationGroup="Group" />
            </div>
            <%-- / Error Notifications --%>
            <div>
                <h3>
                    Patient Name:
                    <asp:Label ID="PatientName" runat="server" />
                </h3>
            </div>
            <div>
                <h3>
                    Study ID:
                    <asp:Label ID="StudyID" runat="server" />
                </h3>
            </div>
            <div>
                <fieldset>
                    <legend>Series</legend>
                    <div>
                        <asp:GridView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            ForeColor="#333333" GridLines="None" ID="Series" runat="server" ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60" HeaderText="Series ID"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("SeriesId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="180" HeaderText="Modality"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# ModalityHandler.GetModality(int.Parse(Eval("ModalityId").ToString())) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="60">
                                    <ItemTemplate>
                                        <asp:HyperLink NavigateUrl='<%# "~/Radiologist/ManageSeries.aspx?SeriesId=" + Eval("SeriesId") +
                                        "&Checksum=" + CryptoHandler.GetHash(Eval("SeriesId").ToString()) %>' runat="server">Manage</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                    <asp:Label ID="None" runat="server" Text="None" />
                </fieldset>
            </div>
            <div>
                <fieldset>
                    <legend>Create New Series</legend>
                    <div>
                        <asp:Label AssociatedControlID="Modality" runat="server" Text="* Modality: " />
                        <ajaxToolkit:ComboBox AutoCompleteMode="Suggest" DropDownStyle="DropDownList" ID="Modality"
                            runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="Modality" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify the imaging modality." runat="server"
                            ToolTip="Please specify the imaging modality." ValidationGroup="Group">
                        <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                        Please specify the imaging modality.
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="topPadding">
                        <asp:Button CssClass="buttons" OnClick="StartButtonClick" Text="Start New Series"
                            runat="server" ValidationGroup="Group" />
                    </div>
                </fieldset>
            </div>
            <div class="topPadding">
                <asp:Button CssClass="buttons" ID="NewButton" Text="New Patient" runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
                    runat="server" Text="Return to Physician Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
