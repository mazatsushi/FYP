﻿using System;
using DB_Handlers;

namespace Guest
{
    /// <summary>
    /// Code behind for the ~/Guest/AccountCreated.aspx page
    /// </summary>

    public partial class AccountCreated : System.Web.UI.Page
    {
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
        /// Redirects the user to their role's home page
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
    }
}