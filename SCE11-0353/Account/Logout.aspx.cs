using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;

public partial class Account_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         * Only attempt to perform logout actions on authenticated users.
         * We don't have to worry about anonymous users as they are restriced from accessing this in the local web.config
         */
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Session.Clear(); // Clear all sessions so as to preserve security and privacy
            Roles.DeleteCookie();
            FormsAuthentication.SignOut(); // The actual sign out action
            Response.Redirect("~/Default.aspx"); // Redirect user back to the landing page
        }
    }
}