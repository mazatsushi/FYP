﻿<%@ Page AutoEventWireup="true" CodeFile="ManageImagingHistory.aspx.cs" Culture="en-SG"
    Inherits="Physician.ManageImagingHistory" Language="C#" MasterPageFile="~/Site.master"
    Title="Manage Imaging" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h2>
                Manage Medical Imaging History
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
                <fieldset>
                    <legend>Imaging History</legend>
                    <div>
                        <asp:GridView AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            ForeColor="#333333" GridLines="None" ID="AllStudies" runat="server" ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60" HeaderText="Study ID"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("StudyId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="180" HeaderText="Description"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180" ItemStyle-Wrap="True">
                                    <ItemTemplate>
                                        <asp:TextBox Enabled="False" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="160" HeaderText="Date Started"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("DateStarted") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80" HeaderText="Completed"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:CheckBox Checked='<%# Eval("IsCompleted") %>' Enabled="False" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="160" HeaderText="Date Finished"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("DateCompleted") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="180" HeaderText="Diagnosis"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180">
                                    <ItemTemplate>
                                        <asp:TextBox Enabled="False" runat="server" Text='<%# Eval("Diagnosis") %>' TextMode="MultiLine" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:HyperLink NavigateUrl='<%# "~/Physician/ManageStudy.aspx?StudyId=" + Eval("StudyId") +
                                        "&Checksum=" + CryptoHandler.GetHash(Eval("StudyId").ToString()) %>' runat="server">Details</asp:HyperLink>
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
            <div class="topPadding">
                <fieldset>
                    <legend>Schedule a New Imaging Appointment</legend>
                    <%-- New Appointment Date --%>
                    <div>
                        <asp:Label AssociatedControlID="DatePicker" runat="server" Text="* Date & Time (24 Hour): " />
                        <asp:TextBox CssClass="textEntry" ID="DatePicker" runat="server" />
                        <asp:ImageButton ID="DOB_Cal" ImageUrl="~/Images/icons/calendar.png" runat="server" />
                        <ajaxToolkit:CalendarExtender ClearTime="True" DaysModeTitleFormat="d MMM yyyy" Enabled="True"
                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy HH:mm" ID="CalendarExtender" PopupButtonID="DOB_Cal"
                            PopupPosition="TopLeft" runat="server" TargetControlID="DatePicker" TodaysDateFormat="d MMM yyyy" />
                        <ajaxToolkit:MaskedEditExtender AutoComplete="False" ClearTextOnInvalid="False" ClipboardEnabled="False"
                            CultureName="en-SG" Enabled="True" ID="DOB_Input" Mask="99/99/9999 99:99" MaskType="DateTime"
                            runat="server" TargetControlID="DatePicker" />
                        <asp:RequiredFieldValidator ControlToValidate="DatePicker" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify a date and time." runat="server"
                            ToolTip="Please specify a date and time." ValidationGroup="Group">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please specify a date and time.
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="DatePicker" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify a valid date and time." runat="server"
                            ToolTip="Please specify a valid date and time." ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](20\d\d)\s(([0-9])|([0-1][0-9])|([2][0-3])):(([0-9])|([0-5][0-9]))$"
                            ValidationGroup="Group">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please specify a valid date and time.
                        </asp:RegularExpressionValidator>
                        <asp:CustomValidator ControlToValidate="DatePicker" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify a valid date and time." OnServerValidate="DateRangeCheck"
                            runat="server" ToolTip="Please specify a valid date and time." ValidationGroup="Group">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please specify a valid date and time.
                        </asp:CustomValidator>
                    </div>
                    <%-- / New Appointment Date --%>
                    <%-- New Study --%>
                    <div class="topPadding" id="newStudyDiv" runat="server">
                        <asp:Label AssociatedControlID="Description" runat="server" Text="* Description / Purpose of Study: " />
                        <div>
                            <asp:TextBox CssClass="textEntry" Height="180" ID="Description" runat="server" TextMode="MultiLine"
                                Width="250" />
                        </div>
                        <asp:RequiredFieldValidator ControlToValidate="Description" CssClass="failureNotification"
                            Display="Dynamic" ErrorMessage="Please specify the description/purpose of the study."
                            ID="DescVal" runat="server" ToolTip="Please specify the description/purpose of the study."
                            ValidationGroup="Group">
                            <asp:Image ImageAlign="TextTop" ImageUrl="~/Images/icons/error.png" runat="server" />
                            Please specify the description/purpose of the study.
                        </asp:RequiredFieldValidator>
                    </div>
                    <%-- / New Study--%>
                    <%-- Existing Study --%>
                    <div class="topPadding" id="existingStudyDiv" runat="server">
                        <asp:Label AssociatedControlID="ExistingId" runat="server" Text="* Associated Study ID: " />
                        <asp:TextBox CssClass="textEntry" Enabled="False" ID="ExistingId" runat="server" />
                    </div>
                    <%-- / Exising Study --%>
                    <div class="topPadding">
                        <asp:Button CssClass="buttons" runat="server" Text="Schedule Appointment" OnClick="AddButtonClick"
                            ValidationGroup="Group" />
                    </div>
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
