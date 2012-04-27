﻿using System;
using System.Web;

namespace Account
{
    /// <summary>
    /// Code behind for the ~/Account/ChangeQuestion.aspx page
    /// </summary>
    public partial class ChangeQuestion : System.Web.UI.Page
    {
        /// <summary>
        /// Event handler for when the Cancel button in this page is clicked
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void CancelButtonClick(object sender, EventArgs e)
        {
            switch (DatabaseHandler.FindMostPrivilegedRole(User.Identity.Name))
            {
                case 0:
                    Response.Redirect("~/Admin/Default.aspx");
                    break;
                case 1:
                    Response.Redirect("~/Patient/Default.aspx");
                    break;
                case 2:
                    Response.Redirect("~/Physician/Default.aspx");
                    break;
                case 3:
                    Response.Redirect("~/Radiologist/Default.aspx");
                    break;
            }
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            Question.Text = HttpUtility.HtmlDecode(DatabaseHandler.GetQuestion(User.Identity.Name));
        }

        /// <summary>
        /// Event handler for when the update button in this page is clicked
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void UpdateButtonClick(object sender, EventArgs e)
        {
            var password = HttpUtility.HtmlEncode(Password.Text.Trim());
            var question = HttpUtility.HtmlEncode(Question.Text.Trim());
            var answer = HttpUtility.HtmlEncode(Answer.Text.Trim().ToLowerInvariant());

            if (!DatabaseHandler.ChangeQuestionAndAnswer(User.Identity.Name, password, question, answer))
                return;

            Response.Redirect("~/Account/ChangeQuestionSuccess.aspx");
        }
    }
}