using System;
using System.Web.Security;

namespace Account
{
    /// <summary>
    /// Code behind for the ~/Account/Logout.aspx page
    /// </summary>

    public partial class Logout : System.Web.UI.Page
    {
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear(); // Clear all sessions so as to preserve security and privacy
            Roles.DeleteCookie();
            FormsAuthentication.SignOut(); // The actual sign out action
            Response.Redirect("~/Default.aspx"); // Redirect user back to the landing page
        }
    }
}