﻿using System;
using System.Web;
using System.Web.UI.WebControls;
using DB_Handlers;

namespace Guest
{
    /// <summary>
    /// Code behind for the ~/Guest/ResetPassword.aspx page
    /// </summary>

    public partial class ResetPassword : System.Web.UI.Page
    {
        private const string SuccessRedirect = "~/Guest/ResetPassword2.aspx";

        /// <summary>
        /// Event that triggers user clicks on the next button
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void NextButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            var username = HttpUtility.HtmlEncode(UserName.Text.Trim());
            Response.Redirect(ResolveUrl(SuccessRedirect + "?Username=" + username));
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Reject if the user is already authenticated
            if (User.Identity.IsAuthenticated)
                TransferToHome(User.Identity.Name);
        }

        /// <summary>
        /// Method that redirects the user to their role's home page
        /// </summary>
        /// <param name="username">The user name</param>
        private void TransferToHome(string username)
        {
            switch (MembershipHandler.FindMostPrivilegedRole(username))
            {
                case 0:
                    Response.Redirect(ResolveUrl("~/Admin/Default.aspx"));
                    break;
                case 1:
                    Response.Redirect(ResolveUrl("~/Patients/Default.aspx"));
                    break;
                case 2:
                    Response.Redirect(ResolveUrl("~/Physician/Default.aspx"));
                    break;
                case 3:
                    Response.Redirect(ResolveUrl("~/Radiologist/Default.aspx"));
                    break;
            }
        }

        /// <summary>
        /// Validator method to check whether username exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void UserNameExists(object source, ServerValidateEventArgs args)
        {
            args.IsValid = MembershipHandler.UserNameExists(HttpUtility.HtmlEncode(UserName.Text.Trim()));
        }
    }
}