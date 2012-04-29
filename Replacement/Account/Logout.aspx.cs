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
            // Clear all session states and remove all associated cookies
            Session.Clear();
            Roles.DeleteCookie();

            // Sign the user out and redirect to application landing page
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
}