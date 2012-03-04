using System;
using System.Web;
using System.Web.Security;

/// <summary>
/// Code behind for the ~/Account/Logout.aspx page
/// </summary>

public partial class Account_Logout : System.Web.UI.Page
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         * Only attempt to perform logout actions on authenticated users.
         * We don't have to worry about anonymous users as they are restriced from accessing this in the local web.config
         */
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
            return;

        Session.Clear(); // Clear all sessions so as to preserve security and privacy
        Roles.DeleteCookie();
        FormsAuthentication.SignOut(); // The actual sign out action
        Response.Redirect("~/Default.aspx"); // Redirect user back to the landing page
    }
}