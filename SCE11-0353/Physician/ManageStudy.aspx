<%@ Page AutoEventWireup="true" CodeFile="ManageStudy.aspx.cs" Culture="en-SG" Inherits="Physician.ManageStudy"
    Language="C#" MasterPageFile="~/Site.master" Title="Manage Study" %>

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
                    <asp:Label ID="StudyIdLabel" runat="server" />
                </h3>
            </div>
            <div id="Details" runat="server">
                <fieldset>
                    <legend>Details</legend>
                    <div>
                        <asp:DetailsView CellPadding="4" ForeColor="#333333" GridLines="None" ID="StudyDetails"
                            runat="server" Width="400px" AutoGenerateRows="False">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                            <EditRowStyle BackColor="#999999" />
                            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                            <Fields>
                                <asp:TemplateField HeaderText="StudyId">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("StudyId") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("Description") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Started">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("DateStarted") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Completed?">
                                    <ItemTemplate>
                                        <asp:CheckBox Checked='<%# Eval("IsCompleted") %>' Enabled="False" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Completed">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("DateCompleted") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diagnosis">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("Diagnosis") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Fields>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        </asp:DetailsView>
                    </div>
                </fieldset>
            </div>
            <div id="Images" runat="server">
                <fieldset>
                    <legend>Images</legend>
                    <asp:Panel ID="ImagesDiv" runat="server" />
                    <asp:Label ID="None" Text="No images found" runat="server" />
                </fieldset>
            </div>
            <div id="CloseStudy" runat="server">
                <fieldset>
                    <legend>Diagnosis</legend>
                    <div>
                        <asp:TextBox CssClass="textEntry" ID="Diagnosis" runat="server" Height="100px" TextMode="MultiLine"
                            Width="300px" />
                    </div>
                    <ajaxToolkit:TextBoxWatermarkExtender runat="server" TargetControlID="Diagnosis"
                        WatermarkText="Enter diagnosis here" WatermarkCssClass="watermarked" />
                    <asp:RequiredFieldValidator ControlToValidate="Diagnosis" CssClass="failureNotification"
                        Display="Dynamic" ErrorMessage="Please provide a diagnosis." ID="DiagValidate"
                        runat="server" ToolTip="Please provide a diagnosis." ValidationGroup="Group">
                    <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                    Please provide a diagnosis.
                    </asp:RequiredFieldValidator>
                    <asp:Button CssClass="buttons" Text="Close this Study" OnClick="CloseButtonClick"
                        runat="server" ValidationGroup="Group" />
                </fieldset>
            </div>
            <div class="topPadding">
                <asp:Button CssClass="buttons" PostBackUrl="~/Physician/ManagePatient.aspx" Text="Return to Patient Management"
                    runat="server" />
                <asp:Button CssClass="buttons" OnClick="ResetButtonClick" PostBackUrl="~/Physician/Default.aspx"
                    runat="server" Text="Return to Physician Homepage" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
